using App.Modules.Lab.Application.DTO;
using App.Modules.Project.Application.DTO;

namespace WebApp.ViewModels;

public class LookupDataViewModel
{
    // Certification Types
    public IEnumerable<CertificationTypeResponse> CertificationTypes { get; set; } = new  List<CertificationTypeResponse>();
    
    // Document Types
    public IEnumerable<DocumentTypeResponse> DocumentTypes { get; set; } = new   List<DocumentTypeResponse>();
    
    // Equipment Types
    public IEnumerable<EquipmentTypeResponse> EquipmentTypes { get; set; } = new List<EquipmentTypeResponse>();
    
    // Experiment Types
    public IEnumerable<ExperimentTypeResponse> ExperimentTypes { get; set; } = new  List<ExperimentTypeResponse>();
    
    // Institute Types
    public IEnumerable<InstituteTypeResponse> InstituteTypes { get; set; } = new   List<InstituteTypeResponse>();
    
    // Lab Types
    public IEnumerable<LabTypeResponse> LabTypes { get; set; } = new List<LabTypeResponse>();
    
    // Project Types
    public IEnumerable<ProjectTypeResponse> ProjectTypes { get; set; } = new   List<ProjectTypeResponse>();
    
    // Reagent Types
    public IEnumerable<ReagentTypeResponse> ReagentTypes { get; set; } = new   List<ReagentTypeResponse>();
    
    // Task Types
    public IEnumerable<ExperimentTaskTypeResponse> TaskTypes { get; set; } = new List<ExperimentTaskTypeResponse>();
}