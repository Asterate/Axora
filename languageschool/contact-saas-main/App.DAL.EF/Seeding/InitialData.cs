namespace App.DAL.EF.Seeding;

using App.Domain.Entities;
using App.Domain.Identity;

public static class InitialData
{
    // Define a fixed ID for the system admin to ensure consistency
    public static readonly Guid SystemAdminId = new Guid("12345678-1234-1234-1234-123456789abc");
    public static readonly Guid LevelA1Id = new Guid("a1111111-1111-1111-1111-111111111111");
    public static readonly Guid LevelA2Id = new Guid("a2222222-2222-2222-2222-222222222222");
    public static readonly Guid LevelB1Id = new Guid("b1111111-1111-1111-1111-111111111111");
    public static readonly Guid LevelB2Id = new Guid("b2222222-2222-2222-2222-222222222222");
    public static readonly Guid LevelC1Id = new Guid("c1111111-1111-1111-1111-111111111111");
    public static readonly Guid LevelC2Id = new Guid("c2222222-2222-2222-2222-222222222222");

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