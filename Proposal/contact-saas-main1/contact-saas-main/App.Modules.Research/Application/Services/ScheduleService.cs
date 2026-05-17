using App.Modules.Project.Application.DTO;
using App.Modules.Project.Application.Interfaces;
using App.Modules.Project.Application.Interfaces.Service;
using App.Modules.Project.Application.Mappers;
using App.Shared.Contracts;
using App.Shared.Contracts.Events;
using MediatR;

namespace App.Modules.Project.Application.Services;

public class ScheduleService : IScheduleService
{
    private readonly IMediator _mediator;
    private readonly IScheduleRepository _schedule;
    private readonly IUnitOfWork _uow;

    public ScheduleService(
        IScheduleRepository schedule, 
        IUnitOfWork uow, IMediator mediator)
    {
        _schedule = schedule;
        _uow = uow;
        _mediator = mediator;
    }
    public async Task<IEnumerable<ScheduleListResponse>> GetAllAsync()
    {
        var entities = await _schedule.GetAllAsync();
        var responses = new List<ScheduleListResponse>();

        foreach (var entity in entities)
        {
            var lab = await _mediator.Send(new ScheduleEvent.GetLabByIdQuery(entity.LabId));
            var equipment = await _mediator.Send(new ScheduleEvent.GetEquipmentByIdQuery(entity.EquipmentId));
            responses.Add(ScheduleMapper.ToListResponse(entity, lab, equipment));
        }
        return responses;
    }

    public async Task<ScheduleResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _schedule.GetByIdAsync(id);
        if (entity == null) return null;
    
        var lab = await _mediator.Send(new ScheduleEvent.GetLabByIdQuery(entity.LabId));
        var equipment = await _mediator.Send(new ScheduleEvent.GetEquipmentByIdQuery(entity.EquipmentId));
    
        return ScheduleMapper.ToResponse(entity, lab, equipment);
    }
    public async Task<SaveScheduleRequest?> GetByIdEditAsync(Guid id)
    {
        var entity = await _schedule.GetByIdAsync(id);
        if (entity == null) return null;
        return ScheduleMapper.ToUpdateRequest(entity);
    }

    public async Task CreateAsync(SaveScheduleRequest request)
    {
        var entity = ScheduleMapper.ToEntity(request);
        await _schedule.AddAsync(entity);
        entity.CreatedAt = DateTime.UtcNow;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task UpdateAsync(Guid id, SaveScheduleRequest request)
    {
        var entity = await _schedule.GetByIdAsync(id);
        if (entity == null) return;
        ScheduleMapper.UpdateEntity(entity, request);
        _schedule.Update(entity);
        entity.UpdatedAt = DateTime.UtcNow;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _schedule.GetByIdAsync(id);
        if (entity == null) return;
        _schedule.Update(entity);
        entity.DeletedAt = DateTime.UtcNow;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }
}