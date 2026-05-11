public class LabTypeListResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class LabTypeResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class CreateLabTypeRequest
{
    public string? Name { get; set; }
}

public class UpdateLabTypeRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}