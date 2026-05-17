using System.Text.Json;
using App.Domain.Entities;
using App.Modules.Project.Application.DTO;
using App.Modules.Project.Domain;
using App.Shared.Domain;
using App.Shared.Helpers;

namespace App.Modules.Project.Application.Mapper;

public static class ProjectTypeMapper
{
    // Entity → List Response
    public static ProjectTypeResponse ToResponse(ProjectType entity)
        => new()
        {
            Id = entity.Id,
            Name = entity.Name.Translate(),
            Description = entity.Description?.Translate()
        };

    // Create Request → Entity
    public static ProjectType ToEntity(SaveProjectTypeRequest request)
        => new ()
        {
            Name = new LangStr()
            {
                [Cultures.English] =  request.NameEn,
                [Cultures.Estonian] =   request.NameEt,
            },
            Description = new LangStr()
            {
                [Cultures.English] =  request.DescriptionEn ?? String.Empty,
                [Cultures.Estonian] =   request.DescriptionEt ?? String.Empty,
            }
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(ProjectType entity, SaveProjectTypeRequest request)
    {
        entity.Name.SetTranslation(request.NameEn, Cultures.English);
        entity.Description ??= new LangStr();
        entity.Description.SetTranslation(request.DescriptionEt ?? String.Empty, Cultures.Estonian);
        entity.Description.SetTranslation(request.DescriptionEn ?? String.Empty, Cultures.English);
    }
}