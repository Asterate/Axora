using App.Modules.Equipment.Application.Interfaces;
using App.Modules.Equipment.Application.Mapper;
using App.Shared.Contracts;

public class EquipmentCertificationTypeService
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
    public async Task<IEnumerable<EquipmentCertificationTypeListResponse>> GetAllAsync()
    {
        var entities = await _equipmentCertificationTypeRepo.GetAllAsync();
        return entities.Select(EquipmentCertificationTypeMapper.ToListResponse);
    }

    public async Task<EquipmentCertificationTypeResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _equipmentCertificationTypeRepo.GetByIdAsync(id);
        if (entity == null) return null;
        return EquipmentCertificationTypeMapper.ToResponse(entity);
    }

    public async Task CreateAsync(CreateEquipmentCertificationTypeRequest request)
    {
        var entity = EquipmentCertificationTypeMapper.ToEntity(request);
        await _equipmentCertificationTypeRepo.AddAsync(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task UpdateAsync(Guid id, UpdateEquipmentCertificationTypeRequest request)
    {
        var entity = await _equipmentCertificationTypeRepo.GetByIdAsync(id);
        if (entity == null) return;
        EquipmentCertificationTypeMapper.UpdateEntity(entity, request);
        _equipmentCertificationTypeRepo.Update(entity);
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