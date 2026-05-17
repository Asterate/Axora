using App.Modules.Project.Application.DTO;
using App.Modules.Project.Application.Interfaces;
using App.Modules.Project.Application.Interfaces.Service;
using App.Modules.Project.Application.Mappers;
using App.Shared.Contracts;

namespace App.Modules.Project.Application.Services;

public class ResultService : IResultService
{
    private readonly IResultRepository _result;
    private readonly IUnitOfWork _uow;

    public ResultService(
        IResultRepository result, 
        IUnitOfWork uow)
    {
        _result = result;
        _uow = uow;
    }
    public async Task<IEnumerable<ResultListResponse>> GetAllAsync()
    {
        var entities = await _result.GetAllAsync();
        return entities.Select(ResultMapper.ToListResponse);
    }

    public async Task<ResultResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _result.GetByIdAsync(id);
        if (entity == null) return null;
        return ResultMapper.ToResponse(entity);
    }

    public async Task CreateAsync(SaveResultRequest request)
    {
        var entity = ResultMapper.ToEntity(request);
        await _result.AddAsync(entity);
        entity.CreatedAt = DateTime.UtcNow;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task UpdateAsync(Guid id, SaveResultRequest request)
    {
        var entity = await _result.GetByIdAsync(id);
        if (entity == null) return;
        ResultMapper.UpdateEntity(entity, request);
        _result.Update(entity);
        entity.UpdatedAt = DateTime.UtcNow;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _result.GetByIdAsync(id);
        if (entity == null) return;
        _result.Update(entity);
        entity.DeletedAt = DateTime.UtcNow;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }
    
}