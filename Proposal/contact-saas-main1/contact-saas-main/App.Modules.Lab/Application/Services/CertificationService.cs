using App.Modules.Equipment.Application.Interfaces;
using App.Modules.Lab.Application.DTO;
using App.Modules.Lab.Application.Interfaces;
using App.Modules.Lab.Application.Mappers;
using App.Shared.Contracts;

namespace App.Modules.Lab.Application.Services;

public class CertificationService :  ICertificationService
{
    private readonly ICertificationRepository _certificationRepo;
    private readonly IUnitOfWork _uow;

    public CertificationService(
        ICertificationRepository certificationRepo,
        IUnitOfWork uow)
    {
        _certificationRepo = certificationRepo;
        _uow = uow;
    }
    public async Task<IEnumerable<CertificationResponse>> GetAllAsync()
    {
        var entities = await _certificationRepo.GetAllAsync();
        return entities.Select(CertificationMapper.ToResponse);
    }

    public async Task<CertificationResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _certificationRepo.GetByIdAsync(id);
        if (entity == null) return null;
        return CertificationMapper.ToResponse(entity);
    }

    public async Task CreateAsync(CreateCertificationRequest request)
    {
        var entity = CertificationMapper.ToEntity(request);
        await _certificationRepo.AddAsync(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task UpdateAsync(Guid id, UpdateCertificationRequest request)
    {
        var entity = await _certificationRepo.GetByIdAsync(id);
        if (entity == null) return;
        CertificationMapper.UpdateEntity(entity, request);
        _certificationRepo.Update(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _certificationRepo.GetByIdAsync(id);
        if (entity == null) return;
        _certificationRepo.Delete(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }
}