using App.Modules.Lab.Application.DTO;
using App.Modules.Lab.Application.Interfaces;
using App.Modules.Lab.Application.Interfaces.Service;
using App.Modules.Lab.Application.Mappers;
using App.Modules.Reagent.Application.Interfaces;
using App.Shared.Contracts;

namespace App.Modules.Lab.Application.Services;

public class ReagentTypeService : IReagentTypeService
{
    private readonly IReagentTypeRepository _reagentType;
    private readonly IUnitOfWork _uow;

    public ReagentTypeService(
        IReagentTypeRepository reagentType, 
        IUnitOfWork uow)
    {
        _reagentType = reagentType;
        _uow = uow;
    }
    public async Task<IEnumerable<ReagentTypeResponse>> GetAllAsync()
    {
        var entities = await _reagentType.GetAllAsync();
        return entities.Select(ReagentTypeMapper.ToResponse);
    }

    public async Task<ReagentTypeResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _reagentType.GetByIdAsync(id);
        if (entity == null) return null;
        return ReagentTypeMapper.ToResponse(entity);
    }

    public async Task CreateAsync(SaveReagentTypeRequest request)
    {
        var entity = ReagentTypeMapper.ToEntity(request);
        await _reagentType.AddAsync(entity);
        entity.CreatedAt = DateTime.UtcNow;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task UpdateAsync(Guid id, SaveReagentTypeRequest request)
    {
        var entity = await _reagentType.GetByIdAsync(id);
        if (entity == null) return;
        ReagentTypeMapper.UpdateEntity(entity, request);
        _reagentType.Update(entity);
        entity.UpdatedAt = DateTime.UtcNow;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _reagentType.GetByIdAsync(id);
        if (entity == null) return;
        _reagentType.Update(entity);
        entity.DeletedAt = DateTime.UtcNow;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }
    public async Task<List<LookupItem>> GetActivesAsync(string? culture = null)
    {
        var entities = await _reagentType.GetAllAsync();
        return entities
            .Where(t => t.DeletedAt == null)
            .Select(t => new LookupItem
            {
                Id = t.Id,
                Name = t.Name.Translate() ?? "??",
            }).ToList();
    }
}