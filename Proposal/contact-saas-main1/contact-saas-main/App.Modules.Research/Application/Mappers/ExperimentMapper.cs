using App.Modules.Project.Application.DTO;
using App.Shared.Domain;
using App.Shared.Helpers;

namespace App.Modules.Project.Application.Mappers;

public static class ExperimentMapper
{
    // Entity → Full Response
    public static ExperimentResponse ToResponse(Project.Domain.Experiment entity)
        => new ()
        {
            Id = entity.Id,
            ExperimentName = entity.ExperimentName.Translate(),
            ExperimentTypeName = entity.ExperimentType.Name.Translate(),
            ProjectName = entity.Projects.ProjectName.Translate() ?? String.Empty,
            InstituteUserId = entity.InstituteUserId,
        };

    // Create Request → Entity
    public static Project.Domain.Experiment ToEntity(SaveExperimentRequest request)
        => new ()
        {
            ExperimentName = new LangStr
            {
                [Cultures.English] = request.ExperimentNameEn,
                [Cultures.Estonian] = request.ExperimentNameEt,
            },
            InstituteUserId = request.InstituteUserId,
            ExperimentNotes =  new LangStr
            {
                [Cultures.English] = request.ExperimentNotesEn,
                [Cultures.Estonian] = request.ExperimentNotesEt,
            },
            ProjectId = request.ProjectId,
            ExperimentTypeId = request.ExperimentTypeId,
        };
    public static SaveExperimentRequest ToRequest(Domain.Experiment entity) => new()
    {
        ExperimentTypeId = entity.ExperimentTypeId,
        ExperimentNameEn = entity.ExperimentName[Cultures.English],
        ExperimentNameEt = entity.ExperimentName[Cultures.Estonian],
        ExperimentNotesEn = entity.ExperimentNotes[Cultures.English],
        ExperimentNotesEt = entity.ExperimentNotes[Cultures.Estonian],
        ProjectId = entity.ProjectId,
        InstituteUserId = entity.InstituteUserId
    };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(Project.Domain.Experiment entity, SaveExperimentRequest request)
    {
        entity.ExperimentName.SetTranslation(request.ExperimentNameEn, Cultures.English);
        entity.ExperimentName.SetTranslation(request.ExperimentNameEt, Cultures.Estonian);
        entity.InstituteUserId = request.InstituteUserId;
        entity.ExperimentNotes.SetTranslation(request.ExperimentNotesEn, Cultures.English);
        entity.ExperimentNotes.SetTranslation(request.ExperimentNotesEt, Cultures.Estonian);
        entity.ProjectId = request.ProjectId;
        entity.ExperimentTypeId = request.ExperimentTypeId;
    }
    public static SaveExperimentRequest ToUpdateRequest(Domain.Experiment request)
    {
        return new SaveExperimentRequest
        {
            ExperimentNameEn = request.ExperimentName.Translate(Cultures.English) ?? String.Empty,
            ExperimentNameEt = request.ExperimentName.Translate(Cultures.Estonian) ?? String.Empty,
            InstituteUserId = request.InstituteUserId,
            ExperimentNotesEn =  request.ExperimentNotes.Translate(Cultures.English) ?? String.Empty,
            ExperimentNotesEt = request.ExperimentNotes.Translate(Cultures.Estonian) ?? String.Empty,
            ProjectId = request.ProjectId,
            ExperimentTypeId = request.ExperimentTypeId,
            
        };
    }
}