using App.DAL.EF;
using App.DTO.v1;
using App.Domain;
using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.BLL.Services;

/// <summary>
/// Project service with IDOR protection
/// Projects are protected by InstituteProject relationship (users can only access their institute's projects)
/// </summary>
public class ProjectService : BaseService, IProjectService
{
    public ProjectService(AppDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Get current user's Institute ID from InstituteUser relationship
    /// </summary>
    private async Task<Guid?> GetCurrentInstituteIdAsync(Guid appUserId)
    {
        var instituteUser = await _context.InstituteUsers
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.UserId == appUserId);
        
        return instituteUser?.InstituteId;
    }

    /// <summary>
    /// Get all projects for the current user's institute (IDOR protected)
    /// Only returns projects associated with the user's institute via InstituteProject
    /// </summary>
    public async Task<IEnumerable<ProjectResponse>> GetAllAsync(Guid appUserId)
    {
        var instituteId = await GetCurrentInstituteIdAsync(appUserId);
        if (!instituteId.HasValue)
        {
            return Enumerable.Empty<ProjectResponse>();
        }

        var projects = await _context.InstituteProjects
            .Where(ip => ip.InstituteId == instituteId.Value)
            .Select(ip => ip.Project)
            .ToListAsync();

        return projects.Select(p => new ProjectResponse
        {
            Id = p.Id,
            ProjectName = p.ProjectName.Translate() ?? string.Empty,
            Funding = p.Funding,
            Requirements = p.Requirements?.Translate() ?? string.Empty,
            RequirementsFilePath = p.RequirementsFilePath,
            ProjectTypeId = p.ProjectTypeId
        }).ToList();
    }

    /// <summary>
    /// Get a specific project by ID (IDOR protected)
    /// Only returns the project if it belongs to the user's institute
    /// </summary>
    public async Task<ProjectResponse?> GetByIdAsync(Guid id, Guid appUserId)
    {
        var instituteId = await GetCurrentInstituteIdAsync(appUserId);
        if (!instituteId.HasValue)
        {
            return null;
        }

        var instituteProject = await _context.InstituteProjects
            .FirstOrDefaultAsync(ip => ip.ProjectId == id && ip.InstituteId == instituteId.Value);

        if (instituteProject == null)
        {
            return null;
        }

        var project = await _context.Projects.FindAsync(id);
        if (project == null)
        {
            return null;
        }

        return new ProjectResponse
        {
            Id = project.Id,
            ProjectName = project.ProjectName.Translate() ?? string.Empty,
            Funding = project.Funding,
            Requirements = project.Requirements?.Translate() ?? string.Empty,
            RequirementsFilePath = project.RequirementsFilePath,
            ProjectTypeId = project.ProjectTypeId
        };
    }

    /// <summary>
    /// Create a new project and associate it with the user's institute
    /// </summary>
    public async Task<ProjectResponse> CreateAsync(CreateProjectRequest dto, Guid appUserId)
    {
        var instituteId = await GetCurrentInstituteIdAsync(appUserId);
        if (!instituteId.HasValue)
        {
            throw new InvalidOperationException("User is not associated with an institute");
        }

        // Validate project type exists
        var projectTypeExists = await _context.ProjectTypes.AnyAsync(p => p.Id == dto.ProjectTypeId);
        if (!projectTypeExists)
        {
            throw new InvalidOperationException($"ProjectType with ID {dto.ProjectTypeId} not found");
        }

        var project = new Project
        {
            Id = Guid.NewGuid(),
            ProjectName = new LangStr(dto.ProjectName ?? string.Empty),
            Funding = dto.Funding,
            Requirements = string.IsNullOrEmpty(dto.Requirements) ? null : new LangStr(dto.Requirements),
            RequirementsFilePath = dto.RequirementsFilePath,
            ProjectTypeId = dto.ProjectTypeId
        };

        _context.Projects.Add(project);

        // Create InstituteProject association
        var instituteProject = new InstituteProject
        {
            Id = Guid.NewGuid(),
            InstituteId = instituteId.Value,
            ProjectId = project.Id,
            CreatedAt = DateTime.UtcNow
        };

        _context.InstituteProjects.Add(instituteProject);
        await _context.SaveChangesAsync();

        return new ProjectResponse
        {
            Id = project.Id,
            ProjectName = project.ProjectName.Translate() ?? string.Empty,
            Funding = project.Funding,
            Requirements = project.Requirements?.Translate() ?? string.Empty,
            RequirementsFilePath = project.RequirementsFilePath,
            ProjectTypeId = project.ProjectTypeId
        };
    }

    /// <summary>
    /// Update a project (IDOR protected)
    /// Only allows updating if the project belongs to the user's institute
    /// </summary>
    public async Task<bool> UpdateAsync(Guid id, UpdateProjectRequest dto, Guid appUserId)
    {
        var instituteId = await GetCurrentInstituteIdAsync(appUserId);
        if (!instituteId.HasValue)
        {
            return false;
        }

        // Check if project belongs to user's institute
        var instituteProject = await _context.InstituteProjects
            .FirstOrDefaultAsync(ip => ip.ProjectId == id && ip.InstituteId == instituteId.Value);

        if (instituteProject == null)
        {
            return false;
        }

        // Validate project type exists
        var projectTypeExists = await _context.ProjectTypes.AnyAsync(p => p.Id == dto.ProjectTypeId);
        if (!projectTypeExists)
        {
            throw new InvalidOperationException($"ProjectType with ID {dto.ProjectTypeId} not found");
        }

        var project = await _context.Projects.FindAsync(id);
        if (project == null)
        {
            return false;
        }

        // Ensure the entity is attached and marked as modified
        _context.Entry(project).State = EntityState.Modified;
        
        project.ProjectName.SetTranslation(dto.ProjectName);
        project.Funding = dto.Funding;
        // Handle Requirements - only update if provided
        if (!string.IsNullOrEmpty(dto.Requirements))
        {
            if (project.Requirements == null)
            {
                project.Requirements = new LangStr(dto.Requirements);
            }
            else
            {
                project.Requirements.SetTranslation(dto.Requirements);
            }
        }
        project.RequirementsFilePath = dto.RequirementsFilePath;
        project.ProjectTypeId = dto.ProjectTypeId;

        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Delete a project (IDOR protected)
    /// Only allows deletion if the project belongs to the user's institute
    /// </summary>
    public async Task<bool> DeleteAsync(Guid id, Guid appUserId)
    {
        var instituteId = await GetCurrentInstituteIdAsync(appUserId);
        if (!instituteId.HasValue)
        {
            return false;
        }

        // Check if project belongs to user's institute
        var instituteProject = await _context.InstituteProjects
            .FirstOrDefaultAsync(ip => ip.ProjectId == id && ip.InstituteId == instituteId.Value);

        if (instituteProject == null)
        {
            return false;
        }

        var project = await _context.Projects.FindAsync(id);
        if (project == null)
        {
            return false;
        }

        // Remove InstituteProject association first
        _context.InstituteProjects.Remove(instituteProject);
        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
        return true;
    }
}