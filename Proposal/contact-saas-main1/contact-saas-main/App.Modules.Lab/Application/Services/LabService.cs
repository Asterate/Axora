using App.Modules.Lab.Application.Interfaces;
using App.Modules.Lab.Application.Mapper;
using App.Shared.Contracts;

public class LabService
{
    private readonly ILabRepository _lab;
    private readonly IUnitOfWork _uow;

    public LabService(
        ILabRepository labRepo,
        IUnitOfWork uow)
    {
        _lab = labRepo;
        _uow = uow;
    }
    public async Task<IEnumerable<LabListResponse>> GetAllAsync()
    {
        var entities = await _lab.GetAllAsync();
        return entities.Select(LabMapper.ToListResponse);
    }

    public async Task<LabResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _lab.GetByIdAsync(id);
        if (entity == null) return null;
        return LabMapper.ToResponse(entity);
    }

    public async Task CreateAsync(CreateLabRequest request)
    {
        var entity = LabMapper.ToEntity(request);
        await _lab.AddAsync(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task UpdateAsync(Guid id, UpdateLabRequest request)
    {
        var entity = await _lab.GetByIdAsync(id);
        if (entity == null) return;
        LabMapper.UpdateEntity(entity, request);
        _lab.Update(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _lab.GetByIdAsync(id);
        if (entity == null) return;
        _lab.Delete(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task<int> CountAsync()
    {
        var all = await _lab.GetAllAsync();
        return all.Count(i => i.DeletedAt == null);
    }
}