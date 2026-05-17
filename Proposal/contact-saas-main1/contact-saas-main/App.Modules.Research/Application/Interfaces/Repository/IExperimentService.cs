
using App.Modules.Project.Application.DTO;

namespace App.Modules.Project.Application.Interfaces.Repository;

/// <summary>
/// Interface for Experiment service with IDOR protection
/// </summary>
public interface IExperimentService
{
    /// <summary>
    /// Get all experiments for the current user (with IDOR protection)
    /// </summary>
    Task<IEnumerable<ExperimentResponse>> GetAllAsync();
    
    /// <summary>
    /// Get a specific experiment by ID (with IDOR protection)
    /// </summary>
    Task<ExperimentResponse?> GetByIdAsync(Guid id);
    
    /// <summary>
    /// Create a new experiment
    /// </summary>
    Task<ExperimentResponse> CreateAsync(SaveExperimentRequest dto);
    
    /// <summary>
    /// Update an experiment (with IDOR protection)
    /// </summary>
    Task<bool> UpdateAsync(Guid id, SaveExperimentRequest dto);
    
    /// <summary>
    /// Delete an experiment (with IDOR protection)
    /// </summary>
    Task<bool> DeleteAsync(Guid id);
}