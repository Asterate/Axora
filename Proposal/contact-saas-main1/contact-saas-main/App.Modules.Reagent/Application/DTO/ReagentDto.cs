public class ReagentListResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class ReagentResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class CreateReagentRequest
{
    public Guid Id { get; set; }
}

public class UpdateReagentRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}