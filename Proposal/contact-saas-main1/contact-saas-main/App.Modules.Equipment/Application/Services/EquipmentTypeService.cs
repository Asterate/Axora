using App.Modules.Equipment.Application.Interfaces;
using App.Modules.Equipment.Domain;
using Modules.Equipment.Application.DTO;

namespace App.Modules.Equipment.Application.Service;

public class EquipmentTypeService
{
    private readonly IEquipmentRepository _equipmentRepo;
    private readonly IEquipmentTypeRepository _equipmentTypeRepo;

    public EquipmentTypeService(
        IEquipmentRepository equipmentRepo,
        IEquipmentTypeRepository equipmentTypeRepo)
    {
        _equipmentRepo = equipmentRepo;
        _equipmentTypeRepo = equipmentTypeRepo;
    }

    public async Task<IEnumerable<EquipmentDto>> GetAllAsync()
    {
        var equipmentType = await _equipmentTypeRepo.GetAllAsync();
        return equipmentType.Select(e => new EquipmentDto
        {
            Id = e.Id,
            Name = e.Name.ToString(),
            Description = e.Description?.ToString()
            
        });
    }

    public async Task<EquipmentDto?> GetByIdAsync(Guid id)
    {
        var equipmentType = await _equipmentTypeRepo.GetByIdAsync(id);
        if (equipmentType == null) return null;

        return new EquipmentDto
        {
            Id = equipmentType.Id,
            Name = equipmentType.Name.ToString(),
            Description = equipmentType.Description?.ToString()
        };
    }

    public async Task CreateAsync(EquipmentDto dto)
    {
        var entity = new EquipmentType
        {
            Name = new Shared.Domain.LangStr { ["en"] = dto.Name ?? "" },
            Description = dto.Description?.ToString() ?? string.Empty
            
        };
        await _equipmentTypeRepo.AddAsync(entity);
    }

    public async Task UpdateAsync(EquipmentDto dto)
    {
        var entity = await _equipmentTypeRepo.GetByIdAsync(dto.Id);
        if (entity == null) return;

        entity.Name = new Shared.Domain.LangStr { ["en"] = dto.Name ?? "" };
        entity.Description = dto.Description?.ToString() ?? string.Empty;

        _equipmentTypeRepo.Update(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _equipmentTypeRepo.GetByIdAsync(id);
        if (entity == null) return;
        _equipmentTypeRepo.Delete(entity);
    }
    
    
}