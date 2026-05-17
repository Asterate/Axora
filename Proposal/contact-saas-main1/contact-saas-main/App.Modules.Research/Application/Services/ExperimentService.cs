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
    public async Task<SaveExperimentRequest?> GetByIdEditAsync(Guid id)
    {
        var entity = await _experimentRepo.GetByIdAsync(id);
        if (entity == null) return null;
        return ExperimentMapper.ToRequest(entity);
    }

    public async Task<ExperimentResponse> CreateAsync(SaveExperimentRequest request)
    {
        var entity = ExperimentMapper.ToEntity(request);
        await _experimentRepo.AddAsync(entity);
        entity.CreatedAt = DateTime.Now;
        await _uow.SaveChangesAsync();

        return ExperimentMapper.ToResponse(entity);
    }

  public async Task UpdateAsync(Guid id, SaveExperimentRequest request)
    {
        var entity = await _experimentRepo.GetByIdAsync(id);
        if (entity == null) return;
        ExperimentMapper.UpdateEntity(entity, request);
        _experimentRepo.Update(entity);
        entity.UpdatedAt = DateTime.Now;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _experimentRepo.GetByIdAsync(id);
        if (entity == null) return;
        _experimentRepo.Update(entity);
        entity.DeletedAt = DateTime.Now;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }
    public async Task<List<LookupItem>> GetActivesAsync(string? culture = null)
    {
        var entities = await _experimentRepo.GetAllAsync();
        return entities
            .Where(t => t.DeletedAt == null)
            .Select(t => new LookupItem
            {
                Id = t.Id,
                Name = t.ExperimentName.Translate(culture) ?? String.Empty,
            }).ToList();
    }
}