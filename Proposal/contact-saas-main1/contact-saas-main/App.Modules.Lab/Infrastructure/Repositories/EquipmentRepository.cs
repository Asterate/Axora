using App.Modules.Equipment.Application.Interfaces;
using App.Modules.Lab.Infrastructure;
using EquipmentEntity = App.Modules.Equipment.Domain.Equipment;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Equipment.Infrastructure.Repositories;

internal sealed class EquipmentRepository : IEquipmentRepository
{
    private readonly LabDbContext _context;

    public EquipmentRepository(LabDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EquipmentEntity>> GetAllAsync()
        => await _context.Equipments.ToListAsync();

    public async Task<EquipmentEntity?> GetByIdAsync(Guid id)
        => await _context.Equipments.FindAsync(id);

    public async Task AddAsync(EquipmentEntity entity)
        => await _context.Equipments.AddAsync(entity);

    public void Update(EquipmentEntity entity)
        => _context.Equipments.Update(entity);

    public void Delete(EquipmentEntity entity)
        => _context.Equipments.Remove(entity);
    
    public async Task<IEnumerable<EquipmentEntity>> GetAllWithTypeAsync()
        => await _context.Equipments
            .Include(e => e.EquipmentType)
            .ToListAsync();

    public async Task<EquipmentEntity?> GetByIdWithTypeAsync(Guid id)
        => await _context.Equipments
            .Include(e => e.EquipmentType)
            .FirstOrDefaultAsync(e => e.Id == id);
}