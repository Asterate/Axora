using App.Modules.Equipment.Application.Interfaces;
using App.Modules.Lab.Application.DTO;
using App.Modules.Lab.Application.Interfaces;
using App.Modules.Lab.Application.Interfaces.Service;
using App.Modules.Lab.Application.Mappers;
using App.Shared.Contracts;

namespace App.Modules.Lab.Application.Services;

public class CertificationTypeService : ICertificationTypeService
{
    private readonly ICertificationTypeRepository _certificationTypeRepo;
    private readonly IUnitOfWork _uow;

    public CertificationTypeService(
        ICertificationTypeRepository certificationTypeRepo,
        IUnitOfWork uow)
    {
        _certificationTypeRepo = certificationTypeRepo;
        _uow = uow;
    }
    public async Task<IEnumerable<CertificationTypeResponse>> GetAllAsync()
    {
        var entities = await _certificationTypeRepo.GetAllAsync();
        return entities.Select(CertificationTypeMapper.ToResponse);
    }

    public async Task<CertificationTypeResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _certificationTypeRepo.GetByIdAsync(id);
        if (entity == null) return null;
        return CertificationTypeMapper.ToResponse(entity);
    }

    public async Task CreateAsync(SaveCertificationTypeRequest request)
    {
        var entity = CertificationTypeMapper.ToEntity(request);
        await _certificationTypeRepo.AddAsync(entity);
        entity.CreatedAt = DateTime.UtcNow;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task UpdateAsync(Guid id, SaveCertificationTypeRequest request)
    {
        var entity = await _certificationTypeRepo.GetByIdAsync(id);
        if (entity == null) return;
        CertificationTypeMapper.UpdateEntity(entity, request);
        _certificationTypeRepo.Update(entity);
        entity.UpdatedAt = DateTime.UtcNow;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _certificationTypeRepo.GetByIdAsync(id);
        if (entity == null) return;
        _certificationTypeRepo.Update(entity);
        entity.DeletedAt = DateTime.UtcNow;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }
    public async Task<List<LookupItem>> GetActivesAsync(string? culture = null)
    {
        var entities = await _certificationTypeRepo.GetAllAsync();
        return entities
            .Where(t => t.DeletedAt == null)
            .Select(t => new LookupItem
            {
                Id = t.Id,
                Name = t.Name.Translate(culture) ?? "???"
            }).ToList();
    }
}