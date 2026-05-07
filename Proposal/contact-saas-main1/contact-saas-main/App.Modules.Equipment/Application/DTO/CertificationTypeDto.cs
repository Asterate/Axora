public class CertificationTypeListResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class CertificationTypeResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class CreateCertificationTypeRequest
{
    public string? Name { get; set; }
}

public class UpdateCertificationTypeRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}