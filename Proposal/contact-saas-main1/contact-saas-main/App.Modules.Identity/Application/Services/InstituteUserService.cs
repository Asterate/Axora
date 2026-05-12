using App.Modules.Equipment.Application.Mapper;
using App.Modules.Identity.Applications.Interfaces;
using App.Shared.Contracts;

public class InstituteUserService
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

    public async Task CreateAsync(CreateInstituteUserRequest request)
    {
        var entity = InstituteUserMapper.ToEntity(request);
        await _instituteUserRepo.AddAsync(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task UpdateAsync(Guid id, UpdateInstituteUserRequest request)
    {
        var entity = await _instituteUserRepo.GetByIdAsync(id);
        if (entity == null) return;
        InstituteUserMapper.UpdateEntity(entity, request);
        _instituteUserRepo.Update(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _instituteUserRepo.GetByIdAsync(id);
        if (entity == null) return;
        _instituteUserRepo.Delete(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }
    public async Task<bool> HasInstituteAsync(Guid userId)
    {
        var entities = await _instituteUserRepo.GetAllAsync();
        return entities.Any(iu => iu.UserId == userId);
    }
    
}