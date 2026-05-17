using App.Modules.Equipment.Application.Interfaces;
using App.Modules.Lab.Application.DTO;
using App.Modules.Lab.Application.Interfaces;
using App.Modules.Lab.Application.Interfaces.Service;
using App.Modules.Lab.Application.Mappers;
using App.Shared.Contracts;

namespace App.Modules.Lab.Application.Services;

public class EquipmentTypeService :  IEquipmentTypeService
{
    private readonly IEquipmentTypeRepository _equipmentTypeRepo;
    private readonly IUnitOfWork _uow;

    public EquipmentTypeService(
        IEquipmentTypeRepository equipmentTypeRepo,
        IUnitOfWork uow)
    {
        _equipmentTypeRepo = equipmentTypeRepo;
        _uow = uow;
    }
    public async Task<IEnumerable<EquipmentTypeResponse>> GetAllAsync()
    {
        var entities = await _equipmentTypeRepo.GetAllAsync();
        return entities.Select(EquipmentTypeMapper.ToResponse);
    }

    public async Task<EquipmentTypeResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _equipmentTypeRepo.GetByIdAsync(id);
        if (entity == null) return null;
        return EquipmentTypeMapper.ToResponse(entity);
    }

    public async Task CreateAsync(SaveEquipmentTypeRequest request)
    {
        var entity = EquipmentTypeMapper.ToEntity(request);
        await _equipmentTypeRepo.AddAsync(entity);
        entity.CreatedAt =  DateTime.UtcNow;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task UpdateAsync(Guid id, SaveEquipmentTypeRequest request)
    {
        var entity = await _equipmentTypeRepo.GetByIdAsync(id);
        if (entity == null) return;
        EquipmentTypeMapper.UpdateEntity(entity, request);
        _equipmentTypeRepo.Update(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _equipmentTypeRepo.GetByIdAsync(id);
        if (entity == null) return;
        _equipmentTypeRepo.Update(entity);
        entity.DeletedAt =  DateTime.UtcNow;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }
    public async Task<List<LookupItem>> GetActivesAsync(string? culture = null)
    {
        var entities = await _equipmentTypeRepo.GetAllAsync();
        return entities
            .Where(t => t.DeletedAt == null)
            .Select(t => new LookupItem
            {
                Id = t.Id,
                Name = t.Name.Translate() ?? "??"
            }).ToList();
    }
}