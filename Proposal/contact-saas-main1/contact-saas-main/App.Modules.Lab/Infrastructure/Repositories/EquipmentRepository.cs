using App.Modules.Equipment.Application.Interfaces;
using App.Modules.Lab.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Equipment.Infrastructure.Repositories;

internal sealed class EquipmentRepository : IEquipmentRepository
{
    private readonly LabDbContext _context;

    public EquipmentRepository(LabDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Lab.Domain.Equipment>> GetAllAsync()
        => await _context.Equipments.ToListAsync();

    public async Task<Lab.Domain.Equipment?> GetByIdAsync(Guid id)
        => await _context.Equipments.FindAsync(id);

    public async Task AddAsync(Lab.Domain.Equipment entity)
        => await _context.Equipments.AddAsync(entity);

    public void Update(Lab.Domain.Equipment entity)
        => _context.Equipments.Update(entity);

    public void Delete(Lab.Domain.Equipment entity)
        => _context.Equipments.Remove(entity);
    
    public async Task<IEnumerable<Lab.Domain.Equipment>> GetAllWithTypeAsync()
        => await _context.Equipments
            .Include(e => e.EquipmentType)
            .ToListAsync();

    public async Task<Lab.Domain.Equipment?> GetByIdWithTypeAsync(Guid id)
        => await _context.Equipments
            .Include(e => e.EquipmentType)
            .FirstOrDefaultAsync(e => e.Id == id);
}