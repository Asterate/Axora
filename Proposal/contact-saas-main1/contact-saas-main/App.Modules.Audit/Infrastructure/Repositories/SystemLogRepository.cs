using App.Domain.Entities;
using App.Modules.Audit.Application.Interface;
using App.Modules.Audit.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Audit.Infrastructure.Repositories;

internal sealed class SystemLogRepository : ISystemLogRepository
{
    private readonly AuditDbContext _context;

    public SystemLogRepository(AuditDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SystemLog>> GetAllAsync()
        => await _context.SystemLogs.ToListAsync();

    public async Task<SystemLog?> GetByIdAsync(Guid id)
        => await _context.SystemLogs.FindAsync(id);

    public async Task AddAsync(SystemLog entity)
        => await _context.SystemLogs.AddAsync(entity);

    public void Update(SystemLog entity)
        => _context.SystemLogs.Update(entity);

    public void Delete(SystemLog entity)
        => _context.SystemLogs.Remove(entity);
}