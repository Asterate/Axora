using App.Modules.Project.Application.Interfaces;
using App.Modules.Project.Application.Mapper;
using App.Shared.Contracts;

public class DocumentService
{
    private readonly IDocumentRepository _document;
    private readonly IUnitOfWork _uow;

    public DocumentService(
        IDocumentRepository document, 
        IUnitOfWork uow)
    {
        _document = document;
        _uow = uow;
    }
    public async Task<IEnumerable<DocumentListResponse>> GetAllAsync()
    {
        var entities = await _document.GetAllAsync();
        return entities
            .Where(s => s.DeletedAt == null)
            .Select(DocumentMapper.ToListResponse);
    }

    public async Task<DocumentResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _document.GetByIdAsync(id);
        if (entity == null) return null;
        return DocumentMapper.ToResponse(entity);
    }

    public async Task CreateAsync(CreateDocumentRequest request)
    {
        var entity = DocumentMapper.ToEntity(request);
        await _document.AddAsync(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task UpdateAsync(Guid id, UpdateDocumentRequest request)
    {
        var entity = await _document.GetByIdAsync(id);
        if (entity == null) return;
        DocumentMapper.UpdateEntity(entity, request);
        _document.Update(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _document.GetByIdAsync(id);
        if (entity == null) return;
        entity.DeletedAt = DateTime.Now;
        _document.Update(entity);
        await _uow.SaveChangesAsync();
    }
    public async Task<IEnumerable<DocumentListResponse>> FindDeletedAsync()
    {
        var entities = await _document.GetAllAsync();
        return entities
            .Where(s => s.DeletedAt != null)
            .Select(DocumentMapper.ToListResponse);
    }
}