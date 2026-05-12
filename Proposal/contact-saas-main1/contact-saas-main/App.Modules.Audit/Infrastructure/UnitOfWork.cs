using App.Modules.Audit.Infrastructure.Data;
using App.Shared.Contracts;

namespace App.Modules.Audit.Infrastructure;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly AuditDbContext _context;

    public UnitOfWork(AuditDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync()
        => await _context.SaveChangesAsync();
}