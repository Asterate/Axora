using App.Domain.Entities;
using App.Modules.Project.Application.Interfaces;
using App.Modules.Project.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Lab.Infrastructure.Repositories;

internal sealed class ResultRepository : IResultRepository
{
    private readonly ProjectDbContext _context;

    public ResultRepository(ProjectDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Result>> GetAllAsync()
        => await _context.Results.ToListAsync();

    public async Task<Result?> GetByIdAsync(Guid id)
        => await _context.Results.FindAsync(id);

    public async Task AddAsync(Result entity)
        => await _context.Results.AddAsync(entity);

    public void Update(Result entity)
        => _context.Results.Update(entity);

    public void Delete(Result entity)
        => _context.Results.Remove(entity);
}