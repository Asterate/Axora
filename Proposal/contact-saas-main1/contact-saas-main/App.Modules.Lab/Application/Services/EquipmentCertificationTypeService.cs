using App.Modules.Equipment.Application.Interfaces;
using App.Modules.Lab.Application.DTO;
using App.Modules.Lab.Application.Interfaces;
using App.Modules.Lab.Application.Mappers;
using App.Shared.Contracts;

namespace App.Modules.Lab.Application.Services;

public class EquipmentCertificationTypeService : IEquipmentCertificationService
{
    private readonly IEquipmentCertificationTypeRepository _equipmentCertificationTypeRepo;
    private readonly IUnitOfWork _uow;

    public EquipmentCertificationTypeService(
        IEquipmentCertificationTypeRepository equipmentCertificationTypeRepo,
        IUnitOfWork uow)
    {
        _equipmentCertificationTypeRepo = equipmentCertificationTypeRepo;
        _uow = uow;
    }
    public async Task<IEnumerable<EquipmentCertificationListResponse>> GetAllAsync()
    {
        var entities = await _equipmentCertificationTypeRepo.GetAllAsync();
        return entities.Select(EquipmentCertificationTypeMapper.ToListResponse);
    }

    public async Task<EquipmentCertificationResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _equipmentCertificationTypeRepo.GetByIdAsync(id);
        if (entity == null) return null;
        return EquipmentCertificationTypeMapper.ToResponse(entity);
    }

    public async Task CreateAsync(CreateEquipmentCertificationRequest request)
    {
        var entity = EquipmentCertificationTypeMapper.ToEntity(request);
        await _equipmentCertificationTypeRepo.AddAsync(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _equipmentCertificationTypeRepo.GetByIdAsync(id);
        if (entity == null) return;
        _equipmentCertificationTypeRepo.Delete(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }
}