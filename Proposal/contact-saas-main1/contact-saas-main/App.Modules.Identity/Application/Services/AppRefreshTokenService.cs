using App.Modules.Equipment.Application.Mapper;
using App.Modules.Identity.Applications.Interfaces;
using App.Shared.Contracts;

public class AppRefreshTokenService
{
    private readonly IAppRefreshTokenRepository _appRefreshToken;
    private readonly IUnitOfWork _uow;

    public AppRefreshTokenService(
        IAppRefreshTokenRepository appRefreshTokenRepo,
        IUnitOfWork uow)
    {
        _appRefreshToken = appRefreshTokenRepo;
        _uow = uow;
    }
    public async Task<IEnumerable<AppRefreshTokenListResponse>> GetAllAsync()
    {
        var entities = await _appRefreshToken.GetAllAsync();
        return entities.Select(AppRefreshTokenMapper.ToListResponse);
    }

    public async Task<AppRefreshTokenResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _appRefreshToken.GetByIdAsync(id);
        if (entity == null) return null;
        return AppRefreshTokenMapper.ToResponse(entity);
    }

    public async Task CreateAsync(CreateAppRefreshTokenRequest request)
    {
        var entity = AppRefreshTokenMapper.ToEntity(request);
        await _appRefreshToken.AddAsync(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task UpdateAsync(Guid id, UpdateAppRefreshTokenRequest request)
    {
        var entity = await _appRefreshToken.GetByIdAsync(id);
        if (entity == null) return;
        AppRefreshTokenMapper.UpdateEntity(entity, request);
        _appRefreshToken.Update(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _appRefreshToken.GetByIdAsync(id);
        if (entity == null) return;
        _appRefreshToken.Delete(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }
}