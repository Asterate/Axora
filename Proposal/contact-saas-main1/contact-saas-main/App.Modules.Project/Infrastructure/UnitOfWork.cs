using App.Modules.Project.Infrastructure;
using App.Shared.Contracts;

namespace App.Modules.Lab.Infrastructure;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly ProjectDbContext _context;

    public UnitOfWork(ProjectDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync()
        => await _context.SaveChangesAsync();
}