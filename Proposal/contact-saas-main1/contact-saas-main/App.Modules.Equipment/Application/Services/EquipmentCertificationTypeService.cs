using App.Modules.Equipment.Application.Interfaces;
using App.Modules.Equipment.Domain;

namespace App.Modules.Equipment.Application.Service;

public class EquipmentCertificationTypeService
{
    private readonly IEquipmentCertificationTypeRepository  _equipmentCertificationTypeRepo;

    public EquipmentCertificationTypeService(
        IEquipmentCertificationTypeRepository equipmentCertificationTypeRepo)
    {
        _equipmentCertificationTypeRepo = equipmentCertificationTypeRepo;
    }

    public async Task<IEnumerable<EquipmentCertificationTypeListResponse>> GetAllAsync()
    {
        var equipment = await  _equipmentCertificationTypeRepo.GetAllAsync();
        return equipment.Select(e => new EquipmentCertificationTypeListResponse
        {
            Id = e.Id,
        });
    }

    public async Task<EquipmentCertificationTypeResponse?> EquipmentCertificationTypeResponse(Guid id)
    {
        var equipment = await  _equipmentCertificationTypeRepo.GetByIdAsync(id);
        if (equipment == null) return null;

        return new EquipmentCertificationTypeResponse
        {
            Id = equipment.Id,
        };
    }

    public async Task CreateAsync(CreateEquipmentCertificationTypeRequest dto)
    {
        var entity = new EquipmentCertificationType
        {
            Id = dto.Id,
        };
        await  _equipmentCertificationTypeRepo.AddAsync(entity);
    }

    public async Task UpdateAsync(UpdateEquipmentCertificationTypeRequest dto)
    {
        var entity = await  _equipmentCertificationTypeRepo.GetByIdAsync(dto.Id);
        if (entity == null) return;

        entity.Id = entity.Id;

        _equipmentCertificationTypeRepo.Update(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await  _equipmentCertificationTypeRepo.GetByIdAsync(id);
        if (entity == null) return;
        _equipmentCertificationTypeRepo.Delete(entity);
    }
    
    
}