using App.Modules.Experiment.Application.Interfaces;
using App.Modules.Experiment.Application.Mapper;
using App.Shared.Contracts;

public class ExperimentService
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
    public async Task<IEnumerable<ExperimentListResponse>> GetAllAsync()
    {
        var entities = await _experimentRepo.GetAllAsync();
        return entities.Select(ExperimentMapper.ToListResponse);
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