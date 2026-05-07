using App.Modules.Equipment.Application.Interfaces;
using App.Modules.Equipment.Domain;

namespace App.Modules.Equipment.Application.Service;

public class CertificationService
{
    private readonly ICertificationRepository  _certificationRepo;

    public CertificationService(
        ICertificationRepository certificationRepo)
    {
        _certificationRepo = certificationRepo;
    }

    public async Task<IEnumerable<CertificationListResponse>> GetAllAsync()
    {
        var equipment = await  _certificationRepo.GetAllAsync();
        return equipment.Select(e => new CertificationListResponse
        {
            Id = e.Id,
            Name = e.CertificationName.ToString(),
        });
    }

    public async Task<CertificationResponse?> CertificationResponse(Guid id)
    {
        var equipment = await  _certificationRepo.GetByIdAsync(id);
        if (equipment == null) return null;

        return new CertificationResponse
        {
            Id = equipment.Id,
            Name = equipment.CertificationName.ToString()
        };
    }

    public async Task CreateAsync(CreateCertificationRequest dto)
    {
        var entity = new Certification
        {
            CertificationName = new Shared.Domain.LangStr { ["en"] = dto.Name ?? "" },
        };
        await  _certificationRepo.AddAsync(entity);
    }

    public async Task UpdateAsync(UpdateCertificationRequest dto)
    {
        var entity = await  _certificationRepo.GetByIdAsync(dto.Id);
        if (entity == null) return;

        entity.CertificationName = new Shared.Domain.LangStr { ["en"] = dto.Name ?? "" };

        _certificationRepo.Update(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await  _certificationRepo.GetByIdAsync(id);
        if (entity == null) return;
        _certificationRepo.Delete(entity);
    }
    
    
}