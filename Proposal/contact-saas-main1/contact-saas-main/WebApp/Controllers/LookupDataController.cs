using App.DAL.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.ViewModels;

namespace WebApp.Controllers;
[ApiExplorerSettings(IgnoreApi = true)]
[Authorize(Roles = "admin")]
[Route("AdminDashboard/LookupData")]
[Route("LookupData")]
public class LookupDataController : Controller
{
    private readonly CertificationTypeService _certificationTypeService;
    private readonly DocumentTypeService _documentTypeService;
    private readonly EquipmentTypeService _equipmentTypeService;
    private readonly ExperimentTypeService _experimentTypeService;
    private readonly InstituteTypeService _instituteTypeService;
    private readonly LabTypeService _labTypeService;
    private readonly ProjectTypeService _projectTypeService;
    private readonly ReagentTypeService _reagentTypeService;
    private readonly ExperimentTaskTypeService _taskTypeService;

    public LookupDataController(CertificationTypeService certificationTypeService,
        DocumentTypeService documentTypeService, EquipmentTypeService equipmentTypeService,
        ExperimentTypeService experimentTypeService, InstituteTypeService instituteTypeService,
        LabTypeService labTypeService, ProjectTypeService projectTypeService,
        ReagentTypeService reagentTypeService, ExperimentTaskTypeService taskTypeService)
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