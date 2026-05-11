public class ResultListResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class ResultResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class CreateResultRequest
{
    public Guid Id { get; set; }
}

public class UpdateResultRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}