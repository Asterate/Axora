using App.Modules.Lab.Application.DTO;
using App.Modules.Lab.Application.Interfaces;
using App.Modules.Lab.Application.Interfaces.Service;
using App.Modules.Lab.Application.Mappers;
using App.Modules.Reagent.Application.Interfaces;
using App.Shared.Contracts;

namespace App.Modules.Lab.Application.Services;

public class ReagentService : IReagentService
{
    private readonly IReagentRepository _reagent;
    private readonly IUnitOfWork _uow;

    public ReagentService(
        IReagentRepository reagent, 
        IUnitOfWork uow)
    {
        _reagent = reagent;
        _uow = uow;
    }
    public async Task<IEnumerable<ReagentListResponse>> GetAllAsync()
    {
        var entities = await _reagent.GetAllAsync();
        return entities.Select(ReagentMapper.ToListResponse);
    }

    public async Task<ReagentResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _reagent.GetByIdAsync(id);
        if (entity == null) return null;
        return ReagentMapper.ToResponse(entity);
    }

    public async Task CreateAsync(SaveReagentRequest request)
    {
        var entity = ReagentMapper.ToEntity(request);
        await _reagent.AddAsync(entity);
        entity.CreatedAt = DateTime.UtcNow;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task UpdateAsync(Guid id, SaveReagentRequest request)
    {
        var entity = await _reagent.GetByIdAsync(id);
        if (entity == null) return;
        ReagentMapper.UpdateEntity(entity, request);
        _reagent.Update(entity);
        entity.UpdatedAt = DateTime.UtcNow;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _reagent.GetByIdAsync(id);
        if (entity == null) return;
        _reagent.Update(entity);
        entity.DeletedAt = DateTime.UtcNow;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }
}