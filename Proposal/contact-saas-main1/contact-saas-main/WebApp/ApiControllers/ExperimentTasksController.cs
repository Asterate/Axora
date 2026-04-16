using App.BLL.Services;
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
[Tags("Experiment Tasks")]
public class ExperimentTasksController : ControllerBase
{
    private readonly IExperimentService _experimentService;
    private readonly AppDbContext _context;

    public ExperimentTasksController(IExperimentService experimentService, AppDbContext context)
    {
        _experimentService = experimentService;
        _context = context;
    }

    // GET: api/v1.0/experimenttasks
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ExperimentTaskResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ExperimentTaskResponse>>> GetExperimentTasks()
    {
        var userId = GetUserId();
        if (userId == null) return BadRequest("Invalid user token");
        
        // Get all experiments for this user (service handles IDOR protection)
        var experiments = await _experimentService.GetAllAsync(userId.Value);
        var experimentIds = experiments.Select(e => e.Id).ToList();
        
        // Get tasks only for allowed experiments
        var tasks = await _context.ExperimentTasks
            .Include(t => t.TaskType)
            .Where(t => experimentIds.Contains(t.ExperimentId) && t.DeletedAt == null)
            .Select(t => new ExperimentTaskResponse
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
                TaskTypeName = t.TaskType.Name
            })
            .ToListAsync();
            
        return Ok(tasks);
    }

    // GET: api/v1.0/experimenttasks/{id}
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ExperimentTaskResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ExperimentTaskResponse>> GetExperimentTask(Guid id)
    {
        var userId = GetUserId();
        if (userId == null) return BadRequest("Invalid user token");
        
        // First get the task
        var task = await _context.ExperimentTasks
            .Include(t => t.TaskType)
            .FirstOrDefaultAsync(t => t.Id == id && t.DeletedAt == null);

        if (task == null) return NotFound();
        
        // Verify user has access to this experiment via the service
        var experiment = await _experimentService.GetByIdAsync(task.ExperimentId, userId.Value);
        if (experiment == null) return NotFound();

        return Ok(new ExperimentTaskResponse
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
            TaskTypeName = task.TaskType.Name
        });
    }

    // POST: api/v1.0/experimenttasks
    [HttpPost]
    [ProducesResponseType(typeof(ExperimentTaskResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ExperimentTaskResponse>> CreateExperimentTask([FromBody] CreateTaskRequest dto)
    {
        var userId = GetUserId();
        if (userId == null) return BadRequest("Invalid user token");
        
        // Verify user has access to the experiment via the service
        var experiment = await _experimentService.GetByIdAsync(dto.ExperimentId, userId.Value);
        if (experiment == null) return BadRequest("You don't have access to this experiment");

        var experimentTask = new ExperimentTask
        {
            Id = Guid.NewGuid(),
            TaskName = new App.Domain.LangStr(dto.TaskName),
            TaskDescription = !string.IsNullOrEmpty(dto.TaskDescription) ? new App.Domain.LangStr(dto.TaskDescription) : null,
            TaskTypeId = dto.TaskTypeId,
            ExperimentId = dto.ExperimentId,
            AssignedUserId = dto.AssignedUserId,
            Priority = dto.Priority,
            Status = EExperimentTaskStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };
        
        _context.ExperimentTasks.Add(experimentTask);
        await _context.SaveChangesAsync();

        var result = new ExperimentTaskResponse
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
    public async Task<IActionResult> UpdateExperimentTask(Guid id, [FromBody] UpdateTaskRequest dto)
    {
        var userId = GetUserId();
        if (userId == null) return BadRequest("Invalid user token");
        
        var task = await _context.ExperimentTasks.FirstOrDefaultAsync(t => t.Id == id);
        if (task == null) return NotFound();
        
        // Verify user has access to this experiment via the service
        var experiment = await _experimentService.GetByIdAsync(task.ExperimentId, userId.Value);
        if (experiment == null) return NotFound();

        _context.Entry(task).State = EntityState.Modified;
        
        task.TaskName.SetTranslation(dto.TaskName);
        if (!string.IsNullOrEmpty(dto.TaskDescription))
        {
            if (task.TaskDescription == null)
                task.TaskDescription = new App.Domain.LangStr(dto.TaskDescription);
            else
                task.TaskDescription.SetTranslation(dto.TaskDescription);
        }
        task.TaskTypeId = dto.TaskTypeId;
        task.ExperimentId = dto.ExperimentId;
        task.AssignedUserId = dto.AssignedUserId;
        task.Priority = dto.Priority;
        task.UpdatedAt = DateTime.UtcNow;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.ExperimentTasks.AnyAsync(t => t.Id == id))
            {
                return NotFound();
            }
            throw;
        }
        
        return NoContent();
    }

    // DELETE: api/v1.0/experimenttasks/{id}
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteExperimentTask(Guid id)
    {
        var userId = GetUserId();
        if (userId == null) return BadRequest("Invalid user token");
        
        var task = await _context.ExperimentTasks.FirstOrDefaultAsync(t => t.Id == id);
        if (task == null) return NotFound();
        
        // Verify user has access to this experiment via the service
        var experiment = await _experimentService.GetByIdAsync(task.ExperimentId, userId.Value);
        if (experiment == null) return NotFound();

        // Soft delete
        task.DeletedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        
        return NoContent();
    }

    private Guid? GetUserId()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
        {
            return null;
        }
        return userId;
    }
}
