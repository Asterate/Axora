using App.Modules.Reagent.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Lab.Infrastructure.Repositories;

internal class ReagentRepository : IReagentRepository
{
    private readonly LabDbContext _context;

    public ReagentRepository(LabDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Domain.Reagent>> GetAllAsync()
        => await _context.Reagents.ToListAsync();

    public async Task<Domain.Reagent?> GetByIdAsync(Guid id)
        => await _context.Reagents
            .FirstOrDefaultAsync(d => d.Id == id);

    public async Task AddAsync(Domain.Reagent entity)
        => await _context.Reagents.AddAsync(entity);

    public void Update(Domain.Reagent entity)
        => _context.Reagents.Update(entity);

    public void Delete(Domain.Reagent entity)
        => _context.Reagents.Remove(entity);
}