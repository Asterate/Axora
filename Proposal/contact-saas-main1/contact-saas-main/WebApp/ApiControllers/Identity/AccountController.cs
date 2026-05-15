using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using App.Domain.Entities;
using App.Domain.Identity;
using App.Dto.v1;
using App.DTO.v1.Identity;
using App.Modules.Identity.Application.DTO;
using App.Modules.Identity.Application.Interfaces;
using App.Modules.Identity.Domain;
using App.Modules.Identity.Infrastructure;
using App.Shared.Contracts.Events;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Helpers;

namespace WebApp.ApiControllers.Identity;

/// <summary>
/// User account controller - login, register, etc.
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<AppUser> _userManager;
    private readonly ILogger<AccountController> _logger;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IAppRefreshTokenService _refreshTokenService;
    private readonly IAccountService _accountService;
    private readonly IMediator _mediator;

    private const string SettingsJwtPrefix = "JWT";
    private const string SettingsJwtKey = SettingsJwtPrefix + ":Key";
    private const string SettingsJwtIssuer = SettingsJwtPrefix + ":Issuer";
    private const string SettingsJwtAudience = SettingsJwtPrefix + ":Audience";
    private const string SettingsJwtExpiresInSeconds = SettingsJwtPrefix + ":ExpiresInSeconds";
    private const string SettingsJwtRefreshTokenExpiresInSeconds = SettingsJwtPrefix + ":RefreshTokenExpiresInSeconds";


    /// <summary>
    /// Constructor
    /// </summary>
    public AccountController(IConfiguration configuration, UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager, ILogger<AccountController> logger,
        IAppRefreshTokenService refreshTokenService, IAccountService accountService,IMediator mediator )
    {
        _configuration = configuration;
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
        _refreshTokenService = refreshTokenService;
        _accountService = accountService;
        _mediator = mediator;
    }

    /// <summary>
    /// User authentication, returns JWT and refresh token
    /// </summary>
    /// <param name="loginInfo">Login model</param>
    /// <param name="jwtExpiresInSeconds">Optional, use custom jwt expiration</param>
    /// <param name="refreshTokenExpiresInSeconds">Optional, use custom refresh token expiration</param>
    /// <returns>JWT and refresh token</returns>
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<JWTResponse>> Login(
        [FromBody] Login loginInfo,
        [FromQuery] int? jwtExpiresInSeconds,
        [FromQuery] int? refreshTokenExpiresInSeconds)
    {
        var result = await _accountService.Login(loginInfo, jwtExpiresInSeconds, refreshTokenExpiresInSeconds);
        if (!result.Success)
            return Unauthorized(new Message(result.Error!));

        var jwt = IdentityExtensions.GenerateJwt(
            result.ClaimsPrincipal!.Claims,
            _configuration.GetValue<string>(SettingsJwtKey)!,
            _configuration.GetValue<string>(SettingsJwtIssuer)!,
            _configuration.GetValue<string>(SettingsJwtAudience)!,
            DateTime.UtcNow.AddSeconds(_configuration.GetValue<int>(SettingsJwtExpiresInSeconds)));

        return Ok(new JWTResponse { JWT = jwt, RefreshToken = result.RefreshToken! });
    }
    
    /// <summary>
    /// Register new user, returns JWT and refresh token
    /// </summary>
    /// <param name="registerModel">Reg info</param>
    /// <param name="jwtExpiresInSeconds">Optional custom jwt expiration</param>
    /// <param name="refreshTokenExpiresInSeconds">Optional custom refresh token expiration</param>
    /// <returns></returns>
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(JWTResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status400BadRequest)]
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<JWTResponse>> Register(
    [FromBody] Register registerModel,
    [FromQuery] int? jwtExpiresInSeconds,
    [FromQuery] int? refreshTokenExpiresInSeconds,
    CancellationToken ct)
{
    var validationError = ValidateInstituteSelection(registerModel);
    if (validationError != null)
        return BadRequest(new Message(validationError));
    var accountResult = await _accountService.RegisterUserAsync(
        registerModel,
        refreshTokenExpiresInSeconds);
    if (!accountResult.Success)
     return BadRequest(new Message(accountResult.Error!));
    
    await _mediator.Publish(new UserRegisteredEvent(
        UserId: accountResult.User!.Id,
        Email: registerModel.Email,
        IsNewInstitute: registerModel.InstituteSelection == InstituteSelectionType.CreateNew,
        ExistingInstituteId: registerModel.InstituteId,
        NewInstituteName: registerModel.NewInstitute?.InstituteName,
        NewInstituteCountry: registerModel.NewInstitute?.InstituteCountry,
        NewInstituteAddress: registerModel.NewInstitute?.InstituteAddress,
        NewInstitutePhone: registerModel.NewInstitute?.InstitutePhoneNumber,
        NewInstituteTypeId: registerModel.NewInstitute?.InstituteTypeId
    ), ct);
    
    var jwt = IdentityExtensions.GenerateJwt(
        accountResult.ClaimsPrincipal!.Claims,
        _configuration.GetValue<string>(SettingsJwtKey)!,
        _configuration.GetValue<string>(SettingsJwtIssuer)!,
        _configuration.GetValue<string>(SettingsJwtAudience)!,
        _accountService.GetExpirationDateTime(jwtExpiresInSeconds, SettingsJwtExpiresInSeconds));

    return Ok(new JWTResponse
    {
        JWT = jwt,
        RefreshToken = accountResult.RefreshToken!
    });
}

    /// <summary>
    /// Renew JWT using refresh token
    /// </summary>
    /// <param name="refreshTokenModel">Data for renewal</param>
    /// <param name="jwtExpiresInSeconds">Optional custom expiration for jwt</param>
    /// <param name="refreshTokenExpiresInSeconds">Optional custom expiration for refresh token</param>
    /// <returns></returns>
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(JWTResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<JWTResponse>> RenewRefreshToken(
    [FromBody] RefreshTokenModel refreshTokenModel,
    [FromQuery] int? jwtExpiresInSeconds,
    [FromQuery] int? refreshTokenExpiresInSeconds)
{
    if (refreshTokenModel == null)
        return BadRequest(new Message("Request body is required"));

    if (string.IsNullOrWhiteSpace(refreshTokenModel.Jwt))
        return BadRequest(new Message("JWT is required"));

    if (string.IsNullOrWhiteSpace(refreshTokenModel.RefreshToken))
        return BadRequest(new Message("Refresh token is required"));

    // Parse JWT
    JwtSecurityToken jwtToken;
    try
    {
        jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(refreshTokenModel.Jwt);
    }
    catch (Exception e)
    {
        return BadRequest(new Message($"Cannot parse token: {e.Message}"));
    }

    // Validate signature, issuer, audience, etc. Ignore expiration for refresh flow.
    if (!IdentityExtensions.ValidateJwt(
            refreshTokenModel.Jwt,
            _configuration.GetValue<string>(SettingsJwtKey)!,
            _configuration.GetValue<string>(SettingsJwtIssuer)!,
            _configuration.GetValue<string>(SettingsJwtAudience)!))
    {
        return BadRequest(new Message("JWT validation failed"));
    }

    // Prefer stable user id claim over email
    var userIdValue =
        jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value ??
        jwtToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;

    if (string.IsNullOrWhiteSpace(userIdValue) || !Guid.TryParse(userIdValue, out var userId))
        return BadRequest(new Message("No valid user id in JWT"));

    var appUser = await _userManager.FindByIdAsync(userId.ToString());
    if (appUser == null)
        return NotFound(new Message("User not found"));

    var newRefreshTokenExpiresAt = _accountService.GetExpirationDateTime(
        refreshTokenExpiresInSeconds,
        SettingsJwtRefreshTokenExpiresInSeconds);

    // Validate old refresh token and rotate it
    var tokenResponse = await _refreshTokenService.ValidateAndRotateAsync(
        refreshTokenModel.RefreshToken,
        appUser.Id,
        newRefreshTokenExpiresAt);

    if (tokenResponse == null)
        return BadRequest(new Message("Invalid or expired refresh token"));

    var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);

    var jwt = IdentityExtensions.GenerateJwt(
        claimsPrincipal.Claims,
        _configuration.GetValue<string>(SettingsJwtKey)!,
        _configuration.GetValue<string>(SettingsJwtIssuer)!,
        _configuration.GetValue<string>(SettingsJwtAudience)!,
        _accountService.GetExpirationDateTime(jwtExpiresInSeconds, SettingsJwtExpiresInSeconds)
    );

    return Ok(new JWTResponse
    {
        JWT = jwt,
        RefreshToken = tokenResponse.RefreshToken
    });
}

    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(Message), StatusCodes.Status404NotFound)]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpPost]
    public async Task<ActionResult> Logout([FromBody] LogoutInfo logout)
    {
        var userId = User.UserId();
    
        if (string.IsNullOrWhiteSpace(logout.RefreshToken))
            return BadRequest(new Message("Refresh token is required"));

        await _refreshTokenService.RevokeAsync(logout.RefreshToken, userId);

        return Ok();
    }
    
    [HttpPost("set-institute")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<ActionResult> SetInstitute(
        [FromBody] SetInstituteDto setInstitute,
        CancellationToken ct)
    {
        var userId = User.UserId();

        try
        {
            await _mediator.Publish(new UserRegisteredEvent(
                UserId: userId,
                Email: User.FindFirstValue(ClaimTypes.Email)!,
                IsNewInstitute: setInstitute.InstituteSelection == (int)InstituteSelectionType.CreateNew,
                ExistingInstituteId: Guid.TryParse(setInstitute.InstituteId, out var g) ? g : null,
                NewInstituteName: setInstitute.NewInstitute?.InstituteName,
                NewInstituteCountry: setInstitute.NewInstitute?.InstituteCountry,
                NewInstituteAddress: setInstitute.NewInstitute?.InstituteAddress,
                NewInstitutePhone: setInstitute.NewInstitute?.InstitutePhoneNumber,
                NewInstituteTypeId: Guid.TryParse(setInstitute.NewInstitute?.InstituteTypeId, out var tg) ? tg : null
            ), ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "SetInstitute failed for user {UserId}", userId);
            return BadRequest(new Message(ex.Message));
        }

        return Ok();
    }
    private static string? ValidateInstituteSelection(Register model) =>
        model.InstituteSelection switch
        {
            InstituteSelectionType.CreateNew when model.NewInstitute == null 
                => "New institute details are required",
            InstituteSelectionType.SelectExisting when model.InstituteId == null 
                => "Institute ID is required",
            InstituteSelectionType.CreateNew 
                => null,  // valid
            InstituteSelectionType.SelectExisting 
                => null,  // valid
            _ => "Invalid institute selection"
        };
}