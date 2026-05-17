using App.Modules.Project.Application.DTO;
using App.Modules.Project.Domain;
using App.Shared.Domain;
using App.Shared.Helpers;

namespace App.Modules.Project.Application.Mappers;

public static class InstituteTypeMapper
{
    // Entity → List Response
    public static InstituteTypeResponse ToResponse(InstituteType entity)
        => new ()
        {
            Id = entity.Id,
            Name = entity.Name.Translate(),
            Description = entity.Description?.Translate()
        };
    

    // Create Request → Entity
    public static InstituteType ToEntity(SaveInstituteTypeRequest request)
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
    public static void UpdateEntity(InstituteType entity, SaveInstituteTypeRequest request)
    {
        entity.Name.SetTranslation(request.NameEn, Cultures.English);
        entity.Description ??= new LangStr();
        entity.Description.SetTranslation(request.DescriptionEt ?? String.Empty, Cultures.Estonian);
        entity.Description.SetTranslation(request.DescriptionEn ?? String.Empty, Cultures.English);
    }
    public static SaveInstituteTypeRequest ToUpdateRequest(InstituteTypeResponse entity)
    {
        return new SaveInstituteTypeRequest
        {
            //issue here
            NameEn = entity.Name ?? String.Empty,
            NameEt = entity.Name ?? String.Empty,
            DescriptionEn = entity.Description ?? String.Empty,
            DescriptionEt = entity.Description ?? String.Empty,
        };
    }
}