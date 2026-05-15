using App.Modules.Project.Application.DTO;
using App.Modules.Project.Application.Interfaces;
using App.Modules.Project.Application.Interfaces.Service;
using App.Modules.Project.Application.Mappers;
using App.Shared.Contracts;

namespace App.Modules.Project.Application.Services;

public class DocumentResultService : IDocumentResultService
{
    private readonly IDocumentResultRepository _documentResult;
    private readonly IUnitOfWork _uow;

    public DocumentResultService(
        IDocumentResultRepository documentResult, 
        IUnitOfWork uow)
    {
        _documentResult = documentResult;
        _uow = uow;
    }
    public async Task<IEnumerable<DocumentResultResponse>> GetAllAsync()
    {
        var entities = await _documentResult.GetAllAsync();
        return entities.Select(DocumentResultMapper.ToResponse);
    }

    public async Task<DocumentResultResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _documentResult.GetByIdAsync(id);
        if (entity == null) return null;
        return DocumentResultMapper.ToResponse(entity);
    }

    public async Task CreateAsync(CreateDocumentResultRequest request)
    {
        var entity = DocumentResultMapper.ToEntity(request);
        await _documentResult.AddAsync(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task UpdateAsync(Guid id, UpdateDocumentResultRequest request)
    {
        var entity = await _documentResult.GetByIdAsync(id);
        if (entity == null) return;
        DocumentResultMapper.UpdateEntity(entity, request);
        _documentResult.Update(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _documentResult.GetByIdAsync(id);
        if (entity == null) return;
        _documentResult.Delete(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }
}