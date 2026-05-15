using App.Modules.Lab.Application.DTO;
using App.Modules.Lab.Application.Interfaces;
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

    public async Task CreateAsync(CreateReagentRequest request)
    {
        var entity = ReagentMapper.ToEntity(request);
        await _reagent.AddAsync(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task UpdateAsync(Guid id, UpdateReagentRequest request)
    {
        var entity = await _reagent.GetByIdAsync(id);
        if (entity == null) return;
        ReagentMapper.UpdateEntity(entity, request);
        _reagent.Update(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _reagent.GetByIdAsync(id);
        if (entity == null) return;
        _reagent.Delete(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }
}