namespace App.DAL.EF.Seeding;

using App.Domain.Entities;
using App.Domain.Identity;

public static class InitialData
{
    // Define a fixed ID for the system admin to ensure consistency
    public static readonly Guid SystemAdminId = new Guid("12345678-1234-1234-1234-123456789abc");

    public static readonly (string roleName, Guid? id)[]
        Roles =
        [
            ("admin", null),
            ("user", null),
            ("root", null),
        ];

    public static readonly (string name, string password, Guid? id, string[] roles, string firstName, string lastName)[]
        Users =
        [
            ("systemadmin@languagesaas.com", "SystemAdmin.2025!", SystemAdminId, ["admin", "root", "user"], "System", "Admin")
        ];
    
}