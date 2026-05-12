using App.Domain.Entities;

public class ScheduleListResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class ScheduleResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class CreateScheduleRequest
{
    public Guid Id { get; set; }
}

public class UpdateScheduleRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }

    public UpdateScheduleRequest(Schedule schedule)
    {
        Id = schedule.Id;
    }
}