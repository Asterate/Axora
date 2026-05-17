using App.Modules.Lab.Application.DTO;
using App.Modules.Lab.Application.Interfaces;
using App.Modules.Lab.Application.Interfaces.Service;
using App.Modules.Lab.Application.Mappers;
using App.Shared.Contracts;

namespace App.Modules.Lab.Application.Services;

public class InstituteLabService : IInstituteLabService
{
    private readonly IInstituteLabRepository _instituteLab;
    private readonly IUnitOfWork _uow;

    public InstituteLabService(
        IInstituteLabRepository instituteLabRepo,
        IUnitOfWork uow)
    {
        _instituteLab = instituteLabRepo;
        _uow = uow;
    }
    public async Task<IEnumerable<InstituteLabResponse>> GetAllAsync()
    {
        var entities = await _instituteLab.GetAllAsync();
        return entities.Select(InstituteLabMapper.ToResponse);
    }

    public async Task<InstituteLabResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _instituteLab.GetByIdAsync(id);
        if (entity == null) return null;
        return InstituteLabMapper.ToResponse(entity);
    }

    public async Task CreateAsync(SaveInstituteLabRequest request)
    {
        var entity = InstituteLabMapper.ToEntity(request);
        await _instituteLab.AddAsync(entity);
        entity.CreatedAt =  DateTime.UtcNow;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task UpdateAsync(Guid id, SaveInstituteLabRequest request)
    {
        var entity = await _instituteLab.GetByIdAsync(id);
        if (entity == null) return;
        InstituteLabMapper.UpdateEntity(entity, request);
        _instituteLab.Update(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _instituteLab.GetByIdAsync(id);
        if (entity == null) return;
        _instituteLab.Update(entity);
        entity.DeletedAt =  DateTime.UtcNow;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }
}