using App.Domain.Entities;
using App.Modules.Reagent.Application.Interfaces;
using App.Modules.Reagent.Infrastructure;
using Microsoft.EntityFrameworkCore;

internal class ReagentTypeRepository : IReagentTypeRepository
{
    private readonly ReagentDbContext _context;

    public ReagentTypeRepository(ReagentDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ReagentType>> GetAllAsync()
        => await _context.ReagentTypes.ToListAsync();

    public async Task<ReagentType?> GetByIdAsync(Guid id)
        => await _context.ReagentTypes
            .FirstOrDefaultAsync(d => d.Id == id);

    public async Task AddAsync(ReagentType entity)
        => await _context.ReagentTypes.AddAsync(entity);

    public void Update(ReagentType entity)
        => _context.ReagentTypes.Update(entity);

    public void Delete(ReagentType entity)
        => _context.ReagentTypes.Remove(entity);
}