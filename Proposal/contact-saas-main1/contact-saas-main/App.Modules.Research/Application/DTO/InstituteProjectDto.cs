namespace App.Modules.Project.Application.DTO;

public class InstituteProjectResponse
{
    public Guid Id { get; set; }
    public string InstituteName { get; set; } =  "??";
    public Guid ProjectId { get; set; }
}

public class SaveInstituteProjectRequest
{
    public Guid InstituteId { get; set; }
    public Guid ProjectId { get; set; }
}