using App.Modules.Lab.Application.DTO;

namespace App.Modules.Lab.Application.Interfaces;

public interface ICertificationService
{
    Task<IEnumerable<CertificationResponse>> GetAllAsync();
    Task<CertificationResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(CreateCertificationRequest request);
    Task UpdateAsync(Guid id, UpdateCertificationRequest request);
    Task DeleteAsync(Guid id);
}