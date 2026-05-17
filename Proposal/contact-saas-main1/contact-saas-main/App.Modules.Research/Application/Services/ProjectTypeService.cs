using App.Modules.Project.Application.DTO;
using App.Modules.Project.Application.Interfaces;
using App.Modules.Project.Application.Interfaces.Service;
using App.Modules.Project.Application.Mapper;
using App.Shared.Contracts;

namespace App.Modules.Project.Application.Services;

public class ProjectTypeService : IProjectTypeService
{
    private readonly IProjectTypeRepository _projectType;
    private readonly IUnitOfWork _uow;

    public ProjectTypeService(
        IProjectTypeRepository projectType, 
        IUnitOfWork uow)
    {
        _projectType = projectType;
        _uow = uow;
    }
    public async Task<IEnumerable<ProjectTypeResponse>> GetAllAsync()
    {
        var entities = await _projectType.GetAllAsync();
        return entities.Select(ProjectTypeMapper.ToResponse);
    }

    public async Task<ProjectTypeResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _projectType.GetByIdAsync(id);
        if (entity == null) return null;
        return ProjectTypeMapper.ToResponse(entity);
    }

    public async Task CreateAsync(SaveProjectTypeRequest request)
    {
        var entity = ProjectTypeMapper.ToEntity(request);
        await _projectType.AddAsync(entity);
        entity.CreatedAt = DateTime.UtcNow;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task UpdateAsync(Guid id, SaveProjectTypeRequest request)
    {
        var entity = await _projectType.GetByIdAsync(id);
        if (entity == null) return;
        ProjectTypeMapper.UpdateEntity(entity, request);
        _projectType.Update(entity);
        entity.UpdatedAt = DateTime.UtcNow;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _projectType.GetByIdAsync(id);
        if (entity == null) return;
        _projectType.Update(entity);
        entity.DeletedAt = DateTime.UtcNow;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }
    // ExperimentTypeService
    public async Task<List<LookupItem>> GetActivesAsync(string? culture = null)
    {
        var entities = await _projectType.GetAllAsync();
        return entities
            .Where(t => t.DeletedAt == null)
            .Select(t => new LookupItem
            {
                Id = t.Id,
                Name = t.Name.Translate(culture) ??  String.Empty,
            }).ToList();
    }
}