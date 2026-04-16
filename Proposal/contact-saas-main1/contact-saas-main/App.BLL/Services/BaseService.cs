using App.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.BLL.Services;

/// <summary>
/// Base service class providing common database access and user identification functionality
/// </summary>
public abstract class BaseService
{
    protected readonly AppDbContext _context;

    protected BaseService(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get the current user's InstituteUser ID from their AppUser ID
    /// Returns null if user is not associated with an InstituteUser
    /// </summary>
    protected async Task<Guid?> GetCurrentInstituteUserIdAsync(Guid appUserId)
    {
        var instituteUser = await _context.InstituteUsers
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.UserId == appUserId);
        
        return instituteUser?.Id;
    }

    /// <summary>
    /// Check if an entity belongs to the specified InstituteUser
    /// </summary>
    protected bool IsOwner(Guid entityInstituteUserId, Guid? currentInstituteUserId)
    {
        return currentInstituteUserId.HasValue && entityInstituteUserId == currentInstituteUserId.Value;
    }
}