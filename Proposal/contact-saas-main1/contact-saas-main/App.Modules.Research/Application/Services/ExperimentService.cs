using App.Modules.Experiment.Application.Interfaces;
using App.Modules.Project.Application.DTO;
using App.Modules.Project.Application.Interfaces.Service;
using App.Modules.Project.Application.Mappers;
using App.Shared.Contracts;

namespace App.Modules.Project.Application.Services;

public class ExperimentService : IExperimentService
{
    private readonly IExperimentRepository _experimentRepo;
    private readonly IUnitOfWork _uow;

    public ExperimentService(
        IExperimentRepository experimentRepo,
        IUnitOfWork uow)
    {
        _experimentRepo = experimentRepo;
        _uow = uow;
    }
    public async Task<IEnumerable<ExperimentResponse>> GetAllAsync()
    {
        var entities = await _experimentRepo.GetAllAsync();
        return entities.Select(ExperimentMapper.ToResponse);
    }

    public async Task<ExperimentResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _experimentRepo.GetByIdAsync(id);
        if (entity == null) return null;
        return ExperimentMapper.ToResponse(entity);
    }

    public async Task CreateAsync(CreateExperimentRequest request)
    {
        var entity = ExperimentMapper.ToEntity(request);
        await _experimentRepo.AddAsync(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

  public async Task UpdateAsync(Guid id, UpdateExperimentRequest request)
    {
        var entity = await _experimentRepo.GetByIdAsync(id);
        if (entity == null) return;
        ExperimentMapper.UpdateEntity(entity, request);
        _experimentRepo.Update(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _experimentRepo.GetByIdAsync(id);
        if (entity == null) return;
        _experimentRepo.Delete(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }
}