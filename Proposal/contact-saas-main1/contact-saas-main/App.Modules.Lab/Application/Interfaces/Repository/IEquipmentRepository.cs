using App.Shared.Contracts;

namespace App.Modules.Equipment.Application.Interfaces;
//TODO: Figure out the stupid Domain.Equipment thing
public interface IEquipmentRepository : IBaseRepository<Lab.Domain.Equipment>
{
    Task<IEnumerable<Lab.Domain.Equipment>> GetAllWithTypeAsync();
    Task<Lab.Domain.Equipment?> GetByIdWithTypeAsync(Guid id);
    
}