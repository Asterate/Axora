using App.Domain.Entities;
using App.Modules.Lab.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Lab.Infrastructure.Repositories;

internal sealed class LabTypeRepository : ILabTypeRepository
{
    private readonly LabDbContext _context;

    public LabTypeRepository(LabDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<LabType>> GetAllAsync()
        => await _context.LabTypes.ToListAsync();

    public async Task<LabType?> GetByIdAsync(Guid id)
        => await _context.LabTypes.FindAsync(id);

    public async Task AddAsync(LabType entity)
        => await _context.LabTypes.AddAsync(entity);

    public void Update(LabType entity)
        => _context.LabTypes.Update(entity);

    public void Delete(LabType entity)
        => _context.LabTypes.Remove(entity);
}