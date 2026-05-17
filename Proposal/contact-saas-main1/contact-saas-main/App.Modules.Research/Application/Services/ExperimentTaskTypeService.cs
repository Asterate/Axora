using App.Modules.Experiment.Application.Interfaces;
using App.Modules.Project.Application.DTO;
using App.Modules.Project.Application.Interfaces.Service;
using App.Modules.Project.Application.Mappers;
using App.Shared.Contracts;

namespace App.Modules.Project.Application.Services;

public class ExperimentTaskTypeService : IExperimentTaskTypeService
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
    public async Task<IEnumerable<ExperimentTaskTypeResponse>> GetAllAsync()
    {
        var entities = await _experimentTaskTypeRepo.GetAllAsync();
        return entities.Select(ExperimentTaskTypeMapper.ToResponse);
    }

    public async Task<ExperimentTaskTypeResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _experimentTaskTypeRepo.GetByIdAsync(id);
        if (entity == null) return null;
        return ExperimentTaskTypeMapper.ToResponse(entity);
    }

    public async Task CreateAsync(SaveExperimentTaskTypeRequest request)
    {
        var entity = ExperimentTaskTypeMapper.ToEntity(request);
        await _experimentTaskTypeRepo.AddAsync(entity);
        entity.CreatedAt = DateTime.Now;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task UpdateAsync(Guid id, SaveExperimentTaskTypeRequest request)
    {
        var entity = await _experimentTaskTypeRepo.GetByIdAsync(id);
        if (entity == null) return;
        ExperimentTaskTypeMapper.UpdateEntity(entity, request);
        _experimentTaskTypeRepo.Update(entity);
        entity.UpdatedAt = DateTime.Now;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _experimentTaskTypeRepo.GetByIdAsync(id);
        if (entity == null) return;
        _experimentTaskTypeRepo.Update(entity);
        entity.DeletedAt = DateTime.Now;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }
    // ExperimentTypeService
    public async Task<List<LookupItem>> GetActivesAsync(string? culture = null)
    {
        var entities = await _experimentTaskTypeRepo.GetAllAsync();
        return entities
            .Where(t => t.DeletedAt == null)
            .Select(t => new LookupItem
            {
                Id = t.Id,
                Name = t.Name.Translate(culture) ??  String.Empty,
            }).ToList();
    }
}