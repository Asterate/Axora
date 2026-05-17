using App.Modules.Lab.Application.Interfaces.Service;
using App.Modules.Lab.Application.Services;
using App.Modules.Project.Application.Interfaces.Service;
using App.Modules.Project.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

namespace WebApp.Controllers;
[ApiExplorerSettings(IgnoreApi = true)]
[Authorize(Roles = "admin")]
[Route("AdminDashboard/LookupData")]
[Route("LookupData")]
public class LookupDataController : Controller
{
    private readonly ICertificationTypeService _certificationTypeService;
    private readonly IDocumentTypeService _documentTypeService;
    private readonly IEquipmentTypeService _equipmentTypeService;
    private readonly IExperimentTypeService _experimentTypeService;
    private readonly IInstituteTypeService _instituteTypeService;
    private readonly ILabTypeService _labTypeService;
    private readonly IProjectTypeService _projectTypeService;
    private readonly IReagentTypeService _reagentTypeService;
    private readonly IExperimentTaskTypeService _taskTypeService;

    public LookupDataController(ICertificationTypeService certificationTypeService,
        IDocumentTypeService documentTypeService, IEquipmentTypeService equipmentTypeService,
        IExperimentTypeService experimentTypeService, IInstituteTypeService instituteTypeService,
        ILabTypeService labTypeService, IProjectTypeService projectTypeService,
        IReagentTypeService reagentTypeService, IExperimentTaskTypeService taskTypeService)
    {
        _certificationTypeService = certificationTypeService;
        _documentTypeService = documentTypeService;
        _equipmentTypeService = equipmentTypeService;
        _experimentTypeService = experimentTypeService;
        _instituteTypeService = instituteTypeService;
        _labTypeService = labTypeService;
        _projectTypeService = projectTypeService;
        _reagentTypeService = reagentTypeService;
        _taskTypeService = taskTypeService;
    }

    public async Task<IActionResult> Index()
    {
        var model = new LookupDataViewModel
        {
            CertificationTypes = await _certificationTypeService.GetAllAsync(),
            DocumentTypes = await _documentTypeService.GetAllAsync(),
            EquipmentTypes = await _equipmentTypeService.GetAllAsync(),
            ExperimentTypes = await _experimentTypeService.GetAllAsync(),
            InstituteTypes = await _instituteTypeService.GetAllAsync(),
            LabTypes = await _labTypeService.GetAllAsync(),
            ProjectTypes = await _projectTypeService.GetAllAsync(),
            ReagentTypes = await _reagentTypeService.GetAllAsync(),
            TaskTypes = await _taskTypeService.GetAllAsync()
        };

        return View(model);
    }
}