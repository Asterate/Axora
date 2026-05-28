using App.Modules.Experiment.Application.Interfaces;
using App.Modules.Project.Application.DTO;
using App.Modules.Project.Application.Interfaces.Service;
using App.Modules.Project.Application.Mappers;
using App.Shared.Contracts;
using MediatR;

namespace App.Modules.Project.Application.Services;

public class ExperimentService : IExperimentService
{
    private readonly IExperimentRepository _experimentRepo;
    private readonly IUnitOfWork _uow;
    private readonly IMediator _mediator;

    public ExperimentService(
        IExperimentRepository experimentRepo,
        IUnitOfWork uow, IMediator mediator)
    {
        _experimentRepo = experimentRepo;
        _uow = uow;
        _mediator = mediator;
    }
    public async Task<IEnumerable<ExperimentResponse>> GetAllAsync(Guid userId)
    {
        var entities = await _experimentRepo.GetAllAsync();
        return entities
            .Where(e => e.InstituteUserId == userId)
            .Select(ExperimentMapper.ToResponse);
    }

    public async Task<ExperimentResponse?> GetByIdAsync(Guid id, Guid instituteId)
    {
        var entity = await _experimentRepo.GetByIdAsync(id);
        if (entity == null) return null;
        return ExperimentMapper.ToResponse(entity);
    }
    public async Task<SaveExperimentRequest?> GetByIdEditAsync(Guid id, Guid instituteId)
    {
        var entity = await _experimentRepo.GetByIdAsync(id);
        if (entity == null) return null;
        return ExperimentMapper.ToRequest(entity);
    }

    public async Task<ExperimentResponse> CreateAsync(SaveExperimentRequest request, Guid instituteId )
    {
        var entity = ExperimentMapper.ToEntity(request);
        await _experimentRepo.AddAsync(entity);
        entity.CreatedAt = DateTime.Now;
        await _uow.SaveChangesAsync();

        // Reload entity with navigation properties for proper mapping
        var createdEntity = await _experimentRepo.GetByIdAsync(entity.Id);
        if (createdEntity == null)
        {
            throw new InvalidOperationException("Failed to retrieve created experiment");
        }

        return ExperimentMapper.ToResponse(createdEntity);
    }

  public async Task UpdateAsync(Guid id, SaveExperimentRequest request, Guid instituteId)
    {
        var entity = await _experimentRepo.GetByIdAsync(id);
        if (entity == null) return;
        ExperimentMapper.UpdateEntity(entity, request);
        _experimentRepo.Update(entity);
        entity.UpdatedAt = DateTime.Now;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id, Guid instituteId)
    {
        var entity = await _experimentRepo.GetByIdAsync(id);
        if (entity == null) return;
        _experimentRepo.Update(entity);
        entity.DeletedAt = DateTime.Now;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }
    public async Task<List<LookupItem>> GetActivesAsync(Guid instituteId, string? culture = null)
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