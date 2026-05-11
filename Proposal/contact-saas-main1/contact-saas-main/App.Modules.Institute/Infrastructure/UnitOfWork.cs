using App.Shared.Contracts;

namespace App.Modules.Institute.Infrastructure;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly InstituteDbContext _context;

    public UnitOfWork(InstituteDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync()
        => await _context.SaveChangesAsync();
}