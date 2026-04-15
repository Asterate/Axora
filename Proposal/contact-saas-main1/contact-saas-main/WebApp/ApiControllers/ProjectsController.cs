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
public class ProjectsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProjectsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/v1.0/projects
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProjectDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjects()
    {
        return await _context.Projects
            .Select(e => new ProjectDto
            {
                Id = e.Id,
                ProjectName = e.ProjectName,
                Funding = e.Funding,
                Requirements = e.Requirements,
                RequirementsFilePath = e.RequirementsFilePath,
                PublicTypeId = e.PublicTypeId
            })
            .ToListAsync();
    }

    // GET: api/v1.0/projects/{id}
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProjectDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProjectDto>> GetProject(Guid id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null) return NotFound();

        return new ProjectDto
        {
            Id = project.Id,
            ProjectName = project.ProjectName,
            Funding = project.Funding,
            Requirements = project.Requirements,
            RequirementsFilePath = project.RequirementsFilePath,
            PublicTypeId = project.PublicTypeId
        };
    }

    // POST: api/v1.0/projects
    [HttpPost]
    [ProducesResponseType(typeof(ProjectDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProjectDto>> CreateProject([FromBody] CreateProjectDto dto)
    {
        var project = new Project
        {
            Id = Guid.NewGuid(),
            ProjectName = dto.ProjectName,
            Funding = dto.Funding,
            Requirements = dto.Requirements,
            RequirementsFilePath = dto.RequirementsFilePath,
            PublicTypeId = dto.PublicTypeId
        };
        
        _context.Projects.Add(project);  // FIXED: Use Projects, not Experiments
        await _context.SaveChangesAsync();

        var result = new ProjectDto
        {
            Id = project.Id,
            ProjectName = project.ProjectName,
            Funding = project.Funding,
            Requirements = project.Requirements,
            RequirementsFilePath = project.RequirementsFilePath,
            PublicTypeId = project.PublicTypeId
        };

        return CreatedAtAction(nameof(GetProject), new { id = result.Id }, result);
    }

    // PUT: api/v1.0/projects/{id}
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProject(Guid id, [FromBody] CreateProjectDto dto)
    {
        var project = await _context.Projects.FindAsync(id);  // FIXED: Use Projects
        if (project == null) return NotFound();

        project.ProjectName = dto.ProjectName;
        project.Funding = dto.Funding;
        project.Requirements = dto.Requirements;
        project.RequirementsFilePath = dto.RequirementsFilePath;
        project.PublicTypeId = dto.PublicTypeId;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/v1.0/projects/{id}
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProject(Guid id)  // FIXED: Rename method
    {
        var project = await _context.Projects.FindAsync(id);  // FIXED: Use Projects
        if (project == null) return NotFound();

        _context.Projects.Remove(project);  // FIXED: Use Projects
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}
