using App.Modules.Project.Application.Interfaces;
using App.Modules.Project.Application.Mapper;
using App.Shared.Contracts;

public class DocumentTypeService
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
    public async Task<IEnumerable<DocumentTypeListResponse>> GetAllAsync()
    {
        var entities = await _documentType.GetAllAsync();
        return entities.Select(DocumentTypeMapper.ToListResponse);
    }

    public async Task<DocumentTypeResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _documentType.GetByIdAsync(id);
        if (entity == null) return null;
        return DocumentTypeMapper.ToResponse(entity);
    }

    public async Task CreateAsync(CreateDocumentTypeRequest request)
    {
        var entity = DocumentTypeMapper.ToEntity(request);
        await _documentType.AddAsync(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task UpdateAsync(Guid id, UpdateDocumentTypeRequest request)
    {
        var entity = await _documentType.GetByIdAsync(id);
        if (entity == null) return;
        DocumentTypeMapper.UpdateEntity(entity, request);
        _documentType.Update(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _documentType.GetByIdAsync(id);
        if (entity == null) return;
        _documentType.Delete(entity);
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
                Name = t.Name?.Translate(culture) ?? "???"
            }).ToList();
    }
}