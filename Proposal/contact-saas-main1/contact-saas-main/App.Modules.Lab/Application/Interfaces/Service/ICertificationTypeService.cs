using App.Modules.Lab.Application.DTO;
using App.Shared.Contracts;

namespace App.Modules.Lab.Application.Interfaces.Service;

public interface ICertificationTypeService
{
    Task<IEnumerable<CertificationTypeResponse>> GetAllAsync();
    Task<CertificationTypeResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(SaveCertificationTypeRequest request);
    Task UpdateAsync(Guid id, SaveCertificationTypeRequest request);
    Task DeleteAsync(Guid id);
    Task<List<LookupItem>> GetActivesAsync(string? culture = null);
}