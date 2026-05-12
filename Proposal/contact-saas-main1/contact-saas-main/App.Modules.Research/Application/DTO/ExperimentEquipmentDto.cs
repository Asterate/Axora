public class ExperimentEquipmentListResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class ExperimentEquipmentResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class CreateExperimentEquipmentRequest
{
    public Guid Id { get; set; }
}

public class UpdateExperimentEquipmentRequest
{
    public Guid Id { get; set; }
}