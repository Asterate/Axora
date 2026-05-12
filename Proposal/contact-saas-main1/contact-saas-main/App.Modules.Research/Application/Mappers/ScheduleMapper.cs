using App.Domain.Entities;

namespace App.Modules.Project.Application.Mapper;

public static class ScheduleMapper
{
    // Entity → List Response
    public static ScheduleListResponse ToListResponse(Schedule entity)
        => new ScheduleListResponse
        {
            Id = entity.Id,
        };

    // Entity → Full Response
    public static ScheduleResponse ToResponse(Schedule entity)
        => new ScheduleResponse
        {
            Id = entity.Id,
        };

    // Create Request → Entity
    public static Schedule ToEntity(CreateScheduleRequest request)
        => new Schedule
        {
            Id = request.Id,
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(Schedule entity, UpdateScheduleRequest request)
    {
        entity.Id = request.Id;
    }
}