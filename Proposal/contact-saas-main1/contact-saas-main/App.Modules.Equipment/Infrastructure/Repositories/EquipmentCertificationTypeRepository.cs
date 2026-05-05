using App.Modules.Equipment.Application.Interfaces;
using App.Modules.Equipment.Domain;
using App.Modules.Equipment.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Equipment.Infrastructure.Repositories;

internal sealed class EquipmentCertificationTypeRepository : IEquipmentCertificationTypeRepository
{
    private readonly EquipmentDbContext _context;

    public EquipmentCertificationTypeRepository(EquipmentDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EquipmentCertificationType>> GetAllAsync()
        => await _context.EquipmentCertificationTypes.ToListAsync();

    public async Task<EquipmentCertificationType?> GetByIdAsync(Guid id)
        => await _context.EquipmentCertificationTypes.FindAsync(id);

    public async Task AddAsync(EquipmentCertificationType entity)
        => await _context.EquipmentCertificationTypes.AddAsync(entity);

    public void Update(EquipmentCertificationType entity)
        => _context.EquipmentCertificationTypes.Update(entity);

    public void Delete(EquipmentCertificationType entity)
        => _context.EquipmentCertificationTypes.Remove(entity);
}