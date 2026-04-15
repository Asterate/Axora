using App.DAL.EF;
using App.DTO.v1;
using App.Domain.Entities;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiControllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize]
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
        var experiment = new Experiment
        {
            Id = Guid.NewGuid(),
            ExperimentName = dto.ExperimentName,
            ExperimentNotes = dto.ExperimentNotes,
            ExperimentTypeId = dto.ExperimentTypeId,
            ProjectId = dto.ProjectId,
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
