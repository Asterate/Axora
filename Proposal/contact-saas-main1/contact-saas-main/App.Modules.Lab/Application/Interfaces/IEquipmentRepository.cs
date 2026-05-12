using App.Shared.Contracts;
using EquipmentEntity = App.Modules.Equipment.Domain.Equipment;

namespace App.Modules.Equipment.Application.Interfaces;
//TODO: Figure out the stupid Domain.Equipment thing
public interface IEquipmentRepository : IBaseRepository<EquipmentEntity>
{
    Task<IEnumerable<EquipmentEntity>> GetAllWithTypeAsync();
    Task<EquipmentEntity?> GetByIdWithTypeAsync(Guid id);
    
}