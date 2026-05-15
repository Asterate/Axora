using App.Modules.Project.Application.DTO;
using App.Modules.Project.Application.Interfaces;
using App.Modules.Project.Application.Mapper;
using App.Modules.Project.Application.Mappers;
using App.Shared.Contracts;

public class ScheduleService
{
    private readonly IScheduleRepository _schedule;
    private readonly IUnitOfWork _uow;

    public ScheduleService(
        IScheduleRepository schedule, 
        IUnitOfWork uow)
    {
        _schedule = schedule;
        _uow = uow;
    }
    public async Task<IEnumerable<ScheduleListResponse>> GetAllAsync()
    {
        var entities = await _schedule.GetAllAsync();
        return entities.Select(ScheduleMapper.ToListResponse);
    }

    public async Task<ScheduleResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _schedule.GetByIdAsync(id);
        if (entity == null) return null;
        return ScheduleMapper.ToResponse(entity);
    }

    public async Task CreateAsync(CreateScheduleRequest request)
    {
        var entity = ScheduleMapper.ToEntity(request);
        await _schedule.AddAsync(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task UpdateAsync(Guid id, UpdateScheduleRequest request)
    {
        var entity = await _schedule.GetByIdAsync(id);
        if (entity == null) return;
        ScheduleMapper.UpdateEntity(entity, request);
        _schedule.Update(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _schedule.GetByIdAsync(id);
        if (entity == null) return;
        _schedule.Delete(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }
}