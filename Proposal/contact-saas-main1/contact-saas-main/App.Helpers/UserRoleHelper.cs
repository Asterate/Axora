using App.Domain.Entities;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace App.Helpers;

public static class UserRoleHelper
{
    public static async Task SyncCompanyUserRolesToIdentityAsync(
        UserManager<AppUser> userManager,
        AppUser user,
        EInstituteUserRole companyRoles)
    {
        foreach (EInstituteUserRole flag in Enum.GetValues(typeof(EInstituteUserRole)))
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

    private static string ToIdentityRole(this EInstituteUserRole role)
    {
        return role switch
        {
            EInstituteUserRole.Owner => "owner",
            EInstituteUserRole.Administrator => "instituteadmin",
            EInstituteUserRole.Manager => "institutemanager",
            EInstituteUserRole.Guest => "guest",
            EInstituteUserRole.Technician => "Technician",
            _ => "None"
        };
    }

}