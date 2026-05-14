using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using App.Domain.Entities;
using App.Domain.Identity;
using App.DTO.v1.Identity;
using App.Helpers;
using App.Modules.Identity.Application.DTO;
using App.Modules.Identity.Application.Interfaces;
using App.Modules.Identity.Domain;
using App.Modules.Identity.Infrastructure;
using Asp.Versioning;
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
    private readonly IdentityModuleDbContext _context;
    private readonly IAppRefreshTokenService _refreshTokenService;
    private readonly InstituteTypeService _instituteTypeService;
    private readonly InstituteService _instituteService;
    private readonly InstituteUserService _instituteUserService;
    private readonly IAccountService _accountService;

    private const string SettingsJWTPrefix = "JWT";
    private const string SettingsJWTKey = SettingsJWTPrefix + ":Key";
    private const string SettingsJWTIssuer = SettingsJWTPrefix + ":Issuer";
    private const string SettingsJWTAudience = SettingsJWTPrefix + ":Audience";
    private const string SettingsJWTExpiresInSeconds = SettingsJWTPrefix + ":ExpiresInSeconds";
    private const string SettingsJWTRefreshTokenExpiresInSeconds = SettingsJWTPrefix + ":RefreshTokenExpiresInSeconds";


    /// <summary>
    /// Constructor
    /// </summary>
    public AccountController(IConfiguration configuration, UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager, ILogger<AccountController> logger, IdentityModuleDbContext context,
        IAppRefreshTokenService refreshTokenService, InstituteTypeService instituteTypeService, InstituteService instituteService,
        InstituteUserService instituteUserService, IAccountService accountService)
    {
        _configuration = configuration;
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
        _context = context;
        _refreshTokenService = refreshTokenService;
        _instituteTypeService = instituteTypeService;
        _instituteService = instituteService;
        _instituteUserService = instituteUserService;
        _accountService = accountService;
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
            return Unauthorized(new App.Dto.v1.Message(result.Error!));

        var jwt = IdentityExtensions.GenerateJwt(
            result.ClaimsPrincipal!.Claims,
            _configuration.GetValue<string>(SettingsJWTKey)!,
            _configuration.GetValue<string>(SettingsJWTIssuer)!,
            _configuration.GetValue<string>(SettingsJWTAudience)!,
            DateTime.UtcNow.AddSeconds(_configuration.GetValue<int>(SettingsJWTExpiresInSeconds))
        );

        return Ok(new JWTResponse
        {
            JWT = jwt,
            RefreshToken = result.RefreshToken!
        });
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
    [ProducesResponseType(typeof(App.Dto.v1.Message), StatusCodes.Status400BadRequest)]
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<JWTResponse>> Register(
    [FromBody] Register registerModel,
    [FromQuery] int? jwtExpiresInSeconds,
    [FromQuery] int? refreshTokenExpiresInSeconds)
{
    var appUser = await _userManager.FindByEmailAsync(registerModel.Email);
    if (appUser != null)
    {
        _logger.LogWarning("User {User} already registered", registerModel.Email);
        return BadRequest(new App.Dto.v1.Message("User already registered"));
    }

    appUser = new AppUser
    {
        Email = registerModel.Email,
        UserName = registerModel.Email
    };

    var result = await _userManager.CreateAsync(appUser, registerModel.Password);
    if (!result.Succeeded)
    {
        var errors = result.Errors.Select(e => e.Description).ToList();
        return BadRequest(new App.Dto.v1.Message { Messages = errors });
    }

    _logger.LogInformation("User {Email} created a new account", appUser.Email);

    // Handle institute
    Institute? institute = null;

    if (registerModel.InstituteSelection == InstituteSelectionType.CreateNew)
    {
        if (registerModel.NewInstitute == null)
            return BadRequest(new App.Dto.v1.Message("New institute details are required"));

        var instituteType = await _instituteTypeService.GetByIdAsync(registerModel.NewInstitute.InstituteTypeId);
        if (instituteType == null)
            return BadRequest(new App.Dto.v1.Message("Invalid institute type"));

        institute = await _instituteService.CreateAndReturnAsync(new CreateInstituteRequest
        {
            Id = Guid.NewGuid(),
            InstituteName = registerModel.NewInstitute.InstituteName,
            InstituteCountry = registerModel.NewInstitute.InstituteCountry,
            InstituteAddress = registerModel.NewInstitute.InstituteAddress,
            InstitutePhoneNumber = registerModel.NewInstitute.InstitutePhoneNumber,
            InstituteTypeId = registerModel.NewInstitute.InstituteTypeId,
            CreatedAt = DateTime.UtcNow,
            Active = true
        });

        _logger.LogInformation("New institute {Name} created for {Email}", institute.InstituteName, appUser.Email);
    }
    else if (registerModel.InstituteSelection == InstituteSelectionType.SelectExisting)
    {
        if (registerModel.InstituteId == null)
            return BadRequest(new App.Dto.v1.Message("Institute ID is required"));

        institute = await _instituteService.GetEntityByIdAsync(registerModel.InstituteId.Value);
        if (institute == null)
            return BadRequest(new App.Dto.v1.Message("Institute not found"));

        _logger.LogInformation("User {Email} joining institute {Id}", appUser.Email, institute.Id);
    }
    else
    {
        return BadRequest(new App.Dto.v1.Message("Invalid institute selection"));
    }

    // Link user to institute
    var instituteUser = new CreateInstituteUserRequest
    {
        Id = Guid.NewGuid(),
        InstituteId = institute.Id,
        UserId = appUser.Id,
        Role = EInstituteUserRole.Employee
    };
    await _instituteUserService.CreateAsync(instituteUser);
    await UserRoleHelper.SyncCompanyUserRolesToIdentityAsync(_userManager, appUser, instituteUser.Role);

    _logger.LogInformation("InstituteUser created for {Email} with role {Role}", appUser.Email, instituteUser.Role);

    // Create refresh token through service
    var tokenResponse = await _refreshTokenService.CreateAsync(new CreateAppRefreshTokenRequest
    {
        UserId = appUser.Id,
        ExpiresAt = GetExpirationDateTime(refreshTokenExpiresInSeconds, SettingsJWTRefreshTokenExpiresInSeconds)
    });

    var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
    var jwt = IdentityExtensions.GenerateJwt(
        claimsPrincipal.Claims,
        _configuration.GetValue<string>(SettingsJWTKey)!,
        _configuration.GetValue<string>(SettingsJWTIssuer)!,
        _configuration.GetValue<string>(SettingsJWTAudience)!,
        GetExpirationDateTime(jwtExpiresInSeconds, SettingsJWTExpiresInSeconds)
    );

    return Ok(new JWTResponse
    {
        JWT = jwt,
        RefreshToken = tokenResponse.RefreshToken
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
    [ProducesResponseType(typeof(App.Dto.v1.Message), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<JWTResponse>> RenewRefreshToken(
    [FromBody] RefreshTokenModel refreshTokenModel,
    [FromQuery] int? jwtExpiresInSeconds,
    [FromQuery] int? refreshTokenExpiresInSeconds)
{
    if (refreshTokenModel == null)
        return BadRequest(new App.Dto.v1.Message("Request body is required"));

    if (string.IsNullOrWhiteSpace(refreshTokenModel.Jwt))
        return BadRequest(new App.Dto.v1.Message("JWT is required"));

    if (string.IsNullOrWhiteSpace(refreshTokenModel.RefreshToken))
        return BadRequest(new App.Dto.v1.Message("Refresh token is required"));

    // Parse JWT
    JwtSecurityToken jwtToken;
    try
    {
        jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(refreshTokenModel.Jwt);
    }
    catch (Exception e)
    {
        return BadRequest(new App.Dto.v1.Message($"Cannot parse token: {e.Message}"));
    }

    // Validate signature, issuer, audience, etc. Ignore expiration for refresh flow.
    if (!IdentityExtensions.ValidateJwt(
            refreshTokenModel.Jwt,
            _configuration.GetValue<string>(SettingsJWTKey)!,
            _configuration.GetValue<string>(SettingsJWTIssuer)!,
            _configuration.GetValue<string>(SettingsJWTAudience)!))
    {
        return BadRequest(new App.Dto.v1.Message("JWT validation failed"));
    }

    // Prefer stable user id claim over email
    var userIdValue =
        jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value ??
        jwtToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;

    if (string.IsNullOrWhiteSpace(userIdValue) || !Guid.TryParse(userIdValue, out var userId))
        return BadRequest(new App.Dto.v1.Message("No valid user id in JWT"));

    var appUser = await _userManager.FindByIdAsync(userId.ToString());
    if (appUser == null)
        return NotFound(new App.Dto.v1.Message("User not found"));

    var newRefreshTokenExpiresAt = GetExpirationDateTime(
        refreshTokenExpiresInSeconds,
        SettingsJWTRefreshTokenExpiresInSeconds);

    // Validate old refresh token and rotate it
    var tokenResponse = await _refreshTokenService.ValidateAndRotateAsync(
        refreshTokenModel.RefreshToken,
        appUser.Id,
        newRefreshTokenExpiresAt);

    if (tokenResponse == null)
        return BadRequest(new App.Dto.v1.Message("Invalid or expired refresh token"));

    var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);

    var jwt = IdentityExtensions.GenerateJwt(
        claimsPrincipal.Claims,
        _configuration.GetValue<string>(SettingsJWTKey)!,
        _configuration.GetValue<string>(SettingsJWTIssuer)!,
        _configuration.GetValue<string>(SettingsJWTAudience)!,
        GetExpirationDateTime(jwtExpiresInSeconds, SettingsJWTExpiresInSeconds)
    );

    return Ok(new JWTResponse
    {
        JWT = jwt,
        RefreshToken = tokenResponse.RefreshToken
    });
}

    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(App.Dto.v1.Message), StatusCodes.Status404NotFound)]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpPost]
    public async Task<ActionResult> Logout([FromBody] LogoutInfo logout)
    {
        var userId = User.UserId();
    
        if (string.IsNullOrWhiteSpace(logout.RefreshToken))
            return BadRequest(new App.Dto.v1.Message("Refresh token is required"));

        await _refreshTokenService.RevokeAsync(logout.RefreshToken, userId, "logout");

        return Ok();
    }
    
    [Authorize(AuthenticationSchemes = "Bearer")]
[HttpPost("set-institute")]
public async Task<ActionResult> SetInstitute([FromBody] SetInstituteDto setInstitute)
{
    var userId = User.UserId();

    Institute? institute = null;
    if (!Enum.IsDefined(typeof(InstituteSelectionType), setInstitute.InstituteSelection))
        return BadRequest(new App.Dto.v1.Message("Invalid institute selection"));
    var selection = (InstituteSelectionType)setInstitute.InstituteSelection;

    if (selection == InstituteSelectionType.CreateNew)
    {
        if (setInstitute.NewInstitute == null)
            return BadRequest(new App.Dto.v1.Message("New institute details are required"));

        if (string.IsNullOrWhiteSpace(setInstitute.NewInstitute.InstituteName))
            return BadRequest(new App.Dto.v1.Message("Institute name is required"));

        Guid? instituteTypeId = string.IsNullOrEmpty(setInstitute.NewInstitute.InstituteTypeId)
            ? null
            : Guid.TryParse(setInstitute.NewInstitute.InstituteTypeId, out var parsed) ? parsed : null;

        if (instituteTypeId == null)
            return BadRequest(new App.Dto.v1.Message("Invalid institute type ID"));

        institute = await _instituteService.CreateAndReturnAsync(new CreateInstituteRequest
        {
            Id = Guid.NewGuid(),
            InstituteName = setInstitute.NewInstitute.InstituteName,
            InstituteCountry = setInstitute.NewInstitute.InstituteCountry ?? string.Empty,
            InstituteAddress = setInstitute.NewInstitute.InstituteAddress ?? string.Empty,
            InstitutePhoneNumber = setInstitute.NewInstitute.InstitutePhoneNumber ?? string.Empty,
            InstituteTypeId = instituteTypeId.Value,
            Active = true,
            CreatedAt = DateTime.UtcNow
        });

        _logger.LogInformation("New institute {Name} created by user {UserId}", institute.InstituteName, userId);
    }
    else if (selection == InstituteSelectionType.SelectExisting)
    {
        if (string.IsNullOrWhiteSpace(setInstitute.InstituteId))
            return BadRequest(new App.Dto.v1.Message("Institute ID is required"));

        if (!Guid.TryParse(setInstitute.InstituteId, out var instituteGuid))
            return BadRequest(new App.Dto.v1.Message("Invalid institute ID format"));

        institute = await _instituteService.GetFirstActiveByIdAsync(instituteGuid);
        if (institute == null)
            return BadRequest(new App.Dto.v1.Message("Institute not found or inactive"));

        _logger.LogInformation("User {UserId} joining institute {InstituteId}", userId, instituteGuid);
    }
    else
    {
        return BadRequest(new App.Dto.v1.Message("Invalid institute selection"));
    }

    // Check if link already exists
    var existingLink = await _context.InstituteUsers
        .Where(iu => iu.UserId == userId && iu.InstituteId == institute.Id)
        .FirstOrDefaultAsync();

    if (existingLink != null)
        return BadRequest(new App.Dto.v1.Message("User is already linked to this institute"));

    var instituteUser = new InstituteUser
    {
        Id = Guid.NewGuid(),
        UserId = userId,
        InstituteId = institute.Id,
        Role = EInstituteUserRole.Employee
    };

    _context.InstituteUsers.Add(instituteUser);
    await _context.SaveChangesAsync();

    _logger.LogInformation("User {UserId} linked to institute {InstituteId} with role {Role}",
        userId, institute.Id, instituteUser.Role);

    return Ok(new { InstituteId = institute.Id, InstituteName = institute.InstituteName });
}
    private DateTime GetExpirationDateTime(int? expiresInSeconds, string settingsKey)
    {
        if (expiresInSeconds <= 0) expiresInSeconds = int.MaxValue;
        expiresInSeconds = expiresInSeconds < _configuration.GetValue<int>(settingsKey)
            ? expiresInSeconds
            : _configuration.GetValue<int>(settingsKey);

        return DateTime.UtcNow.AddSeconds(expiresInSeconds ?? 60);
    }
}