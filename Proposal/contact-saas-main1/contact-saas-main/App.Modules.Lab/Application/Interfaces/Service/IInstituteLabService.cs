using App.Modules.Lab.Application.DTO;

namespace App.Modules.Lab.Application.Interfaces.Service;

public interface IInstituteLabService
{
    Task<IEnumerable<InstituteLabResponse>> GetAllAsync();
    Task<InstituteLabResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(SaveInstituteLabRequest request);
    Task UpdateAsync(Guid id, SaveInstituteLabRequest request);
    Task DeleteAsync(Guid id);
}