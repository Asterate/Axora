namespace App.Modules.Lab.Application.DTO;

public class InstituteLabResponse
{
    public Guid Id { get; set; }
    public Guid InstituteId { get; set; }
    public Guid LabId { get; set; }
    public string LabName { get; set; } = default!;
}

public class CreateInstituteLabRequest
{
    public Guid InstituteId { get; set; }
    public Guid LabId { get; set; }
}

public class UpdateInstituteLabRequest :  CreateInstituteLabRequest
{
    public Guid Id { get; set; }
}