using App.Modules.Equipment.Application.Interfaces;
using App.Modules.Equipment.Domain;
using App.Modules.Equipment.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Equipment.Infrastructure.Repositories;

internal sealed class EquipmentTypeRepository : IEquipmentTypeRepository
{
    private readonly EquipmentDbContext _context;

    public EquipmentTypeRepository(EquipmentDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EquipmentType>> GetAllAsync()
        => await _context.EquipmentTypes.ToListAsync();

    public async Task<EquipmentType?> GetByIdAsync(Guid id)
        => await _context.EquipmentTypes.FindAsync(id);

    public async Task AddAsync(EquipmentType entity)
        => await _context.EquipmentTypes.AddAsync(entity);

    public void Update(EquipmentType entity)
        => _context.EquipmentTypes.Update(entity);

    public void Delete(EquipmentType entity)
        => _context.EquipmentTypes.Remove(entity);
}