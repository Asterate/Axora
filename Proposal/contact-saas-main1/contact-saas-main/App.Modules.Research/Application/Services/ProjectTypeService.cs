using App.Modules.Project.Application.Interfaces;
using App.Modules.Project.Application.Mapper;
using App.Shared.Contracts;

public class ProjectTypeService
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
    public async Task<IEnumerable<ProjectTypeListResponse>> GetAllAsync()
    {
        var entities = await _projectType.GetAllAsync();
        return entities.Select(ProjectTypeMapper.ToListResponse);
    }

    public async Task<ProjectTypeResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _projectType.GetByIdAsync(id);
        if (entity == null) return null;
        return ProjectTypeMapper.ToResponse(entity);
    }

    public async Task CreateAsync(CreateProjectTypeRequest request)
    {
        var entity = ProjectTypeMapper.ToEntity(request);
        await _projectType.AddAsync(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task UpdateAsync(Guid id, UpdateProjectTypeRequest request)
    {
        var entity = await _projectType.GetByIdAsync(id);
        if (entity == null) return;
        ProjectTypeMapper.UpdateEntity(entity, request);
        _projectType.Update(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _projectType.GetByIdAsync(id);
        if (entity == null) return;
        _projectType.Delete(entity);
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
                Name = t.Name?.Translate(culture) ?? "???"
            }).ToList();
    }
}