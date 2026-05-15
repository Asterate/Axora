
using App.Modules.Lab.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Lab.Infrastructure.Repositories;

internal sealed class LabRepository : ILabRepository
{
    private readonly LabDbContext _context;

    public LabRepository(LabDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Domain.Lab>> GetAllAsync()
        => await _context.Labs.ToListAsync();

    public async Task<Domain.Lab?> GetByIdAsync(Guid id)
        => await _context.Labs.FindAsync(id);

    public async Task AddAsync(Domain.Lab entity)
        => await _context.Labs.AddAsync(entity);

    public void Update(Domain.Lab entity)
        => _context.Labs.Update(entity);

    public void Delete(Domain.Lab entity)
        => _context.Labs.Remove(entity);
}