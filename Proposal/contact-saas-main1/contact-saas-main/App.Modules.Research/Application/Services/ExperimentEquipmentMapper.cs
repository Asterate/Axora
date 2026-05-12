using App.Modules.Experiment.Application.Interfaces;
using App.Modules.Experiment.Application.Mapper;
using App.Shared.Contracts;

public class ExperimentEquipmentService
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
    public async Task<IEnumerable<ExperimentEquipmentListResponse>> GetAllAsync()
    {
        var entities = await _experimentEquipmentRepo.GetAllAsync();
        return entities.Select(ExperimentEquipmentMapper.ToListResponse);
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