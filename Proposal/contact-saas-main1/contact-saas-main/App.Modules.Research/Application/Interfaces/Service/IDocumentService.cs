using App.Modules.Project.Application.DTO;
using App.Shared.Contracts;

namespace App.Modules.Project.Application.Interfaces.Service;

public interface IDocumentService
{
    Task<IEnumerable<DocumentListResponse>> GetAllAsync();
    Task<DocumentResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(SaveDocumentRequest request);
    Task UpdateAsync(Guid id, SaveDocumentRequest request);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<DocumentResponse>> FindDeletedAsync();
    Task<List<LookupItem>> GetActivesAsync(string? culture = null);
}