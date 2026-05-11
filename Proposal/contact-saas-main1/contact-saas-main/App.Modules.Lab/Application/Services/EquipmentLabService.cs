using App.Modules.Lab.Application.Interfaces;
using App.Modules.Lab.Application.Mapper;
using App.Shared.Contracts;

public class EquipmentLabService
{
    private readonly IEquipmentLabRepository _equipmentLab;
    private readonly IUnitOfWork _uow;

    public EquipmentLabService(
        IEquipmentLabRepository equipmentLabRepo, 
        IUnitOfWork uow)
    {
        _equipmentLab = equipmentLabRepo;
        _uow = uow;
    }
    public async Task<IEnumerable<EquipmentLabListResponse>> GetAllAsync()
    {
        var entities = await _equipmentLab.GetAllAsync();
        return entities.Select(EquipmentLabMapper.ToEquipmentLabResponse);
    }

    public async Task<EquipmentLabResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _equipmentLab.GetByIdAsync(id);
        if (entity == null) return null;
        return EquipmentLabMapper.ToResponse(entity);
    }

    public async Task CreateAsync(CreateEquipmentLabRequest request)
    {
        var entity = EquipmentLabMapper.ToEntity(request);
        await _equipmentLab.AddAsync(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task UpdateAsync(Guid id, UpdateEquipmentLabRequest request)
    {
        var entity = await _equipmentLab.GetByIdAsync(id);
        if (entity == null) return;
        EquipmentLabMapper.UpdateEntity(entity, request);
        _equipmentLab.Update(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _equipmentLab.GetByIdAsync(id);
        if (entity == null) return;
        _equipmentLab.Delete(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }
}