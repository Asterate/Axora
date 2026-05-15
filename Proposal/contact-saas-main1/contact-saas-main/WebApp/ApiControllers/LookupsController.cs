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
using App.Modules.Lab.Application.Services;
using App.Modules.Project.Application.Services;
using App.Shared.Contracts;

namespace WebApp.ApiControllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
public class LookupsController : ControllerBase
{
    private readonly ExperimentTypeService _experimentTypeService;
    private readonly ProjectTypeService _projectTypeService;
    private readonly ExperimentTaskTypeService _experimentTaskTypeService;
    private readonly InstituteTypeService _instituteTypeService;
    private readonly DocumentTypeService _documentTypeService;
    private readonly LabTypeService _labTypeService;
    private readonly EquipmentTypeService _equipmentTypeService;
    private readonly ReagentTypeService _reagentTypeService;
    private readonly CertificationTypeService _certificationTypeService;
    private readonly ProjectService _projectService;
    private readonly InstituteService _instituteService;

    public LookupsController(ExperimentTypeService experimentTypeService,
        ProjectTypeService projectTypeService, ExperimentTaskTypeService experimentTaskTypeService,
        InstituteTypeService instituteTypeService, DocumentTypeService documentTypeService, LabTypeService labTypeService,
        EquipmentTypeService equipmentTypeService, ReagentTypeService reagentTypeService, CertificationTypeService certificationTypeService,
        ProjectService projectService, InstituteService instituteService)
    {
        _experimentTypeService = experimentTypeService;
        _projectTypeService = projectTypeService;
        _experimentTaskTypeService = experimentTaskTypeService;
        _instituteTypeService = instituteTypeService;
        _documentTypeService = documentTypeService;
        _labTypeService = labTypeService;
        _equipmentTypeService = equipmentTypeService;
        _reagentTypeService = reagentTypeService;
        _certificationTypeService = certificationTypeService;
        _projectService = projectService;
        _instituteService = instituteService;
    }

    // GET: api/v1.0/lookups/experiment-types
    [HttpGet("experiment-types")]
    [ProducesResponseType(typeof(IEnumerable<LookupItem>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LookupItem>>> GetExperimentTypes(string? culture)
    {
        return Ok(await _experimentTypeService.GetActivesAsync(culture));
    }

    // GET: api/v1.0/lookups/project-types
    [HttpGet("project-types")]
    [ProducesResponseType(typeof(IEnumerable<LookupItem>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LookupItem>>> GetProjectTypes(string? culture)
    {
        return Ok(await _projectTypeService.GetActivesAsync(culture));
    }

    // GET: api/v1.0/lookups/task-types
    [HttpGet("experimentTask-types")]
    [ProducesResponseType(typeof(IEnumerable<LookupDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LookupItem>>> GetExperimentTaskTypes(string? culture)
    {
        return Ok(await _experimentTaskTypeService.GetActivesAsync(culture));
    }

    // GET: api/v1.0/lookups/lab-types
    [HttpGet("lab-types")]
    [ProducesResponseType(typeof(IEnumerable<LookupDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LookupItem>>> GetLabTypes(string? culture)
    {
        return Ok(await _labTypeService.GetActivesAsync(culture));
    }

    // GET: api/v1.0/lookups/institute-types
    [HttpGet("institute-types")]
    [ProducesResponseType(typeof(IEnumerable<LookupDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LookupItem>>> InstituteTypes(string? culture)
    {
        return Ok(await _instituteTypeService.GetActivesAsync(culture));
    }

    // GET: api/v1.0/lookups/equipment-types
    [HttpGet("equipment-types")]
    [ProducesResponseType(typeof(IEnumerable<LookupDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LookupDto>>> GetEquipmentTypes(string? culture)
    {
        return Ok(await _equipmentTypeService.GetActivesAsync(culture));
    }

    // GET: api/v1.0/lookups/reagent-types
    [HttpGet("reagent-types")]
    [ProducesResponseType(typeof(IEnumerable<LookupDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LookupDto>>> GetReagentTypes(string? culture)
    {
        return Ok(await _reagentTypeService.GetActivesAsync(culture));
    }

    // GET: api/v1.0/lookups/document-types
    [HttpGet("document-types")]
    [ProducesResponseType(typeof(IEnumerable<LookupDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LookupDto>>> GetDocumentTypes(string? culture)
    {
        return Ok(await _documentTypeService.GetActivesAsync(culture));
    }

    // GET: api/v1.0/lookups/certification-types
    [HttpGet("certification-types")]
    [ProducesResponseType(typeof(IEnumerable<LookupDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LookupDto>>> GetCertificationTypes(string? culture)
    {
        return Ok(await _certificationTypeService.GetActivesAsync(culture));
    }

    // GET: api/v1.0/lookups/projects
    [HttpGet("projects")]
    [ProducesResponseType(typeof(IEnumerable<LookupDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LookupDto>>> GetProjects(string? culture)
    {
        return Ok(await _projectService.GetActivesAsync(culture));
    }

    // GET: api/v1.0/lookups/institute-users
   /* [HttpGet("institute-users")]
    [ProducesResponseType(typeof(IEnumerable<LookupDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LookupDto>>> GetInstituteUsers()
    {
        return await _context.InstituteUsers
            .Include(e => e.User)
            .Select(e => new LookupDto { Id = e.Id, Name = e.User.FirstName + " " + e.User.LastName })
            .ToListAsync();
    }*/

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
        return Ok(await _instituteService.GetActivesAsync(culture));
    }
}