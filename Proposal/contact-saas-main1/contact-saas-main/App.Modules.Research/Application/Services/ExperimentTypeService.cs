using App.Modules.Experiment.Application.Interfaces;
using App.Modules.Experiment.Application.Mapper;
using App.Shared.Contracts;

public class ExperimentTypeService
{
    private readonly IExperimentTypeRepository _experimentTypeRepo;
    private readonly IUnitOfWork _uow;

    public ExperimentTypeService(
        IExperimentTypeRepository experimentTypeRepo,
        IUnitOfWork uow)
    {
        _experimentTypeRepo = experimentTypeRepo;
        _uow = uow;
    }
    public async Task<IEnumerable<ExperimentTypeListResponse>> GetAllAsync()
    {
        var entities = await _experimentTypeRepo.GetAllAsync();
        return entities.Select(ExperimentTypeMapper.ToListResponse);
    }

    public async Task<ExperimentTypeResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _experimentTypeRepo.GetByIdAsync(id);
        if (entity == null) return null;
        return ExperimentTypeMapper.ToResponse(entity);
    }

    public async Task CreateAsync(CreateExperimentTypeRequest request)
    {
        var entity = ExperimentTypeMapper.ToEntity(request);
        await _experimentTypeRepo.AddAsync(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task UpdateAsync(Guid id, UpdateExperimentTypeRequest request)
    {
        var entity = await _experimentTypeRepo.GetByIdAsync(id);
        if (entity == null) return;
        ExperimentTypeMapper.UpdateEntity(entity, request);
        _experimentTypeRepo.Update(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _experimentTypeRepo.GetByIdAsync(id);
        if (entity == null) return;
        _experimentTypeRepo.Delete(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }
    // ExperimentTypeService
    public async Task<List<LookupItem>> GetActivesAsync(string? culture = null)
    {
        var entities = await _experimentTypeRepo.GetAllAsync();
        return entities
            .Where(t => t.DeletedAt == null)
            .Select(t => new LookupItem
            {
                Id = t.Id,
                Name = t.GetName(culture) ?? "???"
            }).ToList();
    }
}