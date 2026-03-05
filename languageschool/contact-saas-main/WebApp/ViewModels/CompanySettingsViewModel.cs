using App.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class CompanySettingsViewModel
{
    
    public List<Language> Languages { get; set; } = new List<Language>();
    public List<SelectListItem> Levels { get; set; } = default!;
    public string NewLanguageName { get; set; } = string.Empty; // user types this
    public string NewLanguageDescription { get; set; } = string.Empty; // user types this
    public Guid? NewLanguageLevelId { get; set; } 
    public CompanyConfig CompanyConfig { get; set; } = default!;
    public Company Company { get; set; } = default!;
    public Guid? EditLanguage { get; set; }
}