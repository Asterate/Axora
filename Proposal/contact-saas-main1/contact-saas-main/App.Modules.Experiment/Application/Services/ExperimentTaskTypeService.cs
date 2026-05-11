using App.Modules.Experiment.Application.Interfaces;
using App.Modules.Experiment.Application.Mapper;
using App.Shared.Contracts;

public class ExperimentTaskTypeService
{
    private readonly IExperimentTaskTypeRepository _experimentTaskTypeRepo;
    private readonly IUnitOfWork _uow;

    public ExperimentTaskTypeService(
        IExperimentTaskTypeRepository experimentTaskTypeRepo,
        IUnitOfWork uow)
    {
        _experimentTaskTypeRepo = experimentTaskTypeRepo;
        _uow = uow;
    }
    public async Task<IEnumerable<ExperimentTaskTypeListResponse>> GetAllAsync()
    {
        var entities = await _experimentTaskTypeRepo.GetAllAsync();
        return entities.Select(ExperimentTaskTypeMapper.ToListResponse);
    }

    public async Task<ExperimentTaskTypeResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _experimentTaskTypeRepo.GetByIdAsync(id);
        if (entity == null) return null;
        return ExperimentTaskTypeMapper.ToResponse(entity);
    }

    public async Task CreateAsync(CreateExperimentTaskTypeRequest request)
    {
        var entity = ExperimentTaskTypeMapper.ToEntity(request);
        await _experimentTaskTypeRepo.AddAsync(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task UpdateAsync(Guid id, UpdateExperimentTaskTypeRequest request)
    {
        var entity = await _experimentTaskTypeRepo.GetByIdAsync(id);
        if (entity == null) return;
        ExperimentTaskTypeMapper.UpdateEntity(entity, request);
        _experimentTaskTypeRepo.Update(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _experimentTaskTypeRepo.GetByIdAsync(id);
        if (entity == null) return;
        _experimentTaskTypeRepo.Delete(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }
}