using App.Domain.Entities;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace App.Helpers;

public static class UserRoleHelper
{
    public static async Task SyncCompanyUserRolesToIdentityAsync(
        UserManager<AppUser> userManager,
        AppUser user,
        ECompanyRoles companyRoles)
    {
        foreach (ECompanyRoles flag in Enum.GetValues(typeof(ECompanyRoles)))
        {

            if (companyRoles.HasFlag(flag))
            {
                var roleName = flag.ToIdentityRole();

                if (!await userManager.IsInRoleAsync(user, roleName))
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }
    }

    private static string ToIdentityRole(this ECompanyRoles role)
    {
        return role switch
        {
            ECompanyRoles.Owner => "CompanyOwner",
            ECompanyRoles.Admin => "CompanyAdmin",
            ECompanyRoles.Manager => "CompanyManager",
            ECompanyRoles.Teacher => "Teacher",
            ECompanyRoles.Student => "Student",
            _ => "None"
        };
    }

}