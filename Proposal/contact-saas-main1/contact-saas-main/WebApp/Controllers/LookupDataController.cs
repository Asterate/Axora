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
        var certificationTypes = _certificationTypeService.GetAllAsync();
        var documentTypes = _documentTypeService.GetAllAsync();
        var equipmentTypes = _equipmentTypeService.GetAllAsync();
        var experimentTypes = _experimentTypeService.GetAllAsync();
        var instituteTypes = _instituteTypeService.GetAllAsync();
        var labTypes = _labTypeService.GetAllAsync();
        var projectTypes = _projectTypeService.GetAllAsync();
        var reagentTypes = _reagentTypeService.GetAllAsync();
        var taskTypes = _taskTypeService.GetAllAsync();

        await Task.WhenAll(certificationTypes, documentTypes, equipmentTypes, 
            experimentTypes, instituteTypes, labTypes, 
            projectTypes, reagentTypes, taskTypes);

        var model = new LookupDataViewModel
        {
            CertificationTypes = certificationTypes.Result,
            DocumentTypes = documentTypes.Result,
            EquipmentTypes = equipmentTypes.Result,
            ExperimentTypes = experimentTypes.Result,
            InstituteTypes = instituteTypes.Result,
            LabTypes = labTypes.Result,
            ProjectTypes = projectTypes.Result,
            ReagentTypes = reagentTypes.Result,
            TaskTypes = taskTypes.Result
        };

        return View(model);
    }
}