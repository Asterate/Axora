using App.DAL.EF;
using App.DTO.v1;
using App.Domain.Entities;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace WebApp.ApiControllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
public class ExperimentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ExperimentsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/v1.0/experiments
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ExperimentDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ExperimentDto>>> GetExperiments()
    {
        return await _context.Experiments
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

    // GET: api/v1.0/experiments/{id}
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ExperimentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ExperimentDto>> GetExperiment(Guid id)
    {
        var experiment = await _context.Experiments.FindAsync(id);
        if (experiment == null) return NotFound();

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

    // POST: api/v1.0/experiments
    [HttpPost]
    [ProducesResponseType(typeof(ExperimentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ExperimentDto>> CreateExperiment([FromBody] CreateExperimentDto dto)
    {
        // Get user ID from JWT token (secure approach)
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
        {
            return BadRequest("Invalid user token");
        }
        
        // Find InstituteUser by AppUser ID
        var instituteUser = await _context.InstituteUsers
            .Include(u => u.User)
            .FirstOrDefaultAsync(u => u.User.Id == userId);
        if (instituteUser == null)
        {
            return BadRequest($"User not found in InstituteUser for ID {userId}. Contact admin to add you to institute.");
        }
        
        // Validate that the project exists
        var projectExists = await _context.Projects.AnyAsync(p => p.Id == dto.ProjectId);
        if (!projectExists)
        {
            return BadRequest($"Project with ID {dto.ProjectId} not found");
        }
        
        // Validate that the experiment type exists
        var experimentTypeExists = await _context.ExperimentTypes.AnyAsync(e => e.Id == dto.ExperimentTypeId);
        if (!experimentTypeExists)
        {
            return BadRequest($"ExperimentType with ID {dto.ExperimentTypeId} not found");
        }
        
        var experiment = new Experiment
        {
            Id = Guid.NewGuid(),
            ExperimentName = dto.ExperimentName,
            ExperimentNotes = dto.ExperimentNotes,
            ExperimentTypeId = dto.ExperimentTypeId,
            ProjectId = dto.ProjectId,
            InstituteUserId = instituteUser.Id,
            CreatedAt = DateTime.UtcNow
        };
        
        _context.Experiments.Add(experiment);
        await _context.SaveChangesAsync();

        var result = new ExperimentDto
        {
            Id = experiment.Id,
            ExperimentName = experiment.ExperimentName,
            ExperimentNotes = experiment.ExperimentNotes,
            CreatedAt = experiment.CreatedAt,
            ExperimentTypeId = experiment.ExperimentTypeId,
            ProjectId = experiment.ProjectId,
            InstituteUserId = experiment.InstituteUserId
        };

        return CreatedAtAction(nameof(GetExperiment), new { id = result.Id }, result);
    }

    // PUT: api/v1.0/experiments/{id}
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateExperiment(Guid id, [FromBody] CreateExperimentDto dto)
    {
        var experiment = await _context.Experiments.FindAsync(id);
        if (experiment == null) return NotFound();

        experiment.ExperimentName = dto.ExperimentName;
        experiment.ExperimentNotes = dto.ExperimentNotes;
        experiment.ExperimentTypeId = dto.ExperimentTypeId;
        experiment.ProjectId = dto.ProjectId;
        experiment.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/v1.0/experiments/{id}
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteExperiment(Guid id)
    {
        var experiment = await _context.Experiments.FindAsync(id);
        if (experiment == null) return NotFound();

        _context.Experiments.Remove(experiment);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}
