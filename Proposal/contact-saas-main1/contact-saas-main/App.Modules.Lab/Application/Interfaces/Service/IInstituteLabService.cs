using App.Modules.Lab.Application.DTO;

namespace App.Modules.Lab.Application.Interfaces;

public interface IInstituteLabService
{
    Task<IEnumerable<InstituteLabResponse>> GetAllAsync();
    Task<InstituteLabResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(CreateInstituteLabRequest request);
    Task UpdateAsync(Guid id, UpdateInstituteLabRequest request);
    Task DeleteAsync(Guid id);
}