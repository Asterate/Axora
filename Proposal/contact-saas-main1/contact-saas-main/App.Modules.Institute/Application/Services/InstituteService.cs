using App.Modules.Institute.Application.Interfaces;
using App.Modules.Institute.Application.Mapper;
using App.Shared.Contracts;

public class InstituteService
{
    private readonly IInstituteRepository _instituteRepo;
    private readonly IUnitOfWork _uow;

    public InstituteService(
        IInstituteRepository instituteRepo,
        IUnitOfWork uow)
    {
        _instituteRepo = instituteRepo;
        _uow = uow;
    }
    public async Task<IEnumerable<InstituteListResponse>> GetAllAsync()
    {
        var entities = await _instituteRepo.GetAllAsync();
        return entities.Select(InstituteMapper.ToListResponse);
    }

    public async Task<InstituteResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _instituteRepo.GetByIdAsync(id);
        if (entity == null) return null;
        return InstituteMapper.ToResponse(entity);
    }

    public async Task CreateAsync(CreateInstituteRequest request)
    {
        var entity = InstituteMapper.ToEntity(request);
        await _instituteRepo.AddAsync(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task UpdateAsync(Guid id, UpdateInstituteRequest request)
    {
        var entity = await _instituteRepo.GetByIdAsync(id);
        if (entity == null) return;
        InstituteMapper.UpdateEntity(entity, request);
        _instituteRepo.Update(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _instituteRepo.GetByIdAsync(id);
        if (entity == null) return;
        _instituteRepo.Delete(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }
    public async Task<int> CountAsync()
    {
        var all = await _instituteRepo.GetAllAsync();
        return all.Count(i => i.DeletedAt == null);
    }
    public async Task<IEnumerable<InstituteListResponse>> GetRecentAsync(int take)
    {
        var all = await _instituteRepo.GetAllAsync();
        return all
            .Where(i => i.DeletedAt == null)
            .OrderByDescending(i => i.CreatedAt)
            .Take(take)
            .Select(InstituteMapper.ToListResponse);
    }
    public async Task<IEnumerable<InstituteListResponse>> FindDeletedAsync()
    {
        var entities = await _instituteRepo.GetAllAsync();
        return entities
            .Where(s => s.DeletedAt != null)
            .Select(InstituteMapper.ToListResponse);
    }
    public async Task<List<LookupItem>> GetActivesAsync()
    {
        var entities = await _instituteRepo.GetActivesAsync();
        return entities.Select(i => new LookupItem 
        { 
            Id = i.Id, 
            Name = i.InstituteName 
        }).ToList();
    }
}