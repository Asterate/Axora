using App.Modules.Equipment.Application.Interfaces;
using App.Modules.Equipment.Domain;
using App.Modules.Lab.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Equipment.Infrastructure.Repositories;

internal sealed class CertificationTypeRepository : ICertificationTypeRepository
{
    private readonly LabDbContext _context;

    public CertificationTypeRepository(LabDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CertificationType>> GetAllAsync()
        => await _context.CertificationTypes.ToListAsync();

    public async Task<CertificationType?> GetByIdAsync(Guid id)
        => await _context.CertificationTypes.FindAsync(id);

    public async Task AddAsync(CertificationType entity)
        => await _context.CertificationTypes.AddAsync(entity);

    public void Update(CertificationType entity)
        => _context.CertificationTypes.Update(entity);

    public void Delete(CertificationType entity)
        => _context.CertificationTypes.Remove(entity);
}