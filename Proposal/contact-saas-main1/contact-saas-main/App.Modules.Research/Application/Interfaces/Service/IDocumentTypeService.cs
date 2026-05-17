using App.Modules.Project.Application.DTO;
using App.Shared.Contracts;

namespace App.Modules.Project.Application.Interfaces.Service;

public interface IDocumentTypeService
{
    Task<IEnumerable<DocumentTypeResponse>> GetAllAsync();
    Task<DocumentTypeResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(SaveDocumentTypeRequest request);
    Task UpdateAsync(Guid id, SaveDocumentTypeRequest request);
    Task DeleteAsync(Guid id);
    Task<List<LookupItem>> GetActivesAsync(string? culture = null);
}