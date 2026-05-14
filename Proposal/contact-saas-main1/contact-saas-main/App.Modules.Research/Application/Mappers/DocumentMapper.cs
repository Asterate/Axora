using App.Domain.Entities;

namespace App.Modules.Project.Application.Mapper;

public static class DocumentMapper
{
    public static DocumentListResponse ToListResponse(Document entity)
        => new DocumentListResponse
        {
            Id = entity.Id,
            DocumentName = entity.DocumentName,
            DocumentType = entity.DocumentType?.Name,
            CreatedAt = entity.CreatedAt
        };
    public static DocumentResponse ToResponse(Document entity)
        => new ()
        {
            Id = entity.Id,
            DocumentName = entity.DocumentName,
            DocumentType = entity.DocumentType?.Name,
            Description = entity.Description,
            CreatedAt = entity.CreatedAt
        };

    // Create Request → Entity
    public static Document ToEntity(CreateDocumentRequest request)
        => new Document
        {
            Id = request.Id,
            DocumentName = request.DocumentName ?? "Document",
            FilePath = request.FilePath ?? "None",
            DocumentTypeId = request.DocumentTypeId
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(Document entity, UpdateDocumentRequest request)
    {
        entity.DocumentName = request.DocumentName ?? "Document";
        entity.FilePath = request.FilePath ?? "None";
        entity.DocumentTypeId = request.DocumentTypeId;
    }
}