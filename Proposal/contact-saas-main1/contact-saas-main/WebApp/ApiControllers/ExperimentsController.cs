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
public class ExperimentsController : ControllerBase
{
    private readonly IExperimentService _experimentService;

    public ExperimentsController(IExperimentService experimentService)
    {
        _experimentService = experimentService;
    }

    // GET: api/v1.0/experiments
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ExperimentDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ExperimentDto>>> GetExperiments()
    {
        var userId = GetUserId();
        if (userId == null) return BadRequest("Invalid user token");
        
        var experiments = await _experimentService.GetAllAsync(userId.Value);
        return Ok(experiments);
    }

    // GET: api/v1.0/experiments/{id}
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ExperimentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ExperimentDto>> GetExperiment(Guid id)
    {
        var userId = GetUserId();
        if (userId == null) return BadRequest("Invalid user token");
        
        var experiment = await _experimentService.GetByIdAsync(id, userId.Value);
        if (experiment == null) return NotFound();

        return Ok(experiment);
    }

    // POST: api/v1.0/experiments
    [HttpPost]
    [ProducesResponseType(typeof(ExperimentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ExperimentDto>> CreateExperiment([FromBody] CreateExperimentDto dto)
    {
        var userId = GetUserId();
        if (userId == null) return BadRequest("Invalid user token");

        try
        {
            var result = await _experimentService.CreateAsync(dto, userId.Value);
            return CreatedAtAction(nameof(GetExperiment), new { id = result.Id }, result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // PUT: api/v1.0/experiments/{id}
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateExperiment(Guid id, [FromBody] CreateExperimentDto dto)
    {
        var userId = GetUserId();
        if (userId == null) return BadRequest("Invalid user token");

        try
        {
            var success = await _experimentService.UpdateAsync(id, dto, userId.Value);
            if (!success) return NotFound();
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE: api/v1.0/experiments/{id}
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteExperiment(Guid id)
    {
        var userId = GetUserId();
        if (userId == null) return BadRequest("Invalid user token");

        var success = await _experimentService.DeleteAsync(id, userId.Value);
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
