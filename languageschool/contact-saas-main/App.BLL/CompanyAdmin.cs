using System.Text;
using App.DAL.EF;
using App.Domain.Entities;
using App.Domain.Identity;
using App.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace App.BLL;

public class CompanyAdmin
{
    
    public static async Task ApproveCompany(AppDbContext context, UserManager<AppUser> userManager, Guid companyId)
    {
        var company = await context.Companies
            .Include(c => c.CompanyUsers)
            .FirstOrDefaultAsync(c => c.Id == companyId);


        if (company == null) throw new InvalidOperationException("Company not found");
        
        company.CompanyStatus = ECompanyStatus.Approved;
        var newOwner = company.CompanyUsers.First();
        newOwner.Roles = ECompanyRoles.Owner;
        context.Update(company);
        context.Update(newOwner);
        await context.SaveChangesAsync(); // save before syncing roles
        var appUser = await userManager.FindByIdAsync(newOwner.AppUserId.ToString());
        if (appUser != null)
        {
            await UserRoleHelper.SyncCompanyUserRolesToIdentityAsync(userManager, appUser, newOwner.Roles);
        }


        await context.SaveChangesAsync();
    }
    
    public static async Task RejectCompany(AppDbContext context, Guid companyId)
    {
        var company = await context.Companies.FindAsync(companyId);
        if (company == null) throw new InvalidOperationException("Company not found");

        company.CompanyStatus = ECompanyStatus.Rejected;
        context.Entry(company).Property(c => c.CompanyStatus).IsModified = true;

        // Remove all company user associations for the rejected company
        var companyUsers = await context.CompanyUsers
            .Where(cu => cu.CompanyId == companyId)
            .ToListAsync();
            
        context.CompanyUsers.RemoveRange(companyUsers);

        await context.SaveChangesAsync();
    }
    
    public static async Task<List<Company>> GetPendingCompanies(AppDbContext context)
    {
        return await context.Companies
            .Where(c => ECompanyStatus.Pending == c.CompanyStatus)
            .Include(c => c.CompanyUsers)
            .ThenInclude(cu => cu.AppUser)
            .ToListAsync();
    }
    
    public static async Task<List<Company>> GetAllProcessedCompanies(AppDbContext context)
    {
        return await context.Companies
            .Include(c => c.CompanyUsers)
            .ThenInclude(cu => cu.AppUser)
            .Where(c => c.CompanyStatus == ECompanyStatus.Approved 
                        || c.CompanyStatus == ECompanyStatus.Rejected)
            .ToListAsync();
    }
    
    public static async Task DeleteCompany(AppDbContext context, Guid companyId)
    {
        var companyConfig = await context.CompanyConfigs
            .Where(c => c.CompanyId == companyId)
            .FirstOrDefaultAsync();
        if (companyConfig != null) context.CompanyConfigs.Remove(companyConfig);
        
        var companySubs = await context.Subs
            .Where(c => c.CompanyId == companyId)
            .FirstOrDefaultAsync();
        if (companyConfig != null)
            if (companySubs != null)
                context.Subs.Remove(companySubs);
        
        // Remove all company user associations first to avoid foreign key constraint violation
        var companyUsers = await context.CompanyUsers
            .Where(cu => cu.CompanyId == companyId)
            .ToListAsync();
            
        context.CompanyUsers.RemoveRange(companyUsers);
        
        // Then delete the company
        var company = await context.Companies.FindAsync(companyId);
        if (company == null) throw new InvalidOperationException("Company not found");
        
        context.Companies.Remove(company);
        
        await context.SaveChangesAsync();
    }
}