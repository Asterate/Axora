namespace WebApp.ViewModels;

public class LookupDataViewModel
{
    // Certification Types
    public IEnumerable<CertificationTypeListResponse> CertificationTypes { get; set; } = new  List<CertificationTypeListResponse>();
    
    // Document Types
    public IEnumerable<DocumentTypeListResponse> DocumentTypes { get; set; } = new   List<DocumentTypeListResponse>();
    
    // Equipment Types
    public IEnumerable<EquipmentTypeListResponse> EquipmentTypes { get; set; } = new List<EquipmentTypeListResponse>();
    
    // Experiment Types
    public IEnumerable<ExperimentTypeListResponse> ExperimentTypes { get; set; } = new  List<ExperimentTypeListResponse>();
    
    // Institute Types
    public IEnumerable<InstituteTypeListResponse> InstituteTypes { get; set; } = new   List<InstituteTypeListResponse>();
    
    // Lab Types
    public IEnumerable<LabTypeListResponse> LabTypes { get; set; } = new List<LabTypeListResponse>();
    
    // Project Types
    public IEnumerable<ProjectTypeListResponse> ProjectTypes { get; set; } = new   List<ProjectTypeListResponse>();
    
    // Reagent Types
    public IEnumerable<ReagentTypeListResponse> ReagentTypes { get; set; } = new   List<ReagentTypeListResponse>();
    
    // Task Types
    public IEnumerable<ExperimentTaskTypeListResponse> TaskTypes { get; set; } = new List<ExperimentTaskTypeListResponse>();
}