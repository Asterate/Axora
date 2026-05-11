using App.Shared.Contracts;

namespace App.Modules.Reagent.Infrastructure;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly ReagentDbContext _context;

    public UnitOfWork(ReagentDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync()
        => await _context.SaveChangesAsync();
}