using App.DTO.v1;

namespace App.BLL.Services;

/// <summary>
/// Interface for Experiment service with IDOR protection
/// </summary>
public interface IExperimentService
{
    /// <summary>
    /// Get all experiments for the current user (with IDOR protection)
    /// </summary>
    Task<IEnumerable<ExperimentResponse>> GetAllAsync(Guid appUserId);
    
    /// <summary>
    /// Get a specific experiment by ID (with IDOR protection)
    /// </summary>
    Task<ExperimentResponse?> GetByIdAsync(Guid id, Guid appUserId);
    
    /// <summary>
    /// Create a new experiment
    /// </summary>
    Task<ExperimentResponse> CreateAsync(CreateExperimentRequest dto, Guid appUserId);
    
    /// <summary>
    /// Update an experiment (with IDOR protection)
    /// </summary>
    Task<bool> UpdateAsync(Guid id, UpdateExperimentRequest dto, Guid appUserId);
    
    /// <summary>
    /// Delete an experiment (with IDOR protection)
    /// </summary>
    Task<bool> DeleteAsync(Guid id, Guid appUserId);
}