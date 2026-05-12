using App.Domain.Entities;
using App.Modules.Experiment.Application.Interfaces;
using App.Modules.Experiment.Application.Mapper;
using App.Shared.Contracts;

public class ExperimentTaskService
{
    private readonly IExperimentTaskRepository _experimentTaskRepo;
    private readonly IUnitOfWork _uow;

    public ExperimentTaskService(
        IExperimentTaskRepository experimentTaskRepo,
        IUnitOfWork uow)
    {
        _experimentTaskRepo = experimentTaskRepo;
        _uow = uow;
    }
    public async Task<IEnumerable<ExperimentTaskListResponse>> GetAllAsync()
    {
        var entities = await _experimentTaskRepo.GetAllAsync();
        return entities.Select(ExperimentTaskMapper.ToListResponse);
    }
    public async Task<IEnumerable<ExperimentTaskListResponse>> GetAllByExperimentIdsAsync(IEnumerable<Guid> experimentIds)
    {
        var entities = await _experimentTaskRepo.GetAllAsync();
        return entities
            .Where(t => experimentIds.Contains(t.ExperimentId) && t.DeletedAt == null)
            .Select(ExperimentTaskMapper.ToListResponse);
    }
    public async Task CreateAsync(CreateExperimentTaskRequest request)
    {
        var entity = ExperimentTaskMapper.ToEntity(request);
        await _experimentTaskRepo.AddAsync(entity);
        await _uow.SaveChangesAsync();
    }

    public async Task<ExperimentTaskResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _experimentTaskRepo.GetByIdAsync(id);
        if (entity == null || entity.DeletedAt != null) return null;
        return ExperimentTaskMapper.ToResponse(entity);
    }

    public async Task<ExperimentTask> CreateAndReturnAsync(CreateExperimentTaskRequest request)
    {
        var entity = ExperimentTaskMapper.ToEntity(request);
        await _experimentTaskRepo.AddAsync(entity);
        await _uow.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(Guid id, UpdateExperimentTaskRequest request)
    {
        var entity = await _experimentTaskRepo.GetByIdAsync(id);
        if (entity == null) return;
        ExperimentTaskMapper.UpdateEntity(entity, request);
        _experimentTaskRepo.Update(entity);
        await _uow.SaveChangesAsync();
    }

    public async Task SoftDeleteAsync(Guid id)
    {
        var entity = await _experimentTaskRepo.GetByIdAsync(id);
        if (entity == null) return;
        entity.DeletedAt = DateTime.UtcNow;
        _experimentTaskRepo.Update(entity);
        await _uow.SaveChangesAsync();
    }
    public async Task<List<LookupItem>> GetActivesAsync(string? culture = null)
    {
        var entities = await _experimentTaskRepo.GetAllAsync();
        return entities
            .Where(t => t.DeletedAt == null)
            .Select(t => new LookupItem
            {
                Id = t.Id,
                Name = t.TaskName?.Translate(culture) ?? "???"
            }).ToList();
    }
    public async Task DeleteAsync(Guid id)
    {
        var entity = await _experimentTaskRepo.GetByIdAsync(id);
        if (entity == null) return;
        _experimentTaskRepo.Delete(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }
}