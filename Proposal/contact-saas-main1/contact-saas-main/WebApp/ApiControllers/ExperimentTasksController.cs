using App.BLL.Services;
using App.DAL.EF;
using App.DTO.v1;
using App.Domain.Entities;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using App.Modules.Experiment.Application.Mapper;
using App.Modules.Project.Application.DTO;
using App.Modules.Project.Application.Mappers;
using App.Modules.Project.Application.Services;
using ExperimentTaskResponse = App.Modules.Project.Application.DTO.ExperimentTaskResponse;

namespace WebApp.ApiControllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
[Tags("Experiment Tasks")]
public class ExperimentTasksController : ControllerBase
{
    private readonly IExperimentService _experimentService;
    private readonly ExperimentTaskService _experimentTaskService;

    public ExperimentTasksController(IExperimentService experimentService, ExperimentTaskService context)
    {
        _experimentService = experimentService;
        _experimentTaskService = context;
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
        var experimentIds = experiments.Select(e => e.Id);
        var tasks = await _experimentTaskService.GetAllByExperimentIdsAsync(experimentIds);
        return Ok(tasks);
            
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
        
        var task = await _experimentTaskService.GetByIdAsync(id);
        if (task == null) return NotFound();
        var experiment = await _experimentService.GetByIdAsync(task.ExperimentId, userId.Value);
        if (experiment == null) return NotFound();
        return Ok(task);
    }

    // POST: api/v1.0/experimenttasks
    [HttpPost]
    [ProducesResponseType(typeof(ExperimentTaskResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ExperimentTaskResponse>> CreateExperimentTask([FromBody] CreateExperimentTaskRequest dto)
    {
        var userId = GetUserId();
        if (userId == null) return BadRequest("Invalid user token");
        
        // Verify user has access to the experiment via the service
        var experiment = await _experimentService.GetByIdAsync(dto.ExperimentId, userId.Value);
        if (experiment == null) return BadRequest("No access to this experiment");
        var created = await _experimentTaskService.CreateAndReturnAsync(dto);
        return CreatedAtAction(nameof(GetExperimentTask), new { id = created.Id }, ExperimentTaskMapper.ToResponse(created));
    }

    // PUT: api/v1.0/experimenttasks/{id}
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateExperimentTask(Guid id, [FromBody] UpdateExperimentTaskRequest dto)
    {
        var userId = GetUserId();
        if (userId == null) return BadRequest("Invalid user token");

        var task = await _experimentTaskService.GetByIdAsync(id);
        if (task == null) return NotFound();

        var experiment = await _experimentService.GetByIdAsync(task.ExperimentId, userId.Value);
        if (experiment == null) return NotFound();

        await _experimentTaskService.UpdateAsync(id, dto);

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
        
        var task = await _experimentTaskService.GetByIdAsync(id);
        if (task == null) return NotFound();
        
        // Verify user has access to this experiment via the service
        var experiment = await _experimentService.GetByIdAsync(task.ExperimentId, userId.Value);
        if (experiment == null) return NotFound();

        // Soft delete
        task.DeletedAt = DateTime.UtcNow;
        
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
