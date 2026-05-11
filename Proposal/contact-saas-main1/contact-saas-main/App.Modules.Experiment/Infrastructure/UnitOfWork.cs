using App.Modules.Experiment.Infrastructure;
using App.Shared.Contracts;

namespace App.Modules.Equipment.Infrastructure;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly ExperimentDbContext _context;

    public UnitOfWork(ExperimentDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync()
        => await _context.SaveChangesAsync();
}