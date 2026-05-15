using System.Text.Json;
using App.Modules.Project.Application.DTO;
using App.Shared.Domain;

namespace App.Modules.Project.Application.Mappers;

public static class InstituteMapper
{
    // Entity → List Response
    public static InstituteListResponse ToListResponse(Project.Domain.Institute entity)
        => new ()
        {
            Id = entity.Id,
            InstituteTypeName = entity.InstituteType.Name,
            Active = entity.Active,
            InstituteName = entity.InstituteName,
            
        };

    // Entity → Full Response
    public static InstituteResponse ToResponse(Project.Domain.Institute entity)
        => new ()
        {
            Id = entity.Id,
            InstituteTypeName = entity.InstituteType.Name,
            Active = entity.Active,
            InstituteName = entity.InstituteName,
            InstituteAddress = entity.InstituteAddress,
            InstituteCountry =  entity.InstituteCountry,
            InstitutePhoneNumber = entity.InstitutePhoneNumber,
        };

    // Create Request → Entity
    public static Project.Domain.Institute ToEntity(CreateInstituteRequest request)
        => new ()
        {
            InstituteName = JsonSerializer.Serialize(new Dictionary<string, string> { ["en"] = request.InstituteName?.ToString() ?? "" }),
            InstituteCountry =  request.InstituteCountry,
            InstituteAddress = request.InstituteAddress ?? String.Empty,
            InstitutePhoneNumber =  request.InstitutePhoneNumber,
            InstituteTypeId =  request.InstituteTypeId,
            Active = request.Active,
        };

    // Update Request → existing Entity (modifies in place)
    public static void UpdateEntity(Project.Domain.Institute entity, UpdateInstituteRequest request)
    {
        entity.InstituteName = JsonSerializer.Serialize(new Dictionary<string, string> { ["en"] = request.InstituteName?.ToString() ?? "" });
        entity.InstituteCountry = request.InstituteCountry;
        entity.InstituteAddress = request.InstituteAddress ?? String.Empty;
        entity.InstitutePhoneNumber = request.InstitutePhoneNumber;
        entity.InstituteTypeId = request.InstituteTypeId;
        entity.Active = request.Active;
    }
    public static UpdateInstituteRequest ToUpdateRequest(Domain.Institute request)
    {
        return new UpdateInstituteRequest
        {
            Id = request.Id,
            InstituteName = JsonSerializer.Serialize(new Dictionary<string, string> { ["en"] = request.InstituteName?.ToString() ?? "" }),
            InstituteCountry =  request.InstituteCountry,
            InstituteAddress = request.InstituteAddress ?? String.Empty,
            InstitutePhoneNumber =  request.InstitutePhoneNumber,
            InstituteTypeId =  request.InstituteTypeId,
            Active = request.Active,
            
        };
    }
}