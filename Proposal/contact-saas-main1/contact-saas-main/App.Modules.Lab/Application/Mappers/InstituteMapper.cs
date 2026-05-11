using App.Domain.Entities;

namespace App.Modules.Lab.Application.Mapper;

public static class InstituteLabMapper
{
    // Entity → List Response
    public static InstituteLabListResponse ToInstituteLabResponse(InstituteLab entity)
        => new InstituteLabListResponse
        {
            Id = entity.Id,
        };

    // Entity → Full Response
    public static InstituteLabResponse ToResponse(InstituteLab entity)
        => new InstituteLabResponse
        {
            Id = entity.Id,
        };

    // Create Request → Entity
    public static InstituteLab ToEntity(CreateInstituteLabRequest request)
        => new InstituteLab
        {
            Id = request.Id,
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(InstituteLab entity, UpdateInstituteLabRequest request)
    {
        entity.Id = request.Id;
    }
}