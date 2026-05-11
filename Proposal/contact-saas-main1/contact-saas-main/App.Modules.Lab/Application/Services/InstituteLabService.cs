using App.Modules.Lab.Application.Interfaces;
using App.Modules.Lab.Application.Mapper;
using App.Shared.Contracts;

public class InstituteLabService
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
    public async Task<IEnumerable<InstituteLabListResponse>> GetAllAsync()
    {
        var entities = await _instituteLab.GetAllAsync();
        return entities.Select(InstituteLabMapper.ToInstituteLabResponse);
    }

    public async Task<InstituteLabResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _instituteLab.GetByIdAsync(id);
        if (entity == null) return null;
        return InstituteLabMapper.ToResponse(entity);
    }

    public async Task CreateAsync(CreateInstituteLabRequest request)
    {
        var entity = InstituteLabMapper.ToEntity(request);
        await _instituteLab.AddAsync(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task UpdateAsync(Guid id, UpdateInstituteLabRequest request)
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
        _instituteLab.Delete(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }
}