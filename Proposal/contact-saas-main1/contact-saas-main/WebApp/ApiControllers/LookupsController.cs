using App.DAL.EF;
using App.DTO.v1;
using App.Domain;
using App.Domain.Entities;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
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
    public async Task<ActionResult<IEnumerable<LookupDto>>> GetExperimentTypes(string? culture)
    {
        var cultureName = culture ?? Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
        var items = await _context.ExperimentTypes.ToListAsync();
        return items.Select(e => new LookupDto { Id = e.Id, Name = e.Name.Translate(cultureName)?? string.Empty }).ToList();
    }

    // GET: api/v1.0/lookups/project-types
    [HttpGet("project-types")]
    [ProducesResponseType(typeof(IEnumerable<LookupDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LookupDto>>> GetProjectTypes(string? culture)
    {
        var cultureName = culture ?? Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
        var items = await _context.ProjectTypes.ToListAsync();
        return items.Select(e => new LookupDto { Id = e.Id, Name = e.Name.Translate(cultureName) ?? string.Empty }).ToList();
    }

    // GET: api/v1.0/lookups/task-types
    [HttpGet("task-types")]
    [ProducesResponseType(typeof(IEnumerable<LookupDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LookupDto>>> GetTaskTypes(string? culture)
    {
        var cultureName = culture ?? Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
        var items = await _context.TaskTypes.ToListAsync();
        return items.Select(e => new LookupDto { Id = e.Id, Name = e.Name.Translate(cultureName) ?? string.Empty}).ToList();
    }

    // GET: api/v1.0/lookups/lab-types
    [HttpGet("lab-types")]
    [ProducesResponseType(typeof(IEnumerable<LookupDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LookupDto>>> GetLabTypes(string? culture)
    {
        var cultureName = culture ?? Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
        var items = await _context.LabTypes.ToListAsync();
        return items.Select(e => new LookupDto { Id = e.Id, Name = e.Name.Translate(cultureName) ?? string.Empty}).ToList();
    }

    // GET: api/v1.0/lookups/institute-types
    [HttpGet("institute-types")]
    [ProducesResponseType(typeof(IEnumerable<LookupDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LookupDto>>> GetInstituteTypes(string? culture)
    {
        var cultureName = culture ?? Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
        var items = await _context.InstituteTypes.ToListAsync();
        return items.Select(e => new LookupDto { Id = e.Id, Name = e.Name.Translate(cultureName) ?? string.Empty}).ToList();
    }

    // GET: api/v1.0/lookups/equipment-types
    [HttpGet("equipment-types")]
    [ProducesResponseType(typeof(IEnumerable<LookupDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LookupDto>>> GetEquipmentTypes(string? culture)
    {
        var cultureName = culture ?? Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
        var items = await _context.EquipmentTypes.ToListAsync();
        return items.Select(e => new LookupDto { Id = e.Id, Name = e.Name.Translate(cultureName) ?? string.Empty}).ToList();
    }

    // GET: api/v1.0/lookups/reagent-types
    [HttpGet("reagent-types")]
    [ProducesResponseType(typeof(IEnumerable<LookupDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LookupDto>>> GetReagentTypes(string? culture)
    {
        var cultureName = culture ?? Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
        var items = await _context.ReagentTypes.ToListAsync();
        return items.Select(e => new LookupDto { Id = e.Id, Name = e.Name.Translate(cultureName) ?? string.Empty }).ToList();
    }

    // GET: api/v1.0/lookups/document-types
    [HttpGet("document-types")]
    [ProducesResponseType(typeof(IEnumerable<LookupDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LookupDto>>> GetDocumentTypes(string? culture)
    {
        var cultureName = culture ?? Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
        var items = await _context.DocumentTypes.ToListAsync();
        return items.Select(e => new LookupDto { Id = e.Id, Name = e.Name.Translate(cultureName)?? string.Empty }).ToList();
    }

    // GET: api/v1.0/lookups/certification-types
    [HttpGet("certification-types")]
    [ProducesResponseType(typeof(IEnumerable<LookupDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LookupDto>>> GetCertificationTypes(string? culture)
    {
        var cultureName = culture ?? Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
        var items = await _context.CertificationTypes.ToListAsync();
        return items.Select(e => new LookupDto { Id = e.Id, Name = e.Name.Translate(cultureName) ?? string.Empty}).ToList();
    }

    // GET: api/v1.0/lookups/projects
    [HttpGet("projects")]
    [ProducesResponseType(typeof(IEnumerable<LookupDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LookupDto>>> GetProjects(string? culture)
    {
        var cultureName = culture ?? Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
        var items = await _context.Projects.ToListAsync();
        return items.Select(e => new LookupDto { Id = e.Id, Name = e.ProjectName.Translate(cultureName)?? string.Empty }).ToList();
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

    // GET: api/v1.0/lookups/institutes
    /// <summary>
    /// Returns available institutes for selection during registration
    /// </summary>
    [HttpGet("institutes")]
    [ProducesResponseType(typeof(IEnumerable<LookupDto>), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<LookupDto>>> GetInstitutes(string? culture)
    {
        var cultureName = culture ?? Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
        var items = await _context.Institutes.Where(i => i.Active).ToListAsync();
        return items.Select(e => new LookupDto { Id = e.Id, Name = e.InstituteName.Translate(cultureName)?? string.Empty }).ToList();
    }
}