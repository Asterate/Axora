using App.Modules.Equipment.Application.Interfaces;
using App.Modules.Lab.Application.DTO;
using App.Modules.Lab.Application.Interfaces;
using App.Modules.Lab.Application.Mappers;
using App.Shared.Contracts;

namespace App.Modules.Lab.Application.Services;

public class EquipmentCertificationService : IEquipmentCertificationService
{
    private readonly IEquipmentCertificationTypeRepository _equipmentCertificationTypeRepo;
    private readonly IUnitOfWork _uow;

    public EquipmentCertificationService(
        IEquipmentCertificationTypeRepository equipmentCertificationTypeRepo,
        IUnitOfWork uow)
    {
        _equipmentCertificationTypeRepo = equipmentCertificationTypeRepo;
        _uow = uow;
    }
    public async Task<IEnumerable<EquipmentCertificationListResponse>> GetAllAsync()
    {
        var entities = await _equipmentCertificationTypeRepo.GetAllAsync();
        return entities.Select(EquipmentCertificationMapper.ToListResponse);
    }

    public async Task<EquipmentCertificationResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _equipmentCertificationTypeRepo.GetByIdAsync(id);
        if (entity == null) return null;
        return EquipmentCertificationMapper.ToResponse(entity);
    }

    public async Task CreateAsync(CreateEquipmentCertificationRequest request)
    {
        var entity = EquipmentCertificationMapper.ToEntity(request);
        await _equipmentCertificationTypeRepo.AddAsync(entity);
        entity.CreatedAt  = DateTime.UtcNow;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _equipmentCertificationTypeRepo.GetByIdAsync(id);
        if (entity == null) return;
        _equipmentCertificationTypeRepo.Update(entity);
        entity.DeletedAt  = DateTime.UtcNow;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }
}