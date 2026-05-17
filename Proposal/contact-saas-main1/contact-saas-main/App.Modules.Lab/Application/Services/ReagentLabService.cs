using App.Modules.Lab.Application.DTO;
using App.Modules.Lab.Application.Interfaces;
using App.Modules.Lab.Application.Interfaces.Service;
using App.Modules.Lab.Application.Mappers;
using App.Shared.Contracts;

namespace App.Modules.Lab.Application.Services;

public class ReagentLabService :  IReagentLabService
{
    private readonly IReagentLabRepository _reagentLab;
    private readonly IUnitOfWork _uow;

    public ReagentLabService(
        IReagentLabRepository reagentLabRepo,
        IUnitOfWork uow)
    {
        _reagentLab = reagentLabRepo;
        _uow = uow;
    }
    public async Task<IEnumerable<ReagentLabResponse>> GetAllAsync()
    {
        var entities = await _reagentLab.GetAllAsync();
        return entities.Select(ReagentLabMapper.ToReagentLabResponse);
    }

    public async Task<ReagentLabResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _reagentLab.GetByIdAsync(id);
        if (entity == null) return null;
        return ReagentLabMapper.ToReagentLabResponse(entity);
    }

    public async Task CreateAsync(SaveReagentLabRequest request)
    {
        var entity = ReagentLabMapper.ToEntity(request);
        await _reagentLab.AddAsync(entity);
        entity.CreatedAt = DateTime.UtcNow;
        await _uow.SaveChangesAsync();
    }

    public async Task UpdateAsync(Guid id, SaveReagentLabRequest request)
    {
        var entity = await _reagentLab.GetByIdAsync(id);
        if (entity == null) return;
        ReagentLabMapper.UpdateEntity(entity, request);
        _reagentLab.Update(entity);
        entity.UpdatedAt = DateTime.UtcNow;
        await _uow.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _reagentLab.GetByIdAsync(id);
        if (entity == null) return;
        _reagentLab.Update(entity);
        entity.DeletedAt = DateTime.UtcNow;
        await _uow.SaveChangesAsync();
    }
}