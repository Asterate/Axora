using App.Modules.Project.Application.Interfaces;
using App.Modules.Project.Application.Mapper;
using App.Shared.Contracts;

public class ResultService
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

    public async Task CreateAsync(CreateResultRequest request)
    {
        var entity = ResultMapper.ToEntity(request);
        await _result.AddAsync(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task UpdateAsync(Guid id, UpdateResultRequest request)
    {
        var entity = await _result.GetByIdAsync(id);
        if (entity == null) return;
        ResultMapper.UpdateEntity(entity, request);
        _result.Update(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _result.GetByIdAsync(id);
        if (entity == null) return;
        _result.Delete(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }
    
}