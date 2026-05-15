using App.Modules.Institute.Application.Interfaces;
using App.Modules.Project.Application.DTO;
using App.Modules.Project.Application.Interfaces.Service;
using App.Modules.Project.Application.Mappers;
using App.Shared.Contracts;

namespace App.Modules.Project.Application.Services;

public class InstituteProjectService : IInstituteProjectService
{
    private readonly IInstituteProjectRepository _instituteProject;
    private readonly IUnitOfWork _uow;

    public InstituteProjectService(
        IInstituteProjectRepository instituteProjectRepo,
        IUnitOfWork uow)
    {
        _instituteProject = instituteProjectRepo;
        _uow = uow;
    }
    public async Task<IEnumerable<InstituteProjectResponse>> GetAllAsync()
    {
        var entities = await _instituteProject.GetAllAsync();
        return entities.Select(InstituteProjectMapper.ToResponse);
    }

    public async Task<InstituteProjectResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _instituteProject.GetByIdAsync(id);
        if (entity == null) return null;
        return InstituteProjectMapper.ToResponse(entity);
    }

    public async Task CreateAsync(CreateInstituteProjectRequest request)
    {
        var entity = InstituteProjectMapper.ToEntity(request);
        await _instituteProject.AddAsync(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task UpdateAsync(Guid id, UpdateInstituteProjectRequest request)
    {
        var entity = await _instituteProject.GetByIdAsync(id);
        if (entity == null) return;
        InstituteProjectMapper.UpdateEntity(entity, request);
        _instituteProject.Update(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _instituteProject.GetByIdAsync(id);
        if (entity == null) return;
        _instituteProject.Delete(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }
}