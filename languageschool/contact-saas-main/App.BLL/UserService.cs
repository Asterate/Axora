using App.DAL.EF;
using App.Domain.Entities;
using App.Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace App.BLL;

public class UserService
{
    // Add a role to a company user
    public static async Task AddRoleToCompanyUser(AppDbContext context, Guid companyId, Guid appUserId, ECompanyRoles role)
    {
        var companyUser = await context.CompanyUsers
            .FirstOrDefaultAsync(cu => cu.CompanyId == companyId && cu.AppUserId == appUserId);

        if (companyUser == null)
        {
            companyUser = new CompanyUser
            {
                CompanyId = companyId,
                AppUserId = appUserId,
                Roles = role
            };
            context.CompanyUsers.Add(companyUser);
        }
        else
        {
            companyUser.Roles |= role;
            context.Entry(companyUser).Property(cu => cu.Roles).IsModified = true;
        }

        await context.SaveChangesAsync();
    }

    // Remove a role from a company user
    public static async Task RemoveRoleFromCompanyUser(AppDbContext context, Guid companyId, Guid appUserId, ECompanyRoles role)
    {
        var companyUser = await context.CompanyUsers
            .FirstOrDefaultAsync(cu => cu.CompanyId == companyId && cu.AppUserId == appUserId);

        if (companyUser != null)
        {
            companyUser.Roles &= ~role;
            context.Entry(companyUser).Property(cu => cu.Roles).IsModified = true;
            await context.SaveChangesAsync();
        }
    }

    // Update all roles of a company user
    public static async Task UpdateCompanyUserRoles(AppDbContext context, Guid companyId, Guid appUserId, ECompanyRoles roles)
    {
        var companyUser = await context.CompanyUsers
            .FirstOrDefaultAsync(cu => cu.CompanyId == companyId && cu.AppUserId == appUserId);

        if (companyUser == null)
        {
            companyUser = new CompanyUser
            {
                CompanyId = companyId,
                AppUserId = appUserId,
                Roles = roles
            };
            context.CompanyUsers.Add(companyUser);
        }
        else
        {
            companyUser.Roles = roles;
            context.Entry(companyUser).Property(cu => cu.Roles).IsModified = true;
        }

        await context.SaveChangesAsync();
    }

    // Get all roles of a company user
    public static async Task<ECompanyRoles> GetCompanyUserRoles(AppDbContext context, Guid companyId, Guid appUserId)
    {
        var companyUser = await context.CompanyUsers
            .FirstOrDefaultAsync(cu => cu.CompanyId == companyId && cu.AppUserId == appUserId);

        return companyUser?.Roles ?? ECompanyRoles.None;
    }

    // Get all users with roles for a company
    public static async Task<List<CompanyUser>> GetCompanyUsersWithRoles(AppDbContext context, Guid companyId)
    {
        return await context.CompanyUsers
            .Where(cu => cu.CompanyId == companyId)
            .Include(cu => cu.AppUser)
            .ToListAsync();
    }

    // Delete a company user (removes all roles)
    public static async Task DeleteCompanyUser(AppDbContext context, Guid companyId, Guid appUserId)
    {
        var companyUser = await context.CompanyUsers
            .FirstOrDefaultAsync(cu => cu.CompanyId == companyId && cu.AppUserId == appUserId);

        if (companyUser != null)
        {
            context.CompanyUsers.Remove(companyUser);
            await context.SaveChangesAsync();
        }
    }

    // Check if a user has a specific role in a company
    public static async Task<bool> HasRoleInCompany(AppDbContext context, Guid companyId, Guid appUserId, ECompanyRoles role)
    {
        var companyUser = await context.CompanyUsers
            .FirstOrDefaultAsync(cu => cu.CompanyId == companyId && cu.AppUserId == appUserId);

        return companyUser != null && (companyUser.Roles & role) == role;
    }
}
