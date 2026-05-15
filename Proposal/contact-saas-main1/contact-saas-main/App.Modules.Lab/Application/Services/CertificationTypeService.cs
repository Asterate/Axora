using App.Modules.Equipment.Application.Interfaces;
using App.Modules.Lab.Application.DTO;
using App.Modules.Lab.Application.Interfaces;
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
    public async Task<IEnumerable<CertificationTypeListResponse>> GetAllAsync()
    {
        var entities = await _certificationTypeRepo.GetAllAsync();
        return entities.Select(CertificationTypeMapper.ToListResponse);
    }

    public async Task<CertificationTypeResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _certificationTypeRepo.GetByIdAsync(id);
        if (entity == null) return null;
        return CertificationTypeMapper.ToResponse(entity);
    }

    public async Task CreateAsync(CreateCertificationTypeRequest request)
    {
        var entity = CertificationTypeMapper.ToEntity(request);
        await _certificationTypeRepo.AddAsync(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task UpdateAsync(Guid id, UpdateCertificationTypeRequest request)
    {
        var entity = await _certificationTypeRepo.GetByIdAsync(id);
        if (entity == null) return;
        CertificationTypeMapper.UpdateEntity(entity, request);
        _certificationTypeRepo.Update(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _certificationTypeRepo.GetByIdAsync(id);
        if (entity == null) return;
        _certificationTypeRepo.Delete(entity);
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
                Name = t.GetName(culture) ?? "???"
            }).ToList();
    }
}