using App.Domain.Entities;
using App.Shared.Domain;

namespace App.Modules.Project.Application.Mapper;

public static class ProjectTypeMapper
{
    // Entity → List Response
    public static ProjectTypeListResponse ToListResponse(ProjectType entity)
        => new()
        {
            Id = entity.Id,
            Name = entity.Name.Translate(),
            Description = entity.Description?.Translate()
        };

    // Entity → Full Response
    public static ProjectTypeResponse ToResponse(ProjectType entity) => new()
    {
        Id = entity.Id,
        NameEn = entity.Name.Translate("en"),
        NameEt = entity.Name.Translate("et"),
        DescriptionEn = entity.Description?.Translate("en"),
        DescriptionEt = entity.Description?.Translate("et")
    };

    // Create Request → Entity
    public static ProjectType ToEntity(CreateProjectTypeRequest request)
        => new ()
        {
            Id = request.Id,
            Name = new LangStr { ["en"] = request.NameEn ?? "", ["et"] = request.NameEt ?? "" },
            Description = request.DescriptionEn == null && request.DescriptionEt == null ? null 
                : new LangStr { ["en"] = request.DescriptionEn ?? "", ["et"] = request.DescriptionEt ?? "" }
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(ProjectType entity, UpdateProjectTypeRequest request)
    {
        entity.Id = request.Id;
        entity.Name["en"] = request.NameEn ?? "";
        entity.Name["et"] = request.NameEt ?? "";
        if (request.DescriptionEn != null || request.DescriptionEt != null)
        {
            entity.Description ??= new LangStr();
            entity.Description["en"] = request.DescriptionEn ?? "";
            entity.Description["et"] = request.DescriptionEt ?? "";
        }
    }
}