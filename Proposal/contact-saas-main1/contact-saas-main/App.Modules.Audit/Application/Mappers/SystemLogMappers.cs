using App.Domain.Entities;

namespace App.Modules.Experiment.Application.Mapper;

public static class SystemLogMapper
{
    // Entity → List Response
    public static SystemLogListResponse ToListResponse(SystemLog entity)
        => new SystemLogListResponse
        {
            Id = entity.Id,
        };

    // Entity → Full Response
    public static SystemLogResponse ToResponse(SystemLog entity)
        => new SystemLogResponse
        {
            Id = entity.Id,
        };

    // Create Request → Entity
    public static SystemLog ToEntity(CreateSystemLogRequest request)
        => new SystemLog
        {
            Id = request.Id,
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(SystemLog entity, UpdateSystemLogRequest request)
    {
        request.Id = entity.Id;
    }
}