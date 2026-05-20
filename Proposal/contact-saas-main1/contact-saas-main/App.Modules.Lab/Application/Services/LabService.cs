using App.Modules.Lab.Application.DTO;
using App.Modules.Lab.Application.Interfaces;
using App.Modules.Lab.Application.Interfaces.Service;
using App.Modules.Lab.Application.Mappers;
using App.Shared.Contracts;

namespace App.Modules.Lab.Application.Services;

public class LabService : ILabService
{
    private readonly ILabRepository _lab;
    private readonly IUnitOfWork _uow;

    public LabService(
        ILabRepository labRepo,
        IUnitOfWork uow)
    {
        _lab = labRepo;
        _uow = uow;
    }
    public async Task<IEnumerable<LabResponse>> GetAllAsync()
    {
        var entities = await _lab.GetAllAsync();
        return entities.Select(LabMapper.ToResponse);
    }

    public async Task<LabResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _lab.GetByIdAsync(id);
        if (entity == null) return null;
        return LabMapper.ToResponse(entity);
    }

    public async Task CreateAsync(SaveLabRequest request)
    {
        var entity = LabMapper.ToEntity(request);
        await _lab.AddAsync(entity);
        entity.CreatedAt = DateTime.UtcNow;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task UpdateAsync(Guid id, SaveLabRequest request)
    {
        var entity = await _lab.GetByIdAsync(id);
        if (entity == null) return;
        LabMapper.UpdateEntity(entity, request);
        _lab.Update(entity);
        entity.UpdatedAt =  DateTime.UtcNow;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _lab.GetByIdAsync(id);
        if (entity == null) return;
        _lab.Update(entity);
        entity.DeletedAt =  DateTime.UtcNow;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task<int> CountAsync()
    {
        var all = await _lab.GetAllAsync();
        return all.Count(i => i.DeletedAt == null);
    }
    public async Task<List<LookupItem>> GetActivesAsync(string? culture = null)
    {
        var entities = await _lab.GetAllAsync();
        return entities
            .Where(t => t.DeletedAt == null)
            .Select(t => new LookupItem
            {
                Id = t.Id,
                Name = t.LabName.Translate(culture) ?? String.Empty,
            }).ToList();
    }
    
}