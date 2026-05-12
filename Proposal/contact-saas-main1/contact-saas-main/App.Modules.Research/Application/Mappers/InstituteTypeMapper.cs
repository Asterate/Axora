using App.Domain.Entities;
using App.Shared.Domain;

namespace App.Modules.Institute.Application.Mapper;

public static class InstituteTypeMapper
{
    // Entity → List Response
    public static InstituteTypeListResponse ToListResponse(InstituteType entity)
        => new InstituteTypeListResponse
        {
            Id = entity.Id,
            Name = entity.Name.ToString()
        };

    // Entity → Full Response
    public static InstituteTypeResponse ToResponse(InstituteType entity)
        => new InstituteTypeResponse
        {
            Id = entity.Id,
            Name = entity.Name.ToString(),
        };

    // Create Request → Entity
    public static InstituteType ToEntity(CreateInstituteTypeRequest request)
        => new InstituteType
        {
            Name = new LangStr { ["en"] = request.Name ?? "" },
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(InstituteType entity, UpdateInstituteTypeRequest request)
    {
        entity.Name = new LangStr { ["en"] = request.Name ?? "" };
    }
}