using App.Modules.Project.Application.DTO;
using App.Modules.Project.Application.Interfaces;
using App.Modules.Project.Application.Interfaces.Service;
using App.Modules.Project.Application.Mappers;
using App.Shared.Contracts;

namespace App.Modules.Project.Application.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _project;
    private readonly IUnitOfWork _uow;

    public ProjectService(
        IProjectRepository project, 
        IUnitOfWork uow)
    {
        _project = project;
        _uow = uow;
    }
    public async Task<IEnumerable<ProjectResponse>> GetAllAsync()
    {
        var entities = await _project.GetAllAsync();
        return entities.Select(ProjectMapper.ToResponse);
    }

    public async Task<ProjectResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _project.GetByIdAsync(id);
        if (entity == null) return null;
        return ProjectMapper.ToResponse(entity);
    }

    public async Task<ProjectResponse> CreateAsync(CreateProjectRequest request)
    {
        var entity = ProjectMapper.ToEntity(request);
        await _project.AddAsync(entity);
        await _uow.SaveChangesAsync();

        return ProjectMapper.ToResponse(entity);
    }

    public async Task UpdateAsync(Guid id, UpdateProjectRequest request)
    {
        var entity = await _project.GetByIdAsync(id);
        if (entity == null) return;
        ProjectMapper.UpdateEntity(entity, request);
        _project.Update(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _project.GetByIdAsync(id);
        if (entity == null) return;
        _project.Delete(entity);
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
                Name = t.GetProjectName(culture) ?? "???"
            }).ToList();
    }
}