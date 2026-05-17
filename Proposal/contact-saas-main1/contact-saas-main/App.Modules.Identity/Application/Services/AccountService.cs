using System.Security.Claims;
using App.Domain.Identity;
using App.Modules.Identity.Application.DTO;
using App.Modules.Identity.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace App.Modules.Identity.Application.Services;

public class AccountService : IAccountService
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ILogger<AccountService> _logger;
    private readonly IAppRefreshTokenService _refreshTokenService;

    private const string SettingsJwtPrefix = "JWT";
    private const string SettingsJwtRefreshTokenExpiresInSeconds = SettingsJwtPrefix + ":RefreshTokenExpiresInSeconds";

    public AccountService(
        IConfiguration configuration,
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        ILogger<AccountService> logger,
        IAppRefreshTokenService refreshTokenService)
    {
        _configuration = configuration;
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
        _refreshTokenService = refreshTokenService;
    }

    public async Task<LoginResult> Login(
        Login loginInfo,
        int? jwtExpiresInSeconds,
        int? refreshTokenExpiresInSeconds)
    {
        var appUser = await _userManager.FindByEmailAsync(loginInfo.Email);
        if (appUser == null)
        {
            return new LoginResult
            {
                Success = false,
                Error = "Invalid email or password"
            };
        }

        var result = await _signInManager.CheckPasswordSignInAsync(appUser, loginInfo.Password, false);
        if (!result.Succeeded)
        {
            return new LoginResult
            {
                Success = false,
                Error = "Invalid email or password"
            };
        }

        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
        await _refreshTokenService.DeleteExpiredByUserIdAsync(appUser.Id);

        var tokenResponse = await _refreshTokenService.CreateAsync(new CreateAppRefreshTokenRequest
        {
            UserId = appUser.Id,
            ExpiresAt = GetExpirationDateTime(refreshTokenExpiresInSeconds, SettingsJwtRefreshTokenExpiresInSeconds)
        });

        return new LoginResult
        {
            Success = true,
            ClaimsPrincipal = claimsPrincipal,
            RefreshToken = tokenResponse.RefreshToken
        };
    }

    public async Task<RegisterUserResult> RegisterUserAsync(
        Register registerModel,
        int? refreshTokenExpiresInSeconds)
    {
        var appUser = await _userManager.FindByEmailAsync(registerModel.Email);
        if (appUser != null)
        {
            _logger.LogWarning("User {Email} already registered", registerModel.Email);
            return new RegisterUserResult
            {
                Success = false,
                Error = "User already registered"
            };
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
            return new RegisterUserResult
            {
                Success = false,
                Error = string.Join("; ", errors)
            };
        }

        _logger.LogInformation("User {Email} created a new account", appUser.Email);

        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);

        var tokenResponse = await _refreshTokenService.CreateAsync(new CreateAppRefreshTokenRequest
        {
            UserId = appUser.Id,
            ExpiresAt = GetExpirationDateTime(refreshTokenExpiresInSeconds, SettingsJwtRefreshTokenExpiresInSeconds)
        });

        return new RegisterUserResult
        {
            Success = true,
            User = appUser,
            ClaimsPrincipal = claimsPrincipal,
            RefreshToken = tokenResponse.RefreshToken
        };
    }


    public Task<JwtResponse> RenewRefreshTokenAsync(RefreshTokenModel model)
    {
        throw new NotImplementedException();
    }

    public Task LogoutAsync(LogoutInfo model, Guid userId)
    {
        throw new NotImplementedException();
    }
    public DateTime GetExpirationDateTime(int? expiresInSeconds, string settingsKey)
    {
        if (expiresInSeconds <= 0) expiresInSeconds = int.MaxValue;
        expiresInSeconds = expiresInSeconds < _configuration.GetValue<int>(settingsKey)
            ? expiresInSeconds
            : _configuration.GetValue<int>(settingsKey);

        return DateTime.UtcNow.AddSeconds(expiresInSeconds ?? 60);
    }
}