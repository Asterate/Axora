using App.Modules.Equipment.Application.Interfaces;
using App.Modules.Equipment.Domain;

namespace App.Modules.Equipment.Application.Service;

public class EquipmentTypeService
{
    private readonly IEquipmentTypeRepository _equipmentTypeRepo;

    public EquipmentTypeService(
        IEquipmentTypeRepository equipmentTypeRepo)
    {
        _equipmentTypeRepo = equipmentTypeRepo;
    }

    public async Task<IEnumerable<EquipmentTypeListResponse>> GetAllAsync()
    {
        var equipment = await _equipmentTypeRepo.GetAllAsync();
        return equipment.Select(e => new EquipmentTypeListResponse
        {
            Id = e.Id,
            Name = e.Name.ToString(),
        });
    }

    public async Task<EquipmentTypeResponse?> EquipmentResponse(Guid id)
    {
        var equipment = await _equipmentTypeRepo.GetByIdAsync(id);
        if (equipment == null) return null;

        return new EquipmentTypeResponse
        {
            Id = equipment.Id,
            Name = equipment.Name.ToString()
        };
    }

    public async Task CreateAsync(CreateEquipmentTypeRequest dto)
    {
        var entity = new EquipmentType
        {
            Name = new Shared.Domain.LangStr { ["en"] = dto.Name ?? "" },
        };
        await _equipmentTypeRepo.AddAsync(entity);
    }

    public async Task UpdateAsync(UpdateEquipmentTypeRequest dto)
    {
        var entity = await _equipmentTypeRepo.GetByIdAsync(dto.Id);
        if (entity == null) return;

        entity.Name = new Shared.Domain.LangStr { ["en"] = dto.Name ?? "" };

        _equipmentTypeRepo.Update(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _equipmentTypeRepo.GetByIdAsync(id);
        if (entity == null) return;
        _equipmentTypeRepo.Delete(entity);
    }
    
    
}