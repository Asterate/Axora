using App.DTO.v1;
using App.Domain.Entities;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using App.Modules.Lab.Application.Interfaces.Service;
using App.Modules.Project.Application.Interfaces.Service;
using App.Shared.Contracts;

namespace WebApp.ApiControllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
public class LookupsController : ControllerBase
{
    private readonly IExperimentTypeService _experimentTypeService;
    private readonly IProjectTypeService _projectTypeService;
    private readonly IExperimentTaskTypeService _experimentTaskTypeService;
    private readonly IInstituteTypeService _instituteTypeService;
    private readonly IDocumentTypeService _documentTypeService;
    private readonly ILabTypeService _labTypeService;
    private readonly IEquipmentTypeService _equipmentTypeService;
    private readonly IReagentTypeService _reagentTypeService;
    private readonly ICertificationTypeService _certificationTypeService;
    private readonly IProjectService _projectService;
    private readonly IInstituteService _instituteService;

    public LookupsController(
        IExperimentTypeService experimentTypeService,
        IProjectTypeService projectTypeService,
        IExperimentTaskTypeService experimentTaskTypeService,
        IInstituteTypeService instituteTypeService,
        IDocumentTypeService documentTypeService,
        ILabTypeService labTypeService,
        IEquipmentTypeService equipmentTypeService,
        IReagentTypeService reagentTypeService,
        ICertificationTypeService certificationTypeService,
        IProjectService projectService,
        IInstituteService instituteService)
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
    public async Task<ActionResult<IEnumerable<LookupItem>>> GetExperimentTypes([FromQuery] string? culture = null)
    {
        return Ok(await _experimentTypeService.GetActivesAsync(culture));
    }

    // GET: api/v1.0/lookups/project-types
    [HttpGet("project-types")]
    [ProducesResponseType(typeof(IEnumerable<LookupItem>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LookupItem>>> GetProjectTypes([FromQuery] string? culture = null)
    {
        return Ok(await _projectTypeService.GetActivesAsync(culture));
    }

    // GET: api/v1.0/lookups/task-types
    [HttpGet("experimentTask-types")]
    [ProducesResponseType(typeof(IEnumerable<LookupItem>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LookupItem>>> GetExperimentTaskTypes([FromQuery] string? culture = null)
    {
        return Ok(await _experimentTaskTypeService.GetActivesAsync(culture));
    }

    // GET: api/v1.0/lookups/lab-types
    [HttpGet("lab-types")]
    [ProducesResponseType(typeof(IEnumerable<LookupItem>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LookupItem>>> GetLabTypes([FromQuery] string? culture = null)
    {
        return Ok(await _labTypeService.GetActivesAsync(culture));
    }

    // GET: api/v1.0/lookups/institute-types
    [HttpGet("institute-types")]
    [ProducesResponseType(typeof(IEnumerable<LookupItem>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LookupItem>>> InstituteTypes([FromQuery] string? culture = null)
    {
        return Ok(await _instituteTypeService.GetActivesAsync(culture));
    }

    // GET: api/v1.0/lookups/equipment-types
    [HttpGet("equipment-types")]
    [ProducesResponseType(typeof(IEnumerable<LookupItem>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LookupItem>>> GetEquipmentTypes([FromQuery] string? culture = null)
    {
        return Ok(await _equipmentTypeService.GetActivesAsync(culture));
    }

    // GET: api/v1.0/lookups/reagent-types
    [HttpGet("reagent-types")]
    [ProducesResponseType(typeof(IEnumerable<LookupItem>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LookupItem>>> GetReagentTypes([FromQuery] string? culture = null)
    {
        return Ok(await _reagentTypeService.GetActivesAsync(culture));
    }

    // GET: api/v1.0/lookups/document-types
    [HttpGet("document-types")]
    [ProducesResponseType(typeof(IEnumerable<LookupItem>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LookupItem>>> GetDocumentTypes([FromQuery] string? culture = null)
    {
        return Ok(await _documentTypeService.GetActivesAsync(culture));
    }

    // GET: api/v1.0/lookups/certification-types
    [HttpGet("certification-types")]
    [ProducesResponseType(typeof(IEnumerable<LookupItem>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LookupItem>>> GetCertificationTypes([FromQuery] string? culture = null)
    {
        return Ok(await _certificationTypeService.GetActivesAsync(culture));
    }

    // GET: api/v1.0/lookups/projects
    [HttpGet("projects")]
    [ProducesResponseType(typeof(IEnumerable<LookupItem>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LookupItem>>> GetProjects([FromQuery] string? culture = null)
    {
        return Ok(await _projectService.GetActivesAsync(culture));
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
    [ProducesResponseType(typeof(IEnumerable<LookupItem>), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<LookupItem>>> GetInstitutes([FromQuery] string? culture = null)
    {
        return Ok(await _instituteService.GetActivesAsync(culture));
    }
    private Guid? GetUserId()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            return null;
        return userId;
    }
}