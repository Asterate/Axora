namespace App.Modules.Lab.Application.DTO;

public class ReagentLabResponse
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public string Unit { get; set; }  = default!;
    public Guid LabId { get; set; }
    public string? LabName { get; set; }
    public Guid ReagentId { get; set; }
    public string? ReagentName { get; set; }
}

public class CreateReagentLabRequest
{
    public int Quantity { get; set; }
    public string Unit { get; set; }  = default!;
    public Guid LabId { get; set; }
    public Guid ReagentId { get; set; }
}

public class UpdateReagentLabRequest :  CreateReagentLabRequest
{
    public Guid Id { get; set; }
}