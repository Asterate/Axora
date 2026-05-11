
using System.Reflection.Metadata;
using App.Domain.Entities;
using App.Modules.Reagent.Application.Interfaces;
using App.Modules.Reagent.Infrastructure;
using Microsoft.EntityFrameworkCore;

internal class ReagentRepository : IReagentRepository
{
    private readonly ReagentDbContext _context;

    public ReagentRepository(ReagentDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Reagent>> GetAllAsync()
        => await _context.Reagents.ToListAsync();

    public async Task<Reagent?> GetByIdAsync(Guid id)
        => await _context.Reagents
            .FirstOrDefaultAsync(d => d.Id == id);

    public async Task AddAsync(Reagent entity)
        => await _context.Reagents.AddAsync(entity);

    public void Update(Reagent entity)
        => _context.Reagents.Update(entity);

    public void Delete(Reagent entity)
        => _context.Reagents.Remove(entity);
}