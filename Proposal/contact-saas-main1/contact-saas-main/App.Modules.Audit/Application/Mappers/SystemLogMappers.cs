using App.Modules.Audit.Application.DTO;
using App.Modules.Audit.Domain;

namespace App.Modules.Audit.Application.Mappers;

public static class SystemLogMapper
{
    // Entity → List Response
    public static SystemLogResponse ToResponse(SystemLog entity)
        => new ()
        {
            Id = entity.Id,
            Timestamp = entity.Timestamp,
            Type = entity.Type,
            Message = entity.Message,
            StatusCode = entity.StatusCode,
            UserName = entity.UserName,
        };

    // Create Request → Entity
    public static SystemLog ToEntity(CreateSystemLogRequest request)
        => new ()
        {
            Timestamp = request.Timestamp,
            Type = request.Type,
            Message = request.Message,
            StatusCode = request.StatusCode,
            UserName = request.UserName,
        };
}