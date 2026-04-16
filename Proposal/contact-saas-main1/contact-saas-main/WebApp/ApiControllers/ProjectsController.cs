using App.BLL.Services;
using App.DAL.EF;
using App.DTO.v1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Asp.Versioning;

namespace WebApp.ApiControllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    // GET: api/v1.0/projects
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProjectDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjects()
    {
        var userId = GetUserId();
        if (userId == null) return BadRequest("Invalid user token");
        
        var projects = await _projectService.GetAllAsync(userId.Value);
        return Ok(projects);
    }

    // GET: api/v1.0/projects/{id}
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProjectDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProjectDto>> GetProject(Guid id)
    {
        var userId = GetUserId();
        if (userId == null) return BadRequest("Invalid user token");
        
        var project = await _projectService.GetByIdAsync(id, userId.Value);
        if (project == null) return NotFound();

        return Ok(project);
    }

    // POST: api/v1.0/projects
    [HttpPost]
    [ProducesResponseType(typeof(ProjectDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProjectDto>> CreateProject([FromBody] CreateProjectDto dto)
    {
        var userId = GetUserId();
        if (userId == null) return BadRequest("Invalid user token");

        try
        {
            var result = await _projectService.CreateAsync(dto, userId.Value);
            return CreatedAtAction(nameof(GetProject), new { id = result.Id }, result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // PUT: api/v1.0/projects/{id}
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProject(Guid id, [FromBody] CreateProjectDto dto)
    {
        var userId = GetUserId();
        if (userId == null) return BadRequest("Invalid user token");

        try
        {
            var success = await _projectService.UpdateAsync(id, dto, userId.Value);
            if (!success) return NotFound();
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE: api/v1.0/projects/{id}
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProject(Guid id)
    {
        var userId = GetUserId();
        if (userId == null) return BadRequest("Invalid user token");

        var success = await _projectService.DeleteAsync(id, userId.Value);
        if (!success) return NotFound();

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
