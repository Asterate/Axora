using App.Modules.Project.Application.DTO;
using App.Shared.Domain;

namespace App.Modules.Project.Application.Mappers;

public static class ExperimentMapper
{
    // Entity → Full Response
    public static ExperimentResponse ToResponse(Project.Domain.Experiment entity)
        => new ()
        {
            Id = entity.Id,
            ExperimentName = entity.ExperimentName,
            ExperimentTypeName = entity.ExperimentType.Name,
            ProjectName = entity.Projects.ProjectName,
            InstituteUserId = entity.InstituteUserId,
            CreatedAt = entity.CreatedAt,
        };

    // Create Request → Entity
    public static Project.Domain.Experiment ToEntity(CreateExperimentRequest request)
        => new ()
        {
            ExperimentName = new LangStr { ["en"] = request.ExperimentName ?? "" },
            InstituteUserId = request.InstituteUserId,
            CreatedAt = request.CreatedAt,
            UpdatedAt =  request.UpdatedAt,
            ExperimentNotes =  request.ExperimentNotes,
            ProjectId = request.ProjectId,
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(Project.Domain.Experiment entity, UpdateExperimentRequest request)
    {
        entity.Id = request.Id;
        entity.ExperimentName = new LangStr { ["en"] = request.ExperimentName ?? "" };
        entity.InstituteUserId = request.InstituteUserId;
        entity.CreatedAt = request.CreatedAt;
        entity.UpdatedAt = request.UpdatedAt;
        entity.ExperimentNotes = request.ExperimentNotes;
        entity.ProjectId = request.ProjectId;
    }
    public static UpdateExperimentRequest ToUpdateRequest(Domain.Experiment request)
    {
        return new UpdateExperimentRequest
        {
            Id = request.Id,
            ExperimentName = new LangStr { ["en"] = request.ExperimentName ?? "" },
            InstituteUserId = request.InstituteUserId,
            CreatedAt = request.CreatedAt,
            UpdatedAt =  request.UpdatedAt,
            ExperimentNotes =  request.ExperimentNotes,
            ProjectId = request.ProjectId,
            
        };
    }
}