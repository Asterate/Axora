using App.Modules.Identity.Application.DTO;
using App.Modules.Identity.Application.Interfaces;
using App.Modules.Identity.Application.Mappers;
using App.Modules.Identity.Applications.Interfaces;
using App.Shared.Contracts;

namespace App.Modules.Identity.Application.Services;

public class InstituteUserService : IInstituteUserService
{
    private readonly IInstituteUserRepository _instituteUserRepo;
    private readonly IUnitOfWork _uow;

    public InstituteUserService(
        IInstituteUserRepository instituteUserRepo,
        IUnitOfWork uow)
    {
        _instituteUserRepo = instituteUserRepo;
        _uow = uow;
    }
    public async Task<IEnumerable<InstituteUserListResponse>> GetAllAsync()
    {
        var entities = await _instituteUserRepo.GetAllAsync();
        return entities.Select(InstituteUserMapper.ToListResponse);
    }

    public async Task<InstituteUserResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _instituteUserRepo.GetByIdAsync(id);
        if (entity == null) return null;
        return InstituteUserMapper.ToResponse(entity);
    }

    public async Task CreateAsync(SaveInstituteUserRequest request)
    {
        var entity = InstituteUserMapper.ToEntity(request);
        await _instituteUserRepo.AddAsync(entity);
        request.CreatedAt = DateTime.UtcNow;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task UpdateAsync(Guid id, SaveInstituteUserRequest request)
    {
        var entity = await _instituteUserRepo.GetByIdAsync(id);
        if (entity == null) return;
        InstituteUserMapper.UpdateEntity(entity, request);
        _instituteUserRepo.Update(entity);
        entity.UpdatedAt = DateTime.UtcNow;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _instituteUserRepo.GetByIdAsync(id);
        if (entity == null) return;
    
        entity.DeletedAt = DateTime.UtcNow;
    
        _instituteUserRepo.Update(entity);
        await _uow.SaveChangesAsync();
    }
    public async Task<bool> HasInstituteAsync(Guid userId)
    {
        var entities = await _instituteUserRepo.GetAllAsync();
        return entities.Any(iu => iu.UserId == userId);
    }
    
}