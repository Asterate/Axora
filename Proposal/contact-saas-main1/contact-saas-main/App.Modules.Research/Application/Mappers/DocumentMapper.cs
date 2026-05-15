using App.Modules.Project.Application.DTO;
using App.Modules.Project.Domain;

namespace App.Modules.Project.Application.Mappers;

public static class DocumentMapper
{
    public static DocumentListResponse ToListResponse(Document entity)
        => new ()
        {
            Id = entity.Id,
            DocumentName = entity.DocumentName,
            DocumentTypeName = entity.DocumentType?.Name,
            CreatedAt = entity.CreatedAt
        };
    public static DocumentResponse ToResponse(Document entity)
        => new ()
        {
            Id = entity.Id,
            DocumentName = entity.DocumentName,
            DocumentTypeName = entity.DocumentType?.Name,
            Description = entity.Description,
            CreatedAt = entity.CreatedAt,
            FilePath =  entity.FilePath,
            UpdatedAt = entity.UpdatedAt,
        };

    // Create Request → Entity
    public static Document ToEntity(CreateDocumentRequest request)
        => new ()
        {
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