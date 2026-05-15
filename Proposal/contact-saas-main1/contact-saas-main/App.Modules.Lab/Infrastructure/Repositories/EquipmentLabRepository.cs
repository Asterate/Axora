using App.Modules.Lab.Application.Interfaces;
using App.Modules.Lab.Domain;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Lab.Infrastructure.Repositories;

internal sealed class EquipmentLabRepository : IEquipmentLabRepository
{
    private readonly LabDbContext _context;

    public EquipmentLabRepository(LabDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EquipmentLab>> GetAllAsync()
        => await _context.EquipmentLabs.ToListAsync();

    public async Task<EquipmentLab?> GetByIdAsync(Guid id)
        => await _context.EquipmentLabs.FindAsync(id);

    public async Task AddAsync(EquipmentLab entity)
        => await _context.EquipmentLabs.AddAsync(entity);

    public void Update(EquipmentLab entity)
        => _context.EquipmentLabs.Update(entity);

    public void Delete(EquipmentLab entity)
        => _context.EquipmentLabs.Remove(entity);
}