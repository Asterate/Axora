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

    public async Task<ExperimentTaskResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _experimentTaskRepo.GetByIdAsync(id);
        if (entity == null) return null;
        return ExperimentTaskMapper.ToResponse(entity);
    }

    public async Task CreateAsync(CreateExperimentTaskRequest request)
    {
        var entity = ExperimentTaskMapper.ToEntity(request);
        await _experimentTaskRepo.AddAsync(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task UpdateAsync(Guid id, UpdateExperimentTaskRequest request)
    {
        var entity = await _experimentTaskRepo.GetByIdAsync(id);
        if (entity == null) return;
        ExperimentTaskMapper.UpdateEntity(entity, request);
        _experimentTaskRepo.Update(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _experimentTaskRepo.GetByIdAsync(id);
        if (entity == null) return;
        _experimentTaskRepo.Delete(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }
}