using App.Shared.Domain;

namespace App.Modules.Institute.Application.Mapper;

public static class InstituteMapper
{
    // Entity → List Response
    public static InstituteListResponse ToListResponse(Domain.Entities.Institute entity)
        => new InstituteListResponse
        {
            Id = entity.Id,
            Name = entity.InstituteName.ToString()
        };

    // Entity → Full Response
    public static InstituteResponse ToResponse(Domain.Entities.Institute entity)
        => new InstituteResponse
        {
            Id = entity.Id,
            Name = entity.InstituteName.ToString(),
        };

    // Create Request → Entity
    public static Domain.Entities.Institute ToEntity(CreateInstituteRequest request)
        => new Domain.Entities.Institute
        {
            InstituteName = new LangStr { ["en"] = request.InstituteName.ToString() ?? "" },
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(Domain.Entities.Institute entity, UpdateInstituteRequest request)
    {
        entity.InstituteName = new LangStr { ["en"] = request.InstituteName.ToString() ?? "" };
    }
}