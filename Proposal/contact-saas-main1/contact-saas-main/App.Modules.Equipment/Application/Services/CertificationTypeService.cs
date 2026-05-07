using App.Modules.Equipment.Application.Interfaces;
using App.Modules.Equipment.Domain;

namespace App.Modules.Equipment.Application.Service;

public class CertificationTypeService
{
    private readonly ICertificationTypeRepository  _certificationTypeRepo;

    public CertificationTypeService(
        ICertificationTypeRepository certificationTypeRepo)
    {
        _certificationTypeRepo = certificationTypeRepo;
    }

    public async Task<IEnumerable<CertificationTypeListResponse>> GetAllAsync()
    {
        var equipment = await  _certificationTypeRepo.GetAllAsync();
        return equipment.Select(e => new CertificationTypeListResponse
        {
            Id = e.Id,
            Name = e.Name.ToString(),
        });
    }

    public async Task<CertificationTypeResponse?> CertificationTypeResponse(Guid id)
    {
        var equipment = await  _certificationTypeRepo.GetByIdAsync(id);
        if (equipment == null) return null;

        return new CertificationTypeResponse
        {
            Id = equipment.Id,
            Name = equipment.Name.ToString()
        };
    }

    public async Task CreateAsync(CreateCertificationTypeRequest dto)
    {
        var entity = new CertificationType
        {
            Name = new Shared.Domain.LangStr { ["en"] = dto.Name ?? "" },
        };
        await  _certificationTypeRepo.AddAsync(entity);
    }

    public async Task UpdateAsync(UpdateCertificationTypeRequest dto)
    {
        var entity = await  _certificationTypeRepo.GetByIdAsync(dto.Id);
        if (entity == null) return;

        entity.Name = new Shared.Domain.LangStr { ["en"] = dto.Name ?? "" };

        _certificationTypeRepo.Update(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await  _certificationTypeRepo.GetByIdAsync(id);
        if (entity == null) return;
        _certificationTypeRepo.Delete(entity);
    }
    
    
}