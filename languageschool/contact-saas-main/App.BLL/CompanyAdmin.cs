using System.Text;
using App.DAL.EF;
using App.Domain.Entities;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace App.BLL;

public class CompanyAdmin
{
    public static void AddCompanyUser(){}
    public static void DeleteCompanyUser(){}
    public static void UpdateCompanyUser(){}
    public static void LoadCompanyUser(){}
    public static void UpdateCompanyConfigTimeZone(){}
    public static void UpdateCompanyConfigAllowOneOnOneSessions(){}
    public static void UpdateCompanyConfigEnableMaterialTracking(){}
    public static void UpdateCompanyConfigMaxStudentsPerCourse(){}
    public static void UpdateCompanyConfigEnableCertificates(){}
    
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
        
        await context.SaveChangesAsync();
    }

    

    // Sanitize string for email generation - replace special characters
    private static string SanitizeForEmail(string input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;
            
        // Replace special characters with their ASCII equivalents
        var sanitized = input
            .Replace("ä", "a")
            .Replace("ö", "o")
            .Replace("ü", "u")
            .Replace("õ", "o")
            .Replace("å", "a")
            .Replace("ø", "o")
            .Replace("é", "e")
            .Replace("è", "e")
            .Replace("ê", "e")
            .Replace("á", "a")
            .Replace("à", "a")
            .Replace("â", "a")
            .Replace("í", "i")
            .Replace("ì", "i")
            .Replace("î", "i")
            .Replace("ó", "o")
            .Replace("ò", "o")
            .Replace("ô", "o")
            .Replace("ú", "u")
            .Replace("ù", "u")
            .Replace("û", "u");
            
        // Remove any remaining non-alphanumeric characters (except spaces and underscores)
        return System.Text.RegularExpressions.Regex.Replace(sanitized, "[^a-zA-Z0-9_ ]", "");
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
    
    public static async Task<List<Company>> GetApprovedCompanies(AppDbContext context)
    {
        return await context.Companies
            .Include(c => c.CreatedBy)
            .Where(c => c.CompanyStatus == ECompanyStatus.Approved)
            .ToListAsync();
    }
    
    public static async Task<List<Company>> GetRejectedCompanies(AppDbContext context)
    {
        return await context.Companies
            .Include(c => c.CreatedBy)
            .Where(c => c.CompanyStatus == ECompanyStatus.Rejected)
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