using App.Domain.Entities;

namespace App.Modules.Project.Application.Mapper;

public static class DocumentTypeMapper
{
    // Entity → List Response
    public static DocumentTypeListResponse ToListResponse(DocumentType entity)
        => new DocumentTypeListResponse
        {
            Id = entity.Id,
        };

    // Entity → Full Response
    public static DocumentTypeResponse ToResponse(DocumentType entity)
        => new DocumentTypeResponse
        {
            Id = entity.Id,
        };

    // Create Request → Entity
    public static DocumentType ToEntity(CreateDocumentTypeRequest request)
        => new DocumentType
        {
            Id = request.Id,
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(DocumentType entity, UpdateDocumentTypeRequest request)
    {
        entity.Id = request.Id;
    }
}