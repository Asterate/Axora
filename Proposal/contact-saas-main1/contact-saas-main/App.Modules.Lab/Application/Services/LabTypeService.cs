using App.Modules.Lab.Application.DTO;
using App.Modules.Lab.Application.Interfaces;
using App.Modules.Lab.Application.Interfaces.Service;
using App.Modules.Lab.Application.Mappers;
using App.Shared.Contracts;

namespace App.Modules.Lab.Application.Services;

public class LabTypeService : ILabTypeService
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
    public async Task<IEnumerable<LabTypeResponse>> GetAllAsync()
    {
        var entities = await _labType.GetAllAsync();
        return entities.Select(LabTypeMapper.ToResponse);
    }

    public async Task<LabTypeResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _labType.GetByIdAsync(id);
        if (entity == null) return null;
        return LabTypeMapper.ToResponse(entity);
    }

    public async Task CreateAsync(SaveLabTypeRequest request)
    {
        var entity = LabTypeMapper.ToEntity(request);
        await _labType.AddAsync(entity);
        entity.CreatedAt = DateTime.UtcNow;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task UpdateAsync(Guid id, SaveLabTypeRequest request)
    {
        var entity = await _labType.GetByIdAsync(id);
        if (entity == null) return;
        LabTypeMapper.UpdateEntity(entity, request);
        _labType.Update(entity);
        entity.UpdatedAt = DateTime.UtcNow;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _labType.GetByIdAsync(id);
        if (entity == null) return;
        _labType.Update(entity);
        entity.DeletedAt = DateTime.UtcNow;
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
                Name = t.Name.Translate() ?? "??"
            }).ToList();
    }
}