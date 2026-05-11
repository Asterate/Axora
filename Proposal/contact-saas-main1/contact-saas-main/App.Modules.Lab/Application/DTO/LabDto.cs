public class LabListResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class LabResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class CreateLabRequest
{
    public string? Name { get; set; }
}

public class UpdateLabRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}