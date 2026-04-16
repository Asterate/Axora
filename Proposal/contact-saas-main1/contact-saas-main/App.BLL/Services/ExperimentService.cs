using App.DAL.EF;
using App.DTO.v1;
using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.BLL.Services;

/// <summary>
/// Experiment service with IDOR protection
/// </summary>
public class ExperimentService : BaseService, IExperimentService
{
    public ExperimentService(AppDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Get all experiments for the current user (IDOR protected)
    /// Only returns experiments owned by the current user's InstituteUser
    /// </summary>
    public async Task<IEnumerable<ExperimentDto>> GetAllAsync(Guid appUserId)
    {
        var instituteUserId = await GetCurrentInstituteUserIdAsync(appUserId);
        if (!instituteUserId.HasValue)
        {
            return Enumerable.Empty<ExperimentDto>();
        }

        return await _context.Experiments
            .Where(e => e.InstituteUserId == instituteUserId.Value)
            .Select(e => new ExperimentDto
            {
                Id = e.Id,
                ExperimentName = e.ExperimentName,
                ExperimentNotes = e.ExperimentNotes,
                CreatedAt = e.CreatedAt,
                UpdatedAt = e.UpdatedAt,
                ExperimentTypeId = e.ExperimentTypeId,
                ProjectId = e.ProjectId,
                InstituteUserId = e.InstituteUserId
            })
            .ToListAsync();
    }

    /// <summary>
    /// Get a specific experiment by ID (IDOR protected)
    /// Only returns the experiment if it belongs to the current user
    /// </summary>
    public async Task<ExperimentDto?> GetByIdAsync(Guid id, Guid appUserId)
    {
        var instituteUserId = await GetCurrentInstituteUserIdAsync(appUserId);
        if (!instituteUserId.HasValue)
        {
            return null;
        }

        var experiment = await _context.Experiments
            .FirstOrDefaultAsync(e => e.Id == id && e.InstituteUserId == instituteUserId.Value);

        if (experiment == null)
        {
            return null;
        }

        return new ExperimentDto
        {
            Id = experiment.Id,
            ExperimentName = experiment.ExperimentName,
            ExperimentNotes = experiment.ExperimentNotes,
            CreatedAt = experiment.CreatedAt,
            UpdatedAt = experiment.UpdatedAt,
            ExperimentTypeId = experiment.ExperimentTypeId,
            ProjectId = experiment.ProjectId,
            InstituteUserId = experiment.InstituteUserId
        };
    }

    /// <summary>
    /// Create a new experiment
    /// </summary>
    public async Task<ExperimentDto> CreateAsync(CreateExperimentDto dto, Guid appUserId)
    {
        var instituteUserId = await GetCurrentInstituteUserIdAsync(appUserId);
        if (!instituteUserId.HasValue)
        {
            throw new InvalidOperationException("User is not associated with an institute");
        }

        // Validate project exists
        var projectExists = await _context.Projects.AnyAsync(p => p.Id == dto.ProjectId);
        if (!projectExists)
        {
            throw new InvalidOperationException($"Project with ID {dto.ProjectId} not found");
        }

        // Validate experiment type exists
        var experimentTypeExists = await _context.ExperimentTypes.AnyAsync(e => e.Id == dto.ExperimentTypeId);
        if (!experimentTypeExists)
        {
            throw new InvalidOperationException($"ExperimentType with ID {dto.ExperimentTypeId} not found");
        }

        var experiment = new Experiment
        {
            Id = Guid.NewGuid(),
            ExperimentName = dto.ExperimentName,
            ExperimentNotes = dto.ExperimentNotes,
            ExperimentTypeId = dto.ExperimentTypeId,
            ProjectId = dto.ProjectId,
            InstituteUserId = instituteUserId.Value,
            CreatedAt = DateTime.UtcNow
        };

        _context.Experiments.Add(experiment);
        await _context.SaveChangesAsync();

        return new ExperimentDto
        {
            Id = experiment.Id,
            ExperimentName = experiment.ExperimentName,
            ExperimentNotes = experiment.ExperimentNotes,
            CreatedAt = experiment.CreatedAt,
            UpdatedAt = experiment.UpdatedAt,
            ExperimentTypeId = experiment.ExperimentTypeId,
            ProjectId = experiment.ProjectId,
            InstituteUserId = experiment.InstituteUserId
        };
    }

    /// <summary>
    /// Update an experiment (IDOR protected)
    /// Only allows updating if the experiment belongs to the current user
    /// </summary>
    public async Task<bool> UpdateAsync(Guid id, CreateExperimentDto dto, Guid appUserId)
    {
        var instituteUserId = await GetCurrentInstituteUserIdAsync(appUserId);
        if (!instituteUserId.HasValue)
        {
            return false;
        }

        var experiment = await _context.Experiments
            .FirstOrDefaultAsync(e => e.Id == id && e.InstituteUserId == instituteUserId.Value);

        if (experiment == null)
        {
            return false;
        }

        // Validate project exists
        var projectExists = await _context.Projects.AnyAsync(p => p.Id == dto.ProjectId);
        if (!projectExists)
        {
            throw new InvalidOperationException($"Project with ID {dto.ProjectId} not found");
        }

        // Validate experiment type exists
        var experimentTypeExists = await _context.ExperimentTypes.AnyAsync(e => e.Id == dto.ExperimentTypeId);
        if (!experimentTypeExists)
        {
            throw new InvalidOperationException($"ExperimentType with ID {dto.ExperimentTypeId} not found");
        }

        experiment.ExperimentName = dto.ExperimentName;
        experiment.ExperimentNotes = dto.ExperimentNotes;
        experiment.ExperimentTypeId = dto.ExperimentTypeId;
        experiment.ProjectId = dto.ProjectId;
        experiment.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Delete an experiment (IDOR protected)
    /// Only allows deletion if the experiment belongs to the current user
    /// </summary>
    public async Task<bool> DeleteAsync(Guid id, Guid appUserId)
    {
        var instituteUserId = await GetCurrentInstituteUserIdAsync(appUserId);
        if (!instituteUserId.HasValue)
        {
            return false;
        }

        var experiment = await _context.Experiments
            .FirstOrDefaultAsync(e => e.Id == id && e.InstituteUserId == instituteUserId.Value);

        if (experiment == null)
        {
            return false;
        }

        _context.Experiments.Remove(experiment);
        await _context.SaveChangesAsync();
        return true;
    }
}