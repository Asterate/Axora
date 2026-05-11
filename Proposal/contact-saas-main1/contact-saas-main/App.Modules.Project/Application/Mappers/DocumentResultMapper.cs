using App.Domain.Entities;

namespace App.Modules.Project.Application.Mapper;

public static class DocumentResultMapper
{
    // Entity → List Response
    public static DocumentResultListResponse ToEquipmentResultLabResponse(DocumentResult entity)
        => new DocumentResultListResponse
        {
            Id = entity.Id,
        };

    // Entity → Full Response
    public static DocumentResultResponse ToResponse(DocumentResult entity)
        => new DocumentResultResponse
        {
            Id = entity.Id,
        };

    // Create Request → Entity
    public static DocumentResult ToEntity(CreateDocumentResultRequest request)
        => new DocumentResult
        {
            Id = request.Id,
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(DocumentResult entity, UpdateDocumentResultRequest request)
    {
        entity.Id = request.Id;
    }
}