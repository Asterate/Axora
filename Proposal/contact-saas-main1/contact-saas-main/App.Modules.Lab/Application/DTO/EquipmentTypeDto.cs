namespace App.Modules.Lab.Application.DTO;

public class EquipmentTypeListResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}

public class EquipmentTypeResponse
{
    public Guid Id { get; set; }
    public string? NameEn { get; set; }
    public string? NameEt { get; set; }
    public string? DescriptionEn { get; set; }
    public string? DescriptionEt { get; set; }
}

public class CreateEquipmentTypeRequest
{
    public string? NameEn { get; set; }
    public string? NameEt { get; set; }
    public string? DescriptionEn { get; set; }
    public string? DescriptionEt { get; set; }
}

public class UpdateEquipmentTypeRequest :  CreateEquipmentTypeRequest
{
    public Guid Id { get; set; }
}