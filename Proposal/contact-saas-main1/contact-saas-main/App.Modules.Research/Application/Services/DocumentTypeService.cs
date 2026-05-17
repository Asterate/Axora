using App.Modules.Project.Application.DTO;
using App.Modules.Project.Application.Interfaces;
using App.Modules.Project.Application.Interfaces.Service;
using App.Modules.Project.Application.Mappers;
using App.Shared.Contracts;

namespace App.Modules.Project.Application.Services;

public class DocumentTypeService : IDocumentTypeService
{
    private readonly IDocumentTypeRepository _documentType;
    private readonly IUnitOfWork _uow;

    public DocumentTypeService(
        IDocumentTypeRepository documentType, 
        IUnitOfWork uow)
    {
        _documentType = documentType;
        _uow = uow;
    }
    public async Task<IEnumerable<DocumentTypeResponse>> GetAllAsync()
    {
        var entities = await _documentType.GetAllAsync();
        return entities.Select(DocumentTypeMapper.ToResponse);
    }

    public async Task<DocumentTypeResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _documentType.GetByIdAsync(id);
        if (entity == null) return null;
        return DocumentTypeMapper.ToResponse(entity);
    }

    public async Task CreateAsync(SaveDocumentTypeRequest request)
    {
        var entity = DocumentTypeMapper.ToEntity(request);
        await _documentType.AddAsync(entity);
        entity.CreatedAt = DateTime.Now;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task UpdateAsync(Guid id, SaveDocumentTypeRequest request)
    {
        var entity = await _documentType.GetByIdAsync(id);
        if (entity == null) return;
        DocumentTypeMapper.UpdateEntity(entity, request);
        _documentType.Update(entity);
        entity.UpdatedAt = DateTime.Now;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _documentType.GetByIdAsync(id);
        if (entity == null) return;
        _documentType.Update(entity);
        entity.DeletedAt = DateTime.Now;
        await _uow.SaveChangesAsync(); // ← actually saves now
    }
    public async Task<List<LookupItem>> GetActivesAsync(string? culture = null)
    {
        var entities = await _documentType.GetAllAsync();
        return entities
            .Where(t => t.DeletedAt == null)
            .Select(t => new LookupItem
            {
                Id = t.Id,
                Name = t.Name.Translate(culture) ?? String.Empty,
            }).ToList();
    }
}