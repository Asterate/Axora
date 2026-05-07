public class EquipmentCertificationTypeListResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class EquipmentCertificationTypeResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class CreateEquipmentCertificationTypeRequest
{
    public Guid Id { get; set; }
}

public class UpdateEquipmentCertificationTypeRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}