using App.Modules.Experiment.Application.Interfaces;
using App.Modules.Project.Application.DTO;
using App.Modules.Project.Application.Interfaces.Service;
using App.Modules.Project.Application.Mappers;
using App.Shared.Contracts;

namespace App.Modules.Project.Application.Services;

public class ExperimentEquipmentService : IExperimentEquipmentService
{
    private readonly IExperimentEquipmentRepository _experimentEquipmentRepo;
    private readonly IUnitOfWork _uow;

    public ExperimentEquipmentService(
        IExperimentEquipmentRepository experimentEquipmentRepo,
        IUnitOfWork uow)
    {
        _experimentEquipmentRepo = experimentEquipmentRepo;
        _uow = uow;
    }
    public async Task<IEnumerable<ExperimentEquipmentResponse>> GetAllAsync()
    {
        var entities = await _experimentEquipmentRepo.GetAllAsync();
        return entities.Select(ExperimentEquipmentMapper.ToResponse);
    }

    public async Task<ExperimentEquipmentResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _experimentEquipmentRepo.GetByIdAsync(id);
        if (entity == null) return null;
        return ExperimentEquipmentMapper.ToResponse(entity);
    }

    public async Task CreateAsync(CreateExperimentEquipmentRequest request)
    {
        var entity = ExperimentEquipmentMapper.ToEntity(request);
        await _experimentEquipmentRepo.AddAsync(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task UpdateAsync(Guid id, UpdateExperimentEquipmentRequest request)
    {
        var entity = await _experimentEquipmentRepo.GetByIdAsync(id);
        if (entity == null) return;
        ExperimentEquipmentMapper.UpdateEntity(entity, request);
        _experimentEquipmentRepo.Update(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _experimentEquipmentRepo.GetByIdAsync(id);
        if (entity == null) return;
        _experimentEquipmentRepo.Delete(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }
}