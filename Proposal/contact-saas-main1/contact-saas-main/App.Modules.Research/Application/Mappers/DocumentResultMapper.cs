using App.Modules.Project.Application.DTO;
using App.Modules.Project.Domain;

namespace App.Modules.Project.Application.Mappers;

public static class DocumentResultMapper
{
   // Entity → Full Response
    public static DocumentResultResponse ToResponse(DocumentResult entity)
        => new ()
        {
            Id = entity.Id,
            ResultName = entity.Result.ResultName,
            DocumentName = entity.Document.DocumentName,
        };

    // Create Request → Entity
    public static DocumentResult ToEntity(CreateDocumentResultRequest request)
        => new ()
        {
            ResultId =  request.ResultId,
            DocumentId = request.DocumentId,
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(DocumentResult entity, UpdateDocumentResultRequest request)
    {
        entity.Id = request.Id;
        entity.ResultId = request.ResultId;
        entity.DocumentId = request.DocumentId;
    }
}