using App.Modules.Experiment.Application.Interfaces;
using App.Modules.Project.Application.DTO;
using App.Modules.Project.Application.Interfaces.Service;
using App.Modules.Project.Application.Mappers;
using App.Shared.Contracts;

namespace App.Modules.Project.Application.Services;

public class ExperimentTypeService : IExperimentTypeService
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
    public async Task<IEnumerable<ExperimentTypeResponse>> GetAllAsync()
    {
        var entities = await _experimentTypeRepo.GetAllAsync();
        return entities.Select(ExperimentTypeMapper.ToResponse);
    }

    public async Task<ExperimentTypeResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _experimentTypeRepo.GetByIdAsync(id);
        if (entity == null) return null;
        return ExperimentTypeMapper.ToResponse(entity);
    }

    public async Task CreateAsync(SaveExperimentTypeRequest request)
    {
        var entity = ExperimentTypeMapper.ToEntity(request);
        await _experimentTypeRepo.AddAsync(entity);
        entity.CreatedAt = DateTime.Now;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task UpdateAsync(Guid id, SaveExperimentTypeRequest request)
    {
        var entity = await _experimentTypeRepo.GetByIdAsync(id);
        if (entity == null) return;
        ExperimentTypeMapper.UpdateEntity(entity, request);
        _experimentTypeRepo.Update(entity);
        entity.UpdatedAt = DateTime.Now;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _experimentTypeRepo.GetByIdAsync(id);
        if (entity == null) return;
        _experimentTypeRepo.Update(entity);
        entity.DeletedAt = DateTime.Now;
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
                Name = t.Name.Translate(culture) ??  String.Empty,
            }).ToList();
    }
}