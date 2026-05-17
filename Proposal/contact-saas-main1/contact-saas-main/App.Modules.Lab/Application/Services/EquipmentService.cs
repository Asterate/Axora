using App.Modules.Equipment.Application.Interfaces;
using App.Modules.Lab.Application.DTO;
using App.Modules.Lab.Application.Interfaces;
using App.Modules.Lab.Application.Interfaces.Service;
using App.Modules.Lab.Application.Mappers;
using App.Shared.Contracts;

namespace App.Modules.Lab.Application.Services;

public class EquipmentService : IEquipmentService
{
    private readonly IEquipmentRepository _equipmentRepo;
    private readonly IUnitOfWork _uow;

    public EquipmentService(
        IEquipmentRepository equipmentRepo,
        IUnitOfWork uow)
    {
        _equipmentRepo = equipmentRepo;
        _uow = uow;
    }
    public async Task<IEnumerable<EquipmentListResponse>> GetAllAsync()
    {
        var entities = await _equipmentRepo.GetAllAsync();
        return entities.Select(EquipmentMapper.ToListResponse);
    }
    public async Task<EquipmentResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _equipmentRepo.GetByIdAsync(id);
        if (entity == null) return null;
        return EquipmentMapper.ToResponse(entity);
    }

    public async Task CreateAsync(SaveEquipmentRequest request)
    {
        var entity = EquipmentMapper.ToEntity(request);
        await _equipmentRepo.AddAsync(entity);
        entity.CreatedAt  = DateTime.UtcNow;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task UpdateAsync(Guid id, SaveEquipmentRequest request)
    {
        var entity = await _equipmentRepo.GetByIdAsync(id);
        if (entity == null) return;
        EquipmentMapper.UpdateEntity(entity, request);
        _equipmentRepo.Update(entity);
        entity.UpdatedAt  = DateTime.UtcNow;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _equipmentRepo.GetByIdAsync(id);
        if (entity == null) return;
        _equipmentRepo.Update(entity);
        entity.UpdatedAt  = DateTime.UtcNow;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }
    public async Task<List<LookupItem>> GetActivesAsync(string? culture = null)
    {
        var entities = await _equipmentRepo.GetAllAsync();
        return entities
            .Where(t => t.DeletedAt == null)
            .Select(t => new LookupItem
            {
                Id = t.Id,
                Name = t.EquipmentName.Translate(culture) ?? String.Empty,
            }).ToList();
    }
}