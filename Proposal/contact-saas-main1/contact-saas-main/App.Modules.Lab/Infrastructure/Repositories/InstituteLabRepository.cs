using App.Domain.Entities;
using App.Modules.Lab.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Lab.Infrastructure.Repositories;

internal sealed class InstituteLabRepository : IInstituteLabRepository
{
    private readonly LabDbContext _context;

    public InstituteLabRepository(LabDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<InstituteLab>> GetAllAsync()
        => await _context.InstituteLabs.ToListAsync();

    public async Task<InstituteLab?> GetByIdAsync(Guid id)
        => await _context.InstituteLabs.FindAsync(id);

    public async Task AddAsync(InstituteLab entity)
        => await _context.InstituteLabs.AddAsync(entity);

    public void Update(InstituteLab entity)
        => _context.InstituteLabs.Update(entity);

    public void Delete(InstituteLab entity)
        => _context.InstituteLabs.Remove(entity);
}