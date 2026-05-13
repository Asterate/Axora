
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

    public async Task<IEnumerable<App.Domain.Entities.Lab>> GetAllAsync()
        => await _context.Labs.ToListAsync();

    public async Task<App.Domain.Entities.Lab?> GetByIdAsync(Guid id)
        => await _context.Labs.FindAsync(id);

    public async Task AddAsync(App.Domain.Entities.Lab entity)
        => await _context.Labs.AddAsync(entity);

    public void Update(App.Domain.Entities.Lab entity)
        => _context.Labs.Update(entity);

    public void Delete(App.Domain.Entities.Lab entity)
        => _context.Labs.Remove(entity);
}