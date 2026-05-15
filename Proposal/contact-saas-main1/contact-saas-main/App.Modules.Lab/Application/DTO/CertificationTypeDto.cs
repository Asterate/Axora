namespace App.Modules.Lab.Application.DTO;

public class CertificationTypeListResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}

public class CertificationTypeResponse
{
    public Guid Id { get; set; }
    public string? NameEn { get; set; }
    public string? NameEt { get; set; }
    public string? DescriptionEn { get; set; }
    public string? DescriptionEt { get; set; }
}

public class CreateCertificationTypeRequest
{
    public string? NameEn { get; set; }
    public string? NameEt { get; set; }
    public string? DescriptionEn { get; set; }
    public string? DescriptionEt { get; set; }
}

public class UpdateCertificationTypeRequest :  CreateCertificationTypeRequest
{
    public Guid Id { get; set; }
}