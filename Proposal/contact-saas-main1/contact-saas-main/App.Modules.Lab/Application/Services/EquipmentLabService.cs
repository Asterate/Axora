using App.Modules.Lab.Application.DTO;
using App.Modules.Lab.Application.Interfaces;
using App.Modules.Lab.Application.Interfaces.Service;
using App.Modules.Lab.Application.Mappers;
using App.Shared.Contracts;

namespace App.Modules.Lab.Application.Services;

public class EquipmentLabService : IEquipmentLabService
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
    public async Task<IEnumerable<EquipmentLabResponse>> GetAllAsync()
    {
        var entities = await _equipmentLab.GetAllAsync();
        return entities.Select(EquipmentLabMapper.ToResponse);
    }

    public async Task<EquipmentLabResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _equipmentLab.GetByIdAsync(id);
        if (entity == null) return null;
        return EquipmentLabMapper.ToResponse(entity);
    }

    public async Task CreateAsync(SaveEquipmentLabRequest request)
    {
        var entity = EquipmentLabMapper.ToEntity(request);
        await _equipmentLab.AddAsync(entity);
        entity.CreatedAt  = DateTime.UtcNow;
        await _uow.SaveChangesAsync();
    }

    public async Task UpdateAsync(Guid id, SaveEquipmentLabRequest request)
    {
        var entity = await _equipmentLab.GetByIdAsync(id);
        if (entity == null) return;
        EquipmentLabMapper.UpdateEntity(entity, request);
        _equipmentLab.Update(entity);
        entity.UpdatedAt  = DateTime.UtcNow;
        await _uow.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _equipmentLab.GetByIdAsync(id);
        if (entity == null) return;
        _equipmentLab.Update(entity);
        entity.DeletedAt  = DateTime.UtcNow;
        await _uow.SaveChangesAsync();
    }
}