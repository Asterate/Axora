using App.Modules.Equipment.Application.Interfaces;
using Modules.Equipment.Application.DTO;

namespace App.Modules.Equipment.Application.Service;

public class EquipmentService
{
    private readonly IEquipmentRepository _equipmentRepo;
    private readonly IEquipmentTypeRepository _equipmentTypeRepo;

    public EquipmentService(
        IEquipmentRepository equipmentRepo,
        IEquipmentTypeRepository equipmentTypeRepo)
    {
        _equipmentRepo = equipmentRepo;
        _equipmentTypeRepo = equipmentTypeRepo;
    }

    public async Task<IEnumerable<EquipmentDto>> GetAllAsync()
    {
        var equipment = await _equipmentRepo.GetAllAsync();
        return equipment.Select(e => new EquipmentDto
        {
            Id = e.Id,
            Name = e.EquipmentName.ToString(),
            EquipmentSerialCode = e.EquipmentSerialCode,
            ManualFilePath = e.ManualFilePath,
            EquipmentTypeId = e.EquipmentTypeId,
        });
    }

    public async Task<EquipmentDto?> GetByIdAsync(Guid id)
    {
        var equipment = await _equipmentRepo.GetByIdAsync(id);
        if (equipment == null) return null;

        return new EquipmentDto
        {
            Id = equipment.Id,
            Name = equipment.EquipmentName.ToString(),
            EquipmentSerialCode = equipment.EquipmentSerialCode,
            ManualFilePath = equipment.ManualFilePath,
            EquipmentTypeId = equipment.EquipmentTypeId,
        };
    }

    public async Task CreateAsync(EquipmentDto dto)
    {
        var entity = new Domain.Equipment
        {
            EquipmentName = new Shared.Domain.LangStr { ["en"] = dto.Name ?? "" },
            EquipmentSerialCode = dto.EquipmentSerialCode,
            ManualFilePath = dto.ManualFilePath,
            EquipmentTypeId = dto.EquipmentTypeId,
        };
        await _equipmentRepo.AddAsync(entity);
    }

    public async Task UpdateAsync(EquipmentDto dto)
    {
        var entity = await _equipmentRepo.GetByIdAsync(dto.Id);
        if (entity == null) return;

        entity.EquipmentName = new Shared.Domain.LangStr { ["en"] = dto.Name ?? "" };
        entity.EquipmentSerialCode = dto.EquipmentSerialCode;
        entity.ManualFilePath = dto.ManualFilePath;
        entity.EquipmentTypeId = dto.EquipmentTypeId;

        _equipmentRepo.Update(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _equipmentRepo.GetByIdAsync(id);
        if (entity == null) return;
        _equipmentRepo.Delete(entity);
    }
    
    
}