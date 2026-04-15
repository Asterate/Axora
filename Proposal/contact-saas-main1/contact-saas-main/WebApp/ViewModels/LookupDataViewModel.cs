using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using App.Domain.Entities;

namespace WebApp.ViewModels;

public class LookupDataViewModel
{
    // Certification Types
    public List<CertificationType> CertificationTypes { get; set; } = new();
    
    // Document Types
    public List<DocumentType> DocumentTypes { get; set; } = new();
    
    // Equipment Types
    public List<EquipmentType> EquipmentTypes { get; set; } = new();
    
    // Experiment Types
    public List<ExperimentType> ExperimentTypes { get; set; } = new();
    
    // Institute Types
    public List<InstituteType> InstituteTypes { get; set; } = new();
    
    // Lab Types
    public List<LabType> LabTypes { get; set; } = new();
    
    // Project Types
    public List<ProjectType> ProjectTypes { get; set; } = new();
    
    // Reagent Types
    public List<ReagentType> ReagentTypes { get; set; } = new();
    
    // Task Types
    public List<TaskType> TaskTypes { get; set; } = new();
}

public class LookupItemViewModel
{
    public Guid Id { get; set; }
    
    [Display(Name = "Name")]
    public string Name { get; set; } = default!;
    
    [Display(Name = "Description")]
    public string? Description { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}