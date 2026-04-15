using App.DAL.EF;
using App.DTO.v1;
using App.Domain.Entities;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq;

namespace WebApp.ApiControllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
public class LookupsController : ControllerBase
{
    private readonly AppDbContext _context;

    public LookupsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/v1.0/lookups/experiment-types
    [HttpGet("experiment-types")]
    [ProducesResponseType(typeof(IEnumerable<LookupDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LookupDto>>> GetExperimentTypes()
    {
        return await _context.ExperimentTypes
            .Select(e => new LookupDto { Id = e.Id, Name = e.ExperimentTypeName })
            .ToListAsync();
    }

    // GET: api/v1.0/lookups/project-types
    [HttpGet("project-types")]
    [ProducesResponseType(typeof(IEnumerable<LookupDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LookupDto>>> GetProjectTypes()
    {
        return await _context.ProjectTypes
            .Select(e => new LookupDto { Id = e.Id, Name = e.Name })
            .ToListAsync();
    }

    // GET: api/v1.0/lookups/task-types
    [HttpGet("task-types")]
    [ProducesResponseType(typeof(IEnumerable<LookupDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LookupDto>>> GetTaskTypes()
    {
        return await _context.TaskTypes
            .Select(e => new LookupDto { Id = e.Id, Name = e.TaskTypeName })
            .ToListAsync();
    }

    // GET: api/v1.0/lookups/lab-types
    [HttpGet("lab-types")]
    [ProducesResponseType(typeof(IEnumerable<LookupDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LookupDto>>> GetLabTypes()
    {
        return await _context.LabTypes
            .Select(e => new LookupDto { Id = e.Id, Name = e.Name })
            .ToListAsync();
    }

    // GET: api/v1.0/lookups/institute-types
    [HttpGet("institute-types")]
    [ProducesResponseType(typeof(IEnumerable<LookupDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LookupDto>>> GetInstituteTypes()
    {
        return await _context.InstituteTypes
            .Select(e => new LookupDto { Id = e.Id, Name = e.Name })
            .ToListAsync();
    }

    // GET: api/v1.0/lookups/equipment-types
    [HttpGet("equipment-types")]
    [ProducesResponseType(typeof(IEnumerable<LookupDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LookupDto>>> GetEquipmentTypes()
    {
        return await _context.EquipmentTypes
            .Select(e => new LookupDto { Id = e.Id, Name = e.EquipmentTypeName })
            .ToListAsync();
    }

    // GET: api/v1.0/lookups/reagent-types
    [HttpGet("reagent-types")]
    [ProducesResponseType(typeof(IEnumerable<LookupDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LookupDto>>> GetReagentTypes()
    {
        return await _context.ReagentTypes
            .Select(e => new LookupDto { Id = e.Id, Name = e.ReagentName })
            .ToListAsync();
    }

    // GET: api/v1.0/lookups/document-types
    [HttpGet("document-types")]
    [ProducesResponseType(typeof(IEnumerable<LookupDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LookupDto>>> GetDocumentTypes()
    {
        return await _context.DocumentTypes
            .Select(e => new LookupDto { Id = e.Id, Name = e.Name })
            .ToListAsync();
    }

    // GET: api/v1.0/lookups/certification-types
    [HttpGet("certification-types")]
    [ProducesResponseType(typeof(IEnumerable<LookupDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LookupDto>>> GetCertificationTypes()
    {
        return await _context.CertificationTypes
            .Select(e => new LookupDto { Id = e.Id, Name = e.Name })
            .ToListAsync();
    }

    // GET: api/v1.0/lookups/projects
    [HttpGet("projects")]
    [ProducesResponseType(typeof(IEnumerable<LookupDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LookupDto>>> GetProjects()
    {
        return await _context.Projects
            .Select(e => new LookupDto { Id = e.Id, Name = e.ProjectName })
            .ToListAsync();
    }

    // GET: api/v1.0/lookups/institute-users
    [HttpGet("institute-users")]
    [ProducesResponseType(typeof(IEnumerable<LookupDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LookupDto>>> GetInstituteUsers()
    {
        return await _context.InstituteUsers
            .Include(e => e.User)
            .Select(e => new LookupDto { Id = e.Id, Name = e.User.FirstName + " " + e.User.LastName })
            .ToListAsync();
    }

    // GET: api/v1.0/lookups/priorities
    /// <summary>
    /// Returns available priority options for experiment tasks
    /// </summary>
    [HttpGet("priorities")]
    [ProducesResponseType(typeof(IEnumerable<PriorityLookupDto>), StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<PriorityLookupDto>> GetPriorities()
    {
        // Priority options: 1=Low, 2=Medium, 3=High, 4=Urgent
        return new List<PriorityLookupDto>
        {
            new() { Id = 1, Name = "Low" },
            new() { Id = 2, Name = "Medium" },
            new() { Id = 3, Name = "High" },
            new() { Id = 4, Name = "Urgent" }
        };
    }

    // GET: api/v1.0/lookups/task-statuses
    /// <summary>
    /// Returns available status options for experiment tasks
    /// </summary>
    [HttpGet("task-statuses")]
    [ProducesResponseType(typeof(IEnumerable<IntLookupDto>), StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<IntLookupDto>> GetTaskStatuses()
    {
        // Status options: 0=Pending, 1=InProgress, 2=Completed, 3=Cancelled
        return Enum.GetValues<EExperimentTaskStatus>()
            .Select(s => new IntLookupDto { Id = (int)s, Name = s.ToString() })
            .ToList();
    }
}