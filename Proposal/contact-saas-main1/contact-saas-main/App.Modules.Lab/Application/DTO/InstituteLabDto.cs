namespace App.Modules.Lab.Application.DTO;

public class InstituteLabResponse
{
    public Guid Id { get; set; }
    public Guid InstituteId { get; set; }
    public Guid LabId { get; set; }
    public string LabName { get; set; } = default!;
}

public class SaveInstituteLabRequest
{
    public Guid InstituteId { get; set; }
    public Guid LabId { get; set; }
}