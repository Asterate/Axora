public class ReagentTypeListResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}

public class ReagentTypeResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class CreateReagentTypeRequest
{
    public Guid Id { get; set; }
}

public class UpdateReagentTypeRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}