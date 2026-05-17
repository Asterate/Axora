using App.Modules.Institute.Application.Interfaces;
using App.Modules.Project.Application.DTO;
using App.Modules.Project.Application.Interfaces.Service;
using App.Modules.Project.Application.Mappers;
using App.Shared.Contracts;

namespace App.Modules.Project.Application.Services;

public class InstituteTypeService : IInstituteTypeService
{
    private readonly IInstituteTypeRepository _instituteTypeRepo;
    private readonly IUnitOfWork _uow;

    public InstituteTypeService(
        IInstituteTypeRepository instituteTypeRepo,
        IUnitOfWork uow)
    {
        _instituteTypeRepo = instituteTypeRepo;
        _uow = uow;
    }
    public async Task<IEnumerable<InstituteTypeResponse>> GetAllAsync()
    {
        var entities = await _instituteTypeRepo.GetAllAsync();
        return entities.Select(InstituteTypeMapper.ToResponse);
    }

    public async Task<InstituteTypeResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _instituteTypeRepo.GetByIdAsync(id);
        if (entity == null) return null;
        return InstituteTypeMapper.ToResponse(entity);
    }

    public async Task CreateAsync(SaveInstituteTypeRequest request)
    {
        var entity = InstituteTypeMapper.ToEntity(request);
        await _instituteTypeRepo.AddAsync(entity);
        entity.CreatedAt = DateTime.UtcNow;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task UpdateAsync(Guid id, SaveInstituteTypeRequest request)
    {
        var entity = await _instituteTypeRepo.GetByIdAsync(id);
        if (entity == null) return;
        InstituteTypeMapper.UpdateEntity(entity, request);
        _instituteTypeRepo.Update(entity);
        entity.UpdatedAt = DateTime.UtcNow;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _instituteTypeRepo.GetByIdAsync(id);
        if (entity == null) return;
        _instituteTypeRepo.Update(entity);
        entity.DeletedAt = DateTime.UtcNow;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }
    
    public async Task<List<LookupItem>> GetActivesAsync(string? culture = null)
    {
        var entities = await _instituteTypeRepo.GetActivesAsync();
        return entities.Select(i => new LookupItem 
        { 
            Id = i.Id, 
            Name = i.Name.Translate(culture) ?? String.Empty,
        }).ToList();
    }
}