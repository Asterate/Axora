public class EquipmentListResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class EquipmentResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class CreateEquipmentRequest
{
    public string? Name { get; set; }
}

public class UpdateEquipmentRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}