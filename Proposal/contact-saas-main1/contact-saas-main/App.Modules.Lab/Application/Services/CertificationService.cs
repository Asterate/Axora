using App.Modules.Equipment.Application.Interfaces;
using App.Modules.Lab.Application.DTO;
using App.Modules.Lab.Application.Interfaces;
using App.Modules.Lab.Application.Interfaces.Service;
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

    public async Task CreateAsync(SaveCertificationRequest request)
    {
        var entity = CertificationMapper.ToEntity(request);
        await _certificationRepo.AddAsync(entity);
        entity.CreatedAt = DateTime.UtcNow;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task UpdateAsync(Guid id, SaveCertificationRequest request)
    {
        var entity = await _certificationRepo.GetByIdAsync(id);
        if (entity == null) return;
        CertificationMapper.UpdateEntity(entity, request);
        _certificationRepo.Update(entity);
        entity.UpdatedAt = DateTime.UtcNow;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _certificationRepo.GetByIdAsync(id);
        if (entity == null) return;
        entity.DeletedAt = DateTime.UtcNow;
        _certificationRepo.Update(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }
}