using App.Shared.Contracts;

namespace App.Modules.Identity.Infrastructure;

internal sealed class UnitOfWork :  IUnitOfWork
{
    private readonly IdentityDbContext _context;

    public UnitOfWork(IdentityDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync()
        => await _context.SaveChangesAsync();
}