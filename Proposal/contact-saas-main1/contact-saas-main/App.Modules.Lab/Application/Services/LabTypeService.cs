using App.Modules.Lab.Application.Interfaces;
using App.Modules.Lab.Application.Mapper;
using App.Shared.Contracts;

public class LabTypeService
{
    private readonly ILabTypeRepository _labType;
    private readonly IUnitOfWork _uow;

    public LabTypeService(
        ILabTypeRepository labTypeRepo,
        IUnitOfWork uow)
    {
        _labType = labTypeRepo;
        _uow = uow;
    }
    public async Task<IEnumerable<LabTypeListResponse>> GetAllAsync()
    {
        var entities = await _labType.GetAllAsync();
        return entities.Select(LabTypeMapper.ToListTypeResponse);
    }

    public async Task<LabTypeResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _labType.GetByIdAsync(id);
        if (entity == null) return null;
        return LabTypeMapper.ToResponse(entity);
    }

    public async Task CreateAsync(CreateLabTypeRequest request)
    {
        var entity = LabTypeMapper.ToEntity(request);
        await _labType.AddAsync(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task UpdateAsync(Guid id, UpdateLabTypeRequest request)
    {
        var entity = await _labType.GetByIdAsync(id);
        if (entity == null) return;
        LabTypeMapper.UpdateEntity(entity, request);
        _labType.Update(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _labType.GetByIdAsync(id);
        if (entity == null) return;
        _labType.Delete(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }
    // ExperimentTypeService
    public async Task<List<LookupItem>> GetActivesAsync(string? culture = null)
    {
        var entities = await _labType.GetAllAsync();
        return entities
            .Where(t => t.DeletedAt == null)
            .Select(t => new LookupItem
            {
                Id = t.Id,
                Name = t.Name?.Translate(culture) ?? "???"
            }).ToList();
    }
}