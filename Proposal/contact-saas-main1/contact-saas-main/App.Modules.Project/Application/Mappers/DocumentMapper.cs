using App.Domain.Entities;

namespace App.Modules.Project.Application.Mapper;

public static class DocumentMapper
{
    // Entity → List Response
    public static DocumentListResponse ToListResponse(Document entity)
        => new DocumentListResponse
        {
            Id = entity.Id,
            DocumentName = entity.DocumentName,
            DocumentType = entity.DocumentType?.Name.ToString()
        };

    // Entity → Full Response
    public static DocumentResponse ToResponse(Document entity)
        => new DocumentResponse
        {
            Id = entity.Id,
        };

    // Create Request → Entity
    public static Document ToEntity(CreateDocumentRequest request)
        => new Document
        {
            Id = request.Id,
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(Document entity, UpdateDocumentRequest request)
    {
        entity.Id = request.Id;
    }
}