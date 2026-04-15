using System.ComponentModel.DataAnnotations;
using App.Domain;
using App.Domain.Entities;
using System.Globalization;

namespace WebApp.ViewModels;

public class EquipmentViewModel
{
    public Guid Id { get; set; }
    
    [Display(Name = "Name")]
    [StringLength(128, MinimumLength = 3)]
    public string Name { get; set; } = default!;
    
    [Display(Name = "SerialCode")]
    [StringLength(128, MinimumLength = 10)]
    public string? EquipmentSerialCode { get; set; }
    
    [Display(Name = "Manual")]
    public string? ManualFilePath { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    
    public Guid EquipmentTypeId { get; set; }
    public string? EquipmentTypeName { get; set; }
    
    /// <summary>
    /// Creates a ViewModel from an Equipment entity.
    /// </summary>
    public static EquipmentViewModel FromEntity(Equipment equipment)
    {
        return new EquipmentViewModel
        {
            Id = equipment.Id,
            // Get the translation for current UI culture, or first available translation
            Name = equipment.EquipmentName.Translate() ?? equipment.EquipmentName.FirstOrDefault().Value ?? "",
            EquipmentSerialCode = equipment.EquipmentSerialCode,
            ManualFilePath = equipment.ManualFilePath,
            CreatedAt = equipment.CreatedAt,
            UpdatedAt = equipment.UpdatedAt,
            DeletedAt = equipment.DeletedAt,
            EquipmentTypeId = equipment.EquipmentTypeId,
            EquipmentTypeName = equipment.EquipmentType?.EquipmentTypeName
        };
    }
    
    /// <summary>
    /// Converts the ViewModel to an Equipment entity.
    /// Sets the name in the current UI language's culture.
    /// </summary>
    public Equipment ToEntity()
    {
        var currentCulture = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
        
        var equipment = new Equipment
        {
            Id = this.Id,
            EquipmentName = new LangStr(),
            EquipmentSerialCode = this.EquipmentSerialCode,
            ManualFilePath = this.ManualFilePath,
            CreatedAt = this.CreatedAt,
            UpdatedAt = this.UpdatedAt,
            DeletedAt = this.DeletedAt,
            EquipmentTypeId = this.EquipmentTypeId
        };
        
        // Set the translation for the current UI language
        if (!string.IsNullOrWhiteSpace(Name))
        {
            equipment.EquipmentName[currentCulture] = Name;
        }
        
        return equipment;
    }
}