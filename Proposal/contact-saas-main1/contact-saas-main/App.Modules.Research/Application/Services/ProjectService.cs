using App.Modules.Project.Application.DTO;
using App.Modules.Project.Application.Interfaces;
using App.Modules.Project.Application.Interfaces.Service;
using App.Modules.Project.Application.Mappers;
using App.Modules.Project.Domain;
using App.Shared.Contracts;
using App.Shared.Contracts.Events;
using MediatR;

namespace App.Modules.Project.Application.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _project;
    private readonly IUnitOfWork _uow;
    private readonly IMediator _mediator;

    public ProjectService(
        IProjectRepository project, 
        IUnitOfWork uow, IMediator mediator)
    {
        _project = project;
        _uow = uow;
        _mediator = mediator;
    }
    public async Task<IEnumerable<ProjectListResponse>> GetAllAsync(Guid userId)
    {
        var instituteId = await _mediator.Send(new InstituteUserEvent.GetInstituteIdByUserIdQuery(userId));
    
        var entities = await _project.GetAllAsync();
    
        return entities
            .Where(p => p.InstituteProjects.Any(ip => ip.InstituteId == instituteId))
            .Select(ProjectMapper.ToListResponse);
    }
    

    public async Task<ProjectResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _project.GetByIdAsync(id);
        if (entity == null) return null;
        return ProjectMapper.ToResponse(entity);
    } 
    public async Task<SaveProjectRequest?> GetByIdEditAsync(Guid id)
    {
        var entity = await _project.GetByIdAsync(id);
        if (entity == null) return null;
        return ProjectMapper.ToRequest(entity);
    }

    public async Task<ProjectResponse> CreateAsync(SaveProjectRequest request, Guid userId)
    {
        var instituteId = await _mediator.Send(new InstituteUserEvent.GetInstituteIdByUserIdQuery(userId));
    
        var entity = ProjectMapper.ToEntity(request);
        entity.CreatedAt = DateTime.UtcNow;
        entity.InstituteProjects = new List<InstituteProject>
        {
            new ()
            {
                InstituteId = instituteId!.Value,
                ProjectId = entity.Id
            }
        };
    
        await _project.AddAsync(entity);
        await _uow.SaveChangesAsync();

        var saved = await _project.GetByIdAsync(entity.Id);
        if (saved == null) throw new InvalidOperationException("Project not found after save");
        return ProjectMapper.ToResponse(saved);
    }

    public async Task UpdateAsync(Guid id, SaveProjectRequest request)
    {
        var entity = await _project.GetByIdAsync(id);
        if (entity == null) return;
        ProjectMapper.UpdateEntity(entity, request);
        _project.Update(entity);
        entity.UpdatedAt = DateTime.UtcNow;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _project.GetByIdAsync(id);
        if (entity == null) return;
        _project.Update(entity);
        entity.DeletedAt = DateTime.UtcNow;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }
    public async Task<int> CountAsync()
    {
        var all = await _project.GetAllAsync();
        return all.Count(i => i.DeletedAt == null);
    }
    
    public async Task<IEnumerable<ProjectListResponse>> GetRecentAsync(int take)
    {
        var all = await _project.GetAllAsync();
        return all
            .Where(i => i.DeletedAt == null)
            .OrderByDescending(i => i.CreatedAt)
            .Take(take)
            .Select(ProjectMapper.ToListResponse);
    }
    public async Task<List<LookupItem>> GetActivesAsync(string? culture = null)
    {
        var entities = await _project.GetAllAsync();
        return entities
            .Where(t => t.DeletedAt == null)
            .Select(t => new LookupItem
            {
                Id = t.Id,
                Name = t.ProjectName.Translate(culture) ?? String.Empty,
            }).ToList();
    }
}