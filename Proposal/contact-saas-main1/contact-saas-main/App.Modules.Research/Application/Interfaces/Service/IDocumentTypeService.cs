using App.Modules.Project.Application.DTO;
using App.Shared.Contracts;

namespace App.Modules.Project.Application.Interfaces.Service;

public interface IDocumentTypeService
{
    Task<IEnumerable<DocumentTypeListResponse>> GetAllAsync();
    Task<DocumentTypeResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(CreateDocumentTypeRequest request);
    Task UpdateAsync(Guid id, UpdateDocumentTypeRequest request);
    Task DeleteAsync(Guid id);
    Task<List<LookupItem>> GetActivesAsync(string? culture = null);
}