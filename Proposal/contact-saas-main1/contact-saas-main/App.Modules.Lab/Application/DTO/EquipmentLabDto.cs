public class EquipmentLabListResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class EquipmentLabResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class CreateEquipmentLabRequest
{
    public Guid Id { get; set; }
}

public class UpdateEquipmentLabRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}