using App.DTO.v1;

namespace App.BLL.Services;

/// <summary>
/// Interface for Project service with IDOR protection
/// </summary>
public interface IProjectService
{
    /// <summary>
    /// Get all projects for the current user's institute (with IDOR protection)
    /// </summary>
    Task<IEnumerable<ProjectResponse>> GetAllAsync(Guid appUserId);
    
    /// <summary>
    /// Get a specific project by ID (with IDOR protection)
    /// </summary>
    Task<ProjectResponse?> GetByIdAsync(Guid id, Guid appUserId);
    
    /// <summary>
    /// Create a new project
    /// </summary>
    Task<ProjectResponse> CreateAsync(CreateProjectRequest dto, Guid appUserId);
    
    /// <summary>
    /// Update a project (with IDOR protection)
    /// </summary>
    Task<bool> UpdateAsync(Guid id, UpdateProjectRequest dto, Guid appUserId);
    
    /// <summary>
    /// Delete a project (with IDOR protection)
    /// </summary>
    Task<bool> DeleteAsync(Guid id, Guid appUserId);
}