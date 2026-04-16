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
[Authorize(AuthenticationSchemes = "Bearer")]
[Tags("Experiment Tasks")]
public class ExperimentTasksController : ControllerBase
{
    private readonly AppDbContext _context;

    public ExperimentTasksController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/v1.0/experimenttasks
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ExperimentTaskDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ExperimentTaskDto>>> GetExperimentTasks()
    {
        return await _context.ExperimentTasks
            .Include(t => t.TaskType)
            .Include(t => t.Experiment)
            .Where(t => t.DeletedAt == null)
            .Select(t => new ExperimentTaskDto
            {
                Id = t.Id,
                TaskName = t.TaskName,
                TaskDescription = t.TaskDescription,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt,
                DeletedAt = t.DeletedAt,
                Status = (int)t.Status,
                Priority = t.Priority,
                TaskTypeId = t.TaskTypeId,
                AssignedUserId = t.AssignedUserId,
                ExperimentId = t.ExperimentId,
                TaskTypeName = t.TaskType.TaskTypeName
            })
            .ToListAsync();
    }

    // GET: api/v1.0/experimenttasks/{id}
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ExperimentTaskDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ExperimentTaskDto>> GetExperimentTask(Guid id)
    {
        var task = await _context.ExperimentTasks
            .Include(t => t.TaskType)
            .Include(t => t.Experiment)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (task == null) return NotFound();

        return new ExperimentTaskDto
        {
            Id = task.Id,
            TaskName = task.TaskName,
            TaskDescription = task.TaskDescription,
            CreatedAt = task.CreatedAt,
            UpdatedAt = task.UpdatedAt,
            DeletedAt = task.DeletedAt,
            Status = (int)task.Status,
            Priority = task.Priority,
            TaskTypeId = task.TaskTypeId,
            AssignedUserId = task.AssignedUserId,
            ExperimentId = task.ExperimentId,
            TaskTypeName = task.TaskType.TaskTypeName
        };
    }

    // POST: api/v1.0/experimenttasks
    [HttpPost]
    [ProducesResponseType(typeof(ExperimentTaskDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ExperimentTaskDto>> CreateExperimentTask([FromBody] CreateTaskDto dto)
    {
        var experimentTask = new ExperimentTask
        {
            Id = Guid.NewGuid(),
            TaskName = dto.TaskName,
            TaskDescription = dto.TaskDescription ?? string.Empty,
            TaskTypeId = dto.TaskTypeId,
            ExperimentId = dto.ExperimentId,
            AssignedUserId = dto.AssignedUserId,
            Priority = dto.Priority,
            Status = EExperimentTaskStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };
        
        _context.ExperimentTasks.Add(experimentTask);
        await _context.SaveChangesAsync();

        var result = new ExperimentTaskDto
        {
            Id = experimentTask.Id,
            TaskName = experimentTask.TaskName,
            TaskDescription = experimentTask.TaskDescription,
            CreatedAt = experimentTask.CreatedAt,
            Status = (int)experimentTask.Status,
            Priority = experimentTask.Priority,
            TaskTypeId = experimentTask.TaskTypeId,
            AssignedUserId = experimentTask.AssignedUserId,
            ExperimentId = experimentTask.ExperimentId
        };

        return CreatedAtAction(nameof(GetExperimentTask), new { id = result.Id }, result);
    }

    // PUT: api/v1.0/experimenttasks/{id}
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateExperimentTask(Guid id, [FromBody] CreateTaskDto dto)
    {
        var experimentTask = await _context.ExperimentTasks.FindAsync(id);
        if (experimentTask == null) return NotFound();

        experimentTask.TaskName = dto.TaskName;
        experimentTask.TaskDescription = dto.TaskDescription ??  string.Empty;
        experimentTask.TaskTypeId = dto.TaskTypeId;
        experimentTask.ExperimentId = dto.ExperimentId;
        experimentTask.AssignedUserId = dto.AssignedUserId;
        experimentTask.Priority = dto.Priority;
        experimentTask.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/v1.0/experimenttasks/{id}
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteExperimentTask(Guid id)
    {
        var experimentTask = await _context.ExperimentTasks.FindAsync(id);
        if (experimentTask == null) return NotFound();

        // Soft delete
        experimentTask.DeletedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}
