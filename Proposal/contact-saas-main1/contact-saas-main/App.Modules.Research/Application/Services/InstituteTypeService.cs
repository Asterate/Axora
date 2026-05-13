using App.Modules.Institute.Application.Interfaces;
using App.Modules.Institute.Application.Mapper;
using App.Shared.Contracts;

public class InstituteTypeService
{
    private readonly IInstituteTypeRepository _instituteTypeRepo;
    private readonly IUnitOfWork _uow;

    public InstituteTypeService(
        IInstituteTypeRepository instituteTypeRepo,
        IUnitOfWork uow)
    {
        _instituteTypeRepo = instituteTypeRepo;
        _uow = uow;
    }
    public async Task<IEnumerable<InstituteTypeListResponse>> GetAllAsync()
    {
        var entities = await _instituteTypeRepo.GetAllAsync();
        return entities.Select(InstituteTypeMapper.ToListResponse);
    }

    public async Task<InstituteTypeResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _instituteTypeRepo.GetByIdAsync(id);
        if (entity == null) return null;
        return InstituteTypeMapper.ToResponse(entity);
    }

    public async Task CreateAsync(CreateInstituteTypeRequest request)
    {
        var entity = InstituteTypeMapper.ToEntity(request);
        await _instituteTypeRepo.AddAsync(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task UpdateAsync(Guid id, UpdateInstituteTypeRequest request)
    {
        var entity = await _instituteTypeRepo.GetByIdAsync(id);
        if (entity == null) return;
        InstituteTypeMapper.UpdateEntity(entity, request);
        _instituteTypeRepo.Update(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _instituteTypeRepo.GetByIdAsync(id);
        if (entity == null) return;
        _instituteTypeRepo.Delete(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }
    
    public async Task<List<LookupItem>> GetActivesAsync(string? culture = null)
    {
        var entities = await _instituteTypeRepo.GetActivesAsync();
        return entities.Select(i => new LookupItem 
        { 
            Id = i.Id, 
            Name = i.GetInstituteName(culture) ?? "???"
        }).ToList();
    }
}