using App.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class EnrollmentViewModel
{
    public Guid CourseId { get; set; }
    public Course? Course { get; set; }
    
    // Student information
    public string StudentFirstName { get; set; } = default!;
    public string StudentLastName { get; set; } = default!;
    public string StudentEmail { get; set; } = default!;
    public string StudentPhone { get; set; } = default!;
    public string StudentAddress { get; set; } = default!;
    public string StudentNativeLanguage { get; set; } = default!;
    public string StudentNationality { get; set; } = default!;
    public string StudentGender { get; set; } = default!;
    
    // Subscription plan
    public EEnrollmentPlan EnrollmentPlan { get; set; } = EEnrollmentPlan.Free;
    public List<SelectListItem> EnrollmentPlans { get; set; } = new List<SelectListItem>();
    
    // Subscription information
    public Subs? Subs { get; set; }
}
