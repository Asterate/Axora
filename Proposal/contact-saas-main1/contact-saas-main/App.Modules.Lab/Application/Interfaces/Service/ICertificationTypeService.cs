using App.Modules.Lab.Application.DTO;
using App.Shared.Contracts;

namespace App.Modules.Lab.Application.Interfaces;

public interface ICertificationTypeService
{
    Task<IEnumerable<CertificationTypeListResponse>> GetAllAsync();
    Task<CertificationTypeResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(CreateCertificationTypeRequest request);
    Task UpdateAsync(Guid id, UpdateCertificationTypeRequest request);
    Task DeleteAsync(Guid id);
    Task<List<LookupItem>> GetActivesAsync(string? culture = null);
}