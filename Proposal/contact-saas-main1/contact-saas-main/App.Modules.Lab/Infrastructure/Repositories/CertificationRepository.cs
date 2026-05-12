using App.Modules.Equipment.Application.Interfaces;
using App.Modules.Equipment.Domain;
using App.Modules.Lab.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Equipment.Infrastructure.Repositories;

internal sealed class CertificationRepository : ICertificationRepository
{
    private readonly LabDbContext  _context;

    public CertificationRepository(LabDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Certification>> GetAllAsync()
        => await _context.Certifications.ToListAsync();

    public async Task<Certification?> GetByIdAsync(Guid id)
        => await _context.Certifications.FindAsync(id);

    public async Task AddAsync(Certification entity)
        => await _context.Certifications.AddAsync(entity);

    public void Update(Certification entity)
        => _context.Certifications.Update(entity);

    public void Delete(Certification entity)
        => _context.Certifications.Remove(entity);
}