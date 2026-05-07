using App.Modules.Equipment.Infrastructure.Data;
using App.Shared.Contracts;

namespace App.Modules.Equipment.Infrastructure;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly EquipmentDbContext _context;

    public UnitOfWork(EquipmentDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync()
        => await _context.SaveChangesAsync();
}