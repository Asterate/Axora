using System.Text;
using App.DAL.EF;
using App.Domain.Entities;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace App.BLL;

public class CompanyOwnerService
{
    // Get all companies owned by a specific user
    public static async Task<List<Company>> GetCompaniesOwnedByUser(AppDbContext context, Guid appUserId)
    {
        var companies = await context.Companies
            .Where(c => c.CompanyUsers.Any(cu => cu.AppUserId == appUserId && cu.Roles.HasFlag(ECompanyRoles.Owner)))
            .ToListAsync();
        return companies;
    }

    // Get company details with associated users
    public static async Task<Company?> GetCompanyWithUsers(AppDbContext context, Guid companyId)
    {
        var company = await context.Companies
            .Include(c => c.CompanyUsers)
            .ThenInclude(cu => cu.AppUser)
            .FirstOrDefaultAsync(c => c.Id == companyId);
        return company;
    }

    // Create a new user and assign them a company role (admin, manager, or teacher)
    public static async Task<(AppUser User, string InitialPassword)> CreateCompanyUser(AppDbContext context, string email, string firstName, string lastName, ECompanyRoles role, Guid companyId, UserManager<AppUser> userManager)
    {
        var newUser = new AppUser
        {
            UserName = email,
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            EmailConfirmed = true
        };

        // Generate a random initial password
        var initialPassword = GenerateRandomPassword();
        
        var result = await userManager.CreateAsync(newUser, initialPassword);
        if (!result.Succeeded)
        {
            throw new ApplicationException($"User creation failed: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

        var companyUser = new CompanyUser
        {
            CompanyId = companyId,
            AppUserId = newUser.Id,
            Roles = role
        };

        context.CompanyUsers.Add(companyUser);
        await context.SaveChangesAsync();

        return (newUser, initialPassword);
    }

    // Generate a random password
    // Generate a random secure password
    private static string GenerateRandomPassword(int length = 12)
    {
        if (length < 8) length = 8; // enforce minimum length

        const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string lower = "abcdefghijklmnopqrstuvwxyz";
        const string digits = "0123456789";
        const string symbols = "!@#$%^&*()-_=+[]{}|;:,.<>?";

        // make sure password has at least one of each
        var random = new Random();
        var password = new StringBuilder();

        password.Append(upper[random.Next(upper.Length)]);
        password.Append(lower[random.Next(lower.Length)]);
        password.Append(digits[random.Next(digits.Length)]);
        password.Append(symbols[random.Next(symbols.Length)]);

        // fill the rest randomly from all categories
        var allChars = upper + lower + digits + symbols;
        for (int i = 4; i < length; i++)
        {
            password.Append(allChars[random.Next(allChars.Length)]);
        }

        // shuffle so first 4 characters aren’t predictable
        var shuffled = password.ToString().ToCharArray().OrderBy(c => random.Next()).ToArray();
        return new string(shuffled);
    }

    // Get all users with specific roles in a company
    public static async Task<List<CompanyUser>> GetCompanyUsersByRole(AppDbContext context, Guid companyId, ECompanyRoles role)
    {
        return await context.CompanyUsers
            .Where(cu => cu.CompanyId == companyId && cu.Roles.HasFlag(role))
            .Include(cu => cu.AppUser)
            .ToListAsync();
    }

    // Update company information
    public static async Task UpdateCompanyInformation(AppDbContext context, Guid companyId, string companyName, string companyAddress, string companyPhoneNumber, string companyEmail)
    {
        var company = await context.Companies.FindAsync(companyId);
        if (company != null)
        {
            company.CompanyName = companyName;
            company.CompanyAddress = companyAddress;
            company.CompanyPhoneNumber = companyPhoneNumber;
            company.CompanyEmail = companyEmail;
            await context.SaveChangesAsync();
        }
    }

    // Load company information
    public static async Task<Company?> LoadCompanyInformation(AppDbContext context, Guid companyId)
    {
        return await context.Companies.FindAsync(companyId);
    }
}