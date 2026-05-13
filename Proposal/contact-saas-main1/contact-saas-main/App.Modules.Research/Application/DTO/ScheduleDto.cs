using App.Domain.Entities;

public class ScheduleListResponse
{
    public Guid Id { get; set; }
    public string? ScheduleName { get; set; }
    public string? Status { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string? ExperimentTask { get; set; }
    public string? Lab { get; set; }
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