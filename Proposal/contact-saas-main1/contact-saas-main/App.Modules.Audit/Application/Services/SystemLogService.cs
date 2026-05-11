using App.Modules.Audit.Application.Interface;
using App.Modules.Experiment.Application.Mapper;
using App.Shared.Contracts;

public class SystemLogService
{
    private readonly ISystemLogRepository _systemLogRepo;
    private readonly IUnitOfWork _uow;

    public SystemLogService(
        ISystemLogRepository systemLogRepo,
        IUnitOfWork uow)
    {
        _systemLogRepo = systemLogRepo;
        _uow = uow;
    }
    public async Task<IEnumerable<SystemLogListResponse>> GetAllAsync()
    {
        var entities = await _systemLogRepo.GetAllAsync();
        return entities.Select(SystemLogMapper.ToListResponse);
    }

    public async Task<SystemLogResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _systemLogRepo.GetByIdAsync(id);
        if (entity == null) return null;
        return SystemLogMapper.ToResponse(entity);
    }

    public async Task CreateAsync(CreateSystemLogRequest request)
    {
        var entity = SystemLogMapper.ToEntity(request);
        await _systemLogRepo.AddAsync(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task UpdateAsync(Guid id, UpdateSystemLogRequest request)
    {
        var entity = await _systemLogRepo.GetByIdAsync(id);
        if (entity == null) return;
        SystemLogMapper.UpdateEntity(entity, request);
        _systemLogRepo.Update(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _systemLogRepo.GetByIdAsync(id);
        if (entity == null) return;
        _systemLogRepo.Delete(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }
    public async Task<IEnumerable<SystemLogListResponse>> GetRecentAsync(int take)
    {
        var all = await _systemLogRepo.GetAllAsync();
        return all
            .Where(i => i.DeletedAt == null)
            .OrderByDescending(i => i.CreatedAt)
            .Take(take)
            .Select(SystemLogMapper.ToListResponse);
    }
}