using System.ComponentModel.DataAnnotations;
using App.Domain;
using App.Domain.Entities;
using App.Modules.Equipment.Domain;
using App.Shared.Domain;

namespace WebApp.ViewModels;

public class EquipmentViewModel
{
    public Guid Id { get; set; }

    [Display(Name = "Name")]
    [StringLength(128, MinimumLength = 3)]
    public string NameEn { get; set; } = default!;

    [Display(Name = "Name")]
    [StringLength(128, MinimumLength = 3)]
    public string NameEt { get; set; } = default!;

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

    public static EquipmentViewModel FromEntity(Equipment equipment)
    {
        return new EquipmentViewModel
        {
            Id = equipment.Id,
            NameEn = equipment.EquipmentName.Translate("en") ?? string.Empty,
            NameEt = equipment.EquipmentName.Translate("et") ?? string.Empty,
            EquipmentSerialCode = equipment.EquipmentSerialCode,
            ManualFilePath = equipment.ManualFilePath,
            CreatedAt = equipment.CreatedAt,
            UpdatedAt = equipment.UpdatedAt,
            DeletedAt = equipment.DeletedAt,
            EquipmentTypeId = equipment.EquipmentTypeId,
            EquipmentTypeName = equipment.EquipmentType?.Name
        };
    }

    public void ApplyTo(Equipment equipment)
    {
        equipment.EquipmentName ??= new LangStr();
        equipment.EquipmentName.SetTranslation(NameEn ?? string.Empty, "en");
        equipment.EquipmentName.SetTranslation(NameEt ?? string.Empty, "et");
        equipment.EquipmentSerialCode = EquipmentSerialCode;
        equipment.ManualFilePath = ManualFilePath;
        equipment.UpdatedAt = UpdatedAt;
        equipment.DeletedAt = DeletedAt;
        equipment.EquipmentTypeId = EquipmentTypeId;
    }

    public Equipment ToEntity()
    {
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
        equipment.EquipmentName.SetTranslation(NameEn ?? string.Empty, "en");
        equipment.EquipmentName.SetTranslation(NameEt ?? string.Empty, "et");
        return equipment;
    }
}
