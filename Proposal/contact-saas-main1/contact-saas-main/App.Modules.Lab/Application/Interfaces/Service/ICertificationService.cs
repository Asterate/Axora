using App.Modules.Lab.Application.DTO;

namespace App.Modules.Lab.Application.Interfaces.Service;

public interface ICertificationService
{
    Task<IEnumerable<CertificationResponse>> GetAllAsync();
    Task<CertificationResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(SaveCertificationRequest request);
    Task UpdateAsync(Guid id, SaveCertificationRequest request);
    Task DeleteAsync(Guid id);
}