using App.DTO.v1.Identity;
using App.Modules.Identity.Application.DTO;

namespace App.Modules.Identity.Application.Interfaces;

public interface IAccountService
{

    Task<LoginResult> Login(
        Login loginInfo,
        int? jwtExpiresInSeconds,
        int? refreshTokenExpiresInSeconds
    );
    Task<RegisterUserResult> RegisterUserAsync(Register registerModel,
        int? refreshTokenExpiresInSeconds);
    Task<JWTResponse> RenewRefreshTokenAsync(RefreshTokenModel model);
    Task LogoutAsync(LogoutInfo model, Guid userId);
}