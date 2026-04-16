using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using App.DAL.EF;
using App.Domain.Entities;
using App.Domain.Identity;
using App.DTO.v1;
using App.DTO.v1.Identity;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
    private readonly Random _random = new Random();
    private readonly AppDbContext _context;

    private const string UserPassProblem = "User/Password problem";
    private const int RandomDelayMin = 500;
    private const int RandomDelayMax = 5000;

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
        SignInManager<AppUser> signInManager, ILogger<AccountController> logger, AppDbContext context)
    {
        _configuration = configuration;
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
        _context = context;
    }

    /// <summary>
    /// User authentication, returns JWT and refresh token
    /// </summary>
    /// <param name="loginInfo">Login model</param>
    /// <param name="jwtExpiresInSeconds">Optional, use custom jwt expiration</param>
    /// <param name="refreshTokenExpiresInSeconds">Optional, use custom refresh token expiration</param>
    /// <returns>JWT and refresh token</returns>
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(JWTResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(App.Dto.v1.Message), StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<JWTResponse>> Login(
        [FromBody]
        Login loginInfo,
        [FromQuery]
        int? jwtExpiresInSeconds,
        [FromQuery]
        int? refreshTokenExpiresInSeconds
    )
    {
        // verify user
        var appUser = await _userManager.FindByEmailAsync(loginInfo.Email);
        if (appUser == null)
        {
            _logger.LogWarning("WebApi login failed, email {} not found", loginInfo.Email);
            await Task.Delay(_random.Next(RandomDelayMin, RandomDelayMax));
            return NotFound(new App.Dto.v1.Message(UserPassProblem));
        }

        // verify password
        var result = await _signInManager.CheckPasswordSignInAsync(appUser, loginInfo.Password, false);
        if (!result.Succeeded)
        {
            _logger.LogWarning("WebApi login failed, password {} for email {} was wrong", loginInfo.Password,
                loginInfo.Email);
            await Task.Delay(_random.Next(RandomDelayMin, RandomDelayMax));
            return NotFound(new App.Dto.v1.Message(UserPassProblem));
        }

        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
        if (!_context.Database.ProviderName!.Contains("InMemory"))
        {
            var deletedRows = await _context
                .RefreshTokens
                .Where(t => t.UserId == appUser.Id && t.Expiration < DateTime.UtcNow)
                .ExecuteDeleteAsync();
            _logger.LogInformation("Deleted {} refresh tokens", deletedRows);
        }
        else
        {
            //TODO: inMemory delete for testing
        }

        var refreshToken = new AppRefreshToken()
        {
            UserId = appUser.Id,
            Expiration = GetExpirationDateTime(refreshTokenExpiresInSeconds, SettingsJWTRefreshTokenExpiresInSeconds)
        };
        _context.RefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync();


        var jwt = IdentityExtensions.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration.GetValue<string>(SettingsJWTKey)!,
            _configuration.GetValue<string>(SettingsJWTIssuer)!,
            _configuration.GetValue<string>(SettingsJWTAudience)!,
            GetExpirationDateTime(jwtExpiresInSeconds, SettingsJWTExpiresInSeconds)
        );

        var responseData = new JWTResponse()
        {
            JWT = jwt,
            RefreshToken = refreshToken.RefreshToken
        };

        return Ok(responseData);
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
        [FromBody]
        Register registerModel,
        [FromQuery]
        int? jwtExpiresInSeconds,
        [FromQuery]
        int? refreshTokenExpiresInSeconds
    )
    {
        var appUser = await _userManager.FindByEmailAsync(registerModel.Email);
        if (appUser != null)
        {
            _logger.LogWarning(" User {User} already registered", registerModel.Email);
            return BadRequest(new App.Dto.v1.Message("User already registered"));
        }

        var refreshToken = new AppRefreshToken()
        {
            Expiration = GetExpirationDateTime(refreshTokenExpiresInSeconds, SettingsJWTRefreshTokenExpiresInSeconds)
        };

        appUser = new AppUser()
        {
            Email = registerModel.Email,
            UserName = registerModel.Email,

            RefreshTokens = new List<AppRefreshToken>()
            {
                refreshToken
            }
        };
        var result = await _userManager.CreateAsync(appUser, registerModel.Password);

        if (result.Succeeded)
        {
            _logger.LogInformation("User {Email} created a new account with password", appUser.Email);

            // Handle institute selection
            Institute? institute = null;
            
            if (registerModel.InstituteSelection == App.DTO.v1.Identity.InstituteSelectionType.CreateNew)
            {
                // Create new institute
                if (registerModel.NewInstitute == null)
                {
                    return BadRequest(new App.Dto.v1.Message("New institute details are required when creating a new institute"));
                }
                
                // Get institute type
                var instituteType = await _context.InstituteTypes.FindAsync(registerModel.NewInstitute.InstituteTypeId);
                if (instituteType == null)
                {
                    return BadRequest(new App.Dto.v1.Message("Invalid institute type"));
                }
                
                institute = new App.Domain.Entities.Institute
                {
                    Id = Guid.NewGuid(),
                    InstituteName = registerModel.NewInstitute.InstituteName,
                    InstituteCountry = registerModel.NewInstitute.InstituteCountry,
                    InstituteAddress = registerModel.NewInstitute.InstituteAddress,
                    InstitutePhoneNumber = registerModel.NewInstitute.InstitutePhoneNumber,
                    InstituteTypeId = registerModel.NewInstitute.InstituteTypeId,
                    InstituteType = instituteType,
                    CreatedAt = DateTime.UtcNow,
                    Active = true
                };
                _context.Institutes.Add(institute);
                _logger.LogInformation("New institute {InstituteName} created for {Email}", institute.InstituteName, appUser.Email);
            }
            else if (registerModel.InstituteSelection == App.DTO.v1.Identity.InstituteSelectionType.SelectExisting)
            {
                // Use existing institute
                if (registerModel.InstituteId == null)
                {
                    return BadRequest(new App.Dto.v1.Message("Institute ID is required when selecting an existing institute"));
                }
                
                institute = await _context.Institutes.FindAsync(registerModel.InstituteId);
                if (institute == null)
                {
                    return BadRequest(new App.Dto.v1.Message("Institute not found"));
                }
                _logger.LogInformation("User {Email} joining existing institute {InstituteId}", appUser.Email, registerModel.InstituteId);
            }
            else
            {
                // Fallback: use default institute
                institute = _context.Institutes.FirstOrDefault();
                if (institute == null)
                {
                    return BadRequest(new App.Dto.v1.Message("No institutes available. Please create one first."));
                }
            }

            // Create InstituteUser linking user to institute
            var instituteUser = new App.Domain.Entities.InstituteUser
            {
                Id = Guid.NewGuid(),
                InstituteId = institute.Id,
                User = appUser,
                Role = App.Domain.Entities.EInstituteUserRole.Employee
            };
            _context.InstituteUsers.Add(instituteUser);
            await _context.SaveChangesAsync();
            
            // Sync user roles based on InstituteUser role
            await App.Helpers.UserRoleHelper.SyncCompanyUserRolesToIdentityAsync(_userManager, appUser, instituteUser.Role);
            
            _logger.LogInformation("InstituteUser created for {Email} with institute {InstituteName} and role {Role}", appUser.Email, institute.InstituteName, instituteUser.Role);

            var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
            var jwt = IdentityExtensions.GenerateJwt(
                claimsPrincipal.Claims,
                _configuration.GetValue<string>(SettingsJWTKey)!,
                _configuration.GetValue<string>(SettingsJWTIssuer)!,
                _configuration.GetValue<string>(SettingsJWTAudience)!,
                GetExpirationDateTime(jwtExpiresInSeconds, SettingsJWTExpiresInSeconds)
            );
            _logger.LogInformation("WebApi login. User {User}", registerModel.Email);
            return Ok(new JWTResponse()
            {
                JWT = jwt,
                RefreshToken = refreshToken.RefreshToken,
            });
        }

        var errors = result.Errors.Select(error => error.Description).ToList();
        return BadRequest(new App.Dto.v1.Message() { Messages = errors });
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
        [FromBody]
        RefreshTokenModel refreshTokenModel,
        [FromQuery]
        int? jwtExpiresInSeconds,
        [FromQuery]
        int? refreshTokenExpiresInSeconds
    )
    {
        JwtSecurityToken jwtToken;
        // get user info from jwt
        try
        {
            jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(refreshTokenModel.Jwt);
            if (jwtToken == null)
            {
                return BadRequest(new App.Dto.v1.Message("No token"));
            }
        }
        catch (Exception e)
        {
            return BadRequest(new App.Dto.v1.Message($"Cant parse the token, {e.Message}"));
        }

        // validate jwt, ignore expiration date
        if (!IdentityExtensions.ValidateJwt(
                refreshTokenModel.Jwt,
                _configuration.GetValue<string>(SettingsJWTKey)!,
                _configuration.GetValue<string>(SettingsJWTIssuer)!,
                _configuration.GetValue<string>(SettingsJWTAudience)!
            ))
        {
            return BadRequest("JWT validation fail");
        }

        var userEmail = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        if (userEmail == null)
        {
            return BadRequest(new App.Dto.v1.Message("No email in jwt"));
        }

        // get user and tokens
        var appUser = await _userManager.FindByEmailAsync(userEmail);
        if (appUser == null)
        {
            return NotFound($"User with email {userEmail} not found");
        }


        // load and compare refresh tokens

        await _context.Entry(appUser).Collection(u => u.RefreshTokens!)
            .Query()
            .Where(x =>
                (x.RefreshToken == refreshTokenModel.RefreshToken && x.Expiration > DateTime.UtcNow) ||
                (x.PreviousRefreshToken == refreshTokenModel.RefreshToken &&
                 x.PreviousExpiration > DateTime.UtcNow)
            )
            .ToListAsync();

        if (appUser.RefreshTokens == null)
        {
            return Problem("RefreshTokens collection is null");
        }

        if (appUser.RefreshTokens.Count == 0)
        {
            return Problem("RefreshTokens collection is empty, no valid refresh tokens found");
        }

        if (appUser.RefreshTokens.Count != 1)
        {
            return Problem("More than one valid refresh token found.");
        }

        // generate new jwt

        // get claims based user
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);

        // generate jwt
        var jwt = IdentityExtensions.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration.GetValue<string>(SettingsJWTKey)!,
            _configuration.GetValue<string>(SettingsJWTIssuer)!,
            _configuration.GetValue<string>(SettingsJWTAudience)!,
            GetExpirationDateTime(jwtExpiresInSeconds, SettingsJWTExpiresInSeconds)
        );

        // make new refresh token, obsolete old ones
        var refreshToken = appUser.RefreshTokens.First();
        if (refreshToken.RefreshToken == refreshTokenModel.RefreshToken)
        {
            refreshToken.PreviousRefreshToken = refreshToken.RefreshToken;
            refreshToken.PreviousExpiration = DateTime.UtcNow.AddMinutes(1);

            refreshToken.RefreshToken = Guid.NewGuid().ToString();
            refreshToken.Expiration =
                GetExpirationDateTime(refreshTokenExpiresInSeconds, SettingsJWTRefreshTokenExpiresInSeconds);

            await _context.SaveChangesAsync();
        }

        var res = new JWTResponse()
        {
            JWT = jwt,
            RefreshToken = refreshToken.RefreshToken,
        };

        return Ok(res);
    }

    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(App.Dto.v1.Message), StatusCodes.Status404NotFound)]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpPost]
    public async Task<ActionResult> Logout([FromBody] LogoutInfo logout)
    {
        // delete the refresh token - so user is kicked out after jwt expiration
        // We do not invalidate the jwt on serverside - that would require pipeline modification and checking against db on every request
        // so client can actually continue to use the jwt until it expires (keep the jwt expiration time short ~1 min)

        var appUser = await _context.Users
            .Where(u => u.Id == User.UserId())
            .SingleOrDefaultAsync();
        if (appUser == null)
        {
            return NotFound(
                new App.Dto.v1.Message(UserPassProblem)
            );
        }

        await _context.Entry(appUser)
            .Collection(u => u.RefreshTokens!)
            .Query()
            .Where(x =>
                (x.RefreshToken == logout.RefreshToken) ||
                (x.PreviousRefreshToken == logout.RefreshToken)
            )
            .ToListAsync();

        foreach (var appRefreshToken in appUser.RefreshTokens!)
        {
            _context.RefreshTokens.Remove(appRefreshToken);
        }

        var deleteCount = await _context.SaveChangesAsync();

        return Ok(new { TokenDeleteCount = deleteCount });
    }

    [HttpPost("set-institute")]
    public async Task<ActionResult> SetInstitute([FromBody] App.DTO.v1.Identity.SetInstituteDto setInstitute)
    {
        var userId = User.UserId();
        var appUser = await _context.Users
            .Where(u => u.Id == userId)
            .SingleOrDefaultAsync();

        if (appUser == null)
        {
            return NotFound(new App.Dto.v1.Message("User not found"));
        }

        Institute? institute;

        if (setInstitute.InstituteSelection == 1)
        {
            // Create new institute
            Guid? instituteTypeId = string.IsNullOrEmpty(setInstitute.NewInstitute?.InstituteTypeId)
                ? null
                : Guid.Parse(setInstitute.NewInstitute.InstituteTypeId);

            institute = new Institute
            {
                Id = Guid.NewGuid(),
                InstituteName = setInstitute.NewInstitute!.InstituteName,
                InstituteCountry = setInstitute.NewInstitute!.InstituteCountry ?? string.Empty,
                InstituteAddress = setInstitute.NewInstitute!.InstituteAddress ?? string.Empty,
                InstitutePhoneNumber = setInstitute.NewInstitute!.InstitutePhoneNumber ?? string.Empty,
                InstituteTypeId = instituteTypeId ?? Guid.Empty,
                Active = true,
                CreatedAt = DateTime.UtcNow
            };

            _context.Institutes.Add(institute);
            await _context.SaveChangesAsync();
        }
        else
        {
            // Select existing institute - verify it exists and is active
            if (string.IsNullOrEmpty(setInstitute.InstituteId))
            {
                return BadRequest(new App.Dto.v1.Message("Institute ID is required when selecting existing institute"));
            }

            if (!Guid.TryParse(setInstitute.InstituteId, out var instituteGuid))
            {
                return BadRequest(new App.Dto.v1.Message("Invalid institute ID format"));
            }
            
            institute = await _context.Institutes
                .Where(i => i.Id == instituteGuid && i.Active)
                .FirstOrDefaultAsync();
            
            if (institute == null)
            {
                return BadRequest(new App.Dto.v1.Message("Institute not found or inactive"));
            }
        }

        // Check if user already has an InstituteUser record for this institute
        var existingLink = await _context.InstituteUsers
            .Where(iu => iu.UserId == userId && iu.InstituteId == institute.Id)
            .FirstOrDefaultAsync();

        if (existingLink == null)
        {
            // Create new InstituteUser link
            var instituteUser = new InstituteUser
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                InstituteId = institute.Id,
                Role = EInstituteUserRole.Employee
            };

            _context.InstituteUsers.Add(instituteUser);
            await _context.SaveChangesAsync();
        }

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