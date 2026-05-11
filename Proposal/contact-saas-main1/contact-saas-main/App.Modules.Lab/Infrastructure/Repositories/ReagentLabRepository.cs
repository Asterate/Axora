using App.Domain.Entities;
using App.Modules.Lab.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Lab.Infrastructure.Repositories;

internal sealed class ReagentLabRepository : IReagentLabRepository
{
    private readonly LabDbContext _context;

    public ReagentLabRepository(LabDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ReagentLab>> GetAllAsync()
        => await _context.ReagentLabs.ToListAsync();

    public async Task<ReagentLab?> GetByIdAsync(Guid id)
        => await _context.ReagentLabs.FindAsync(id);

    public async Task AddAsync(ReagentLab entity)
        => await _context.ReagentLabs.AddAsync(entity);

    public void Update(ReagentLab entity)
        => _context.ReagentLabs.Update(entity);

    public void Delete(ReagentLab entity)
        => _context.ReagentLabs.Remove(entity);
}