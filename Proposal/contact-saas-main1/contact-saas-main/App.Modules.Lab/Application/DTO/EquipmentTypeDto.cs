public class EquipmentTypeListResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}

public class EquipmentTypeResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class CreateEquipmentTypeRequest
{
    public string? Name { get; set; }
}

public class UpdateEquipmentTypeRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}