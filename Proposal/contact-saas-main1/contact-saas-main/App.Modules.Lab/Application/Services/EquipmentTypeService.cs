using App.Modules.Equipment.Application.Interfaces;
using App.Modules.Equipment.Application.Mapper;
using App.Modules.Lab.Application.DTO;
using App.Modules.Lab.Application.Interfaces;
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
    public async Task<IEnumerable<EquipmentTypeListResponse>> GetAllAsync()
    {
        var entities = await _equipmentTypeRepo.GetAllAsync();
        return entities.Select(EquipmentTypeMapper.ToListResponse);
    }

    public async Task<EquipmentTypeResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _equipmentTypeRepo.GetByIdAsync(id);
        if (entity == null) return null;
        return EquipmentTypeMapper.ToResponse(entity);
    }

    public async Task CreateAsync(CreateEquipmentTypeRequest request)
    {
        var entity = EquipmentTypeMapper.ToEntity(request);
        await _equipmentTypeRepo.AddAsync(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task UpdateAsync(Guid id, UpdateEquipmentTypeRequest request)
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
        _equipmentTypeRepo.Delete(entity);
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
                Name = t.GetName(culture) ?? "???"
            }).ToList();
    }
}