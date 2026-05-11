using App.Domain.Entities;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace App.Helpers;

public static class UserRoleHelper
{
    public static async Task SyncCompanyUserRolesToIdentityAsync(
        UserManager<AppUser> userManager,
        AppUser user,
        EInstituteUserRole companyRole)
    {
        // For non-flags enums, we just check the single role
        var roleName = companyRole.ToIdentityRole();

        if (roleName != "None" && !await userManager.IsInRoleAsync(user, roleName))
        {
            await userManager.AddToRoleAsync(user, roleName);
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
            EInstituteUserRole.Employee => "employee",
            _ => "None"
        };
    }

}