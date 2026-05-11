public class ReagentLabListResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class ReagentLabResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class CreateReagentLabRequest
{
    public Guid Id { get; set; }
}

public class UpdateReagentLabRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}