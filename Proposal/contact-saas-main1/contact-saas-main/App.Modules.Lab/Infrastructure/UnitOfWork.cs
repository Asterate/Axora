using App.Shared.Contracts;

namespace App.Modules.Lab.Infrastructure;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly LabDbContext _context;

    public UnitOfWork(LabDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync()
        => await _context.SaveChangesAsync();
}