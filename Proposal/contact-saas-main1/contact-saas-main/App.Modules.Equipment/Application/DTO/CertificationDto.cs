public class CertificationListResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class CertificationResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class CreateCertificationRequest
{
    public string? Name { get; set; }
}

public class UpdateCertificationRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}