using App.Shared.Contracts;

namespace App.Modules.Identity.Infrastructure;

internal sealed class UnitOfWork :  IUnitOfWork
{
    private readonly IdentityModuleDbContext _context;

    public UnitOfWork(IdentityModuleDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync()
        => await _context.SaveChangesAsync();
}