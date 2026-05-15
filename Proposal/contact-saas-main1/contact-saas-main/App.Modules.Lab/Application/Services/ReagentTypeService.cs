using App.Modules.Lab.Application.DTO;
using App.Modules.Lab.Application.Interfaces;
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
    public async Task<IEnumerable<ReagentTypeListResponse>> GetAllAsync()
    {
        var entities = await _reagentType.GetAllAsync();
        return entities.Select(ReagentTypeMapper.ToListResponse);
    }

    public async Task<ReagentTypeResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _reagentType.GetByIdAsync(id);
        if (entity == null) return null;
        return ReagentTypeMapper.ToResponse(entity);
    }

    public async Task CreateAsync(CreateReagentTypeRequest request)
    {
        var entity = ReagentTypeMapper.ToEntity(request);
        await _reagentType.AddAsync(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task UpdateAsync(Guid id, UpdateReagentTypeRequest request)
    {
        var entity = await _reagentType.GetByIdAsync(id);
        if (entity == null) return;
        ReagentTypeMapper.UpdateEntity(entity, request);
        _reagentType.Update(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _reagentType.GetByIdAsync(id);
        if (entity == null) return;
        _reagentType.Delete(entity);
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
                Name = t.GetName(culture) ?? "???"
            }).ToList();
    }
}