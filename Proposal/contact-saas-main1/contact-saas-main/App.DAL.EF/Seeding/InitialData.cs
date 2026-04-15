namespace App.DAL.EF.Seeding;

using App.Domain.Entities;
using App.Domain.Identity;

public static class InitialData
{
    // Define a fixed ID for the system admin to ensure consistency
    public static readonly Guid SystemAdminId = new Guid("12345678-1234-1234-1234-123456789abc");
    public static readonly Guid SystemSupportId = new Guid("12345678-5674-5674-5674-123456789abc");
    public static readonly Guid SystemBillingId = new Guid("12345678-9879-9879-9879-123456789abc");
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
            ("support", null),
            ("billing", null),
            ("owner", null),
            ("instituteadmin", null),
            ("institutemanager", null),
            ("employee", null),
            ("guest", null),
        ];

    // Test user ID (already exists in DB, don't re-create)
    public static readonly Guid TestUserId = new("019d925c-e034-7125-9a72-dfef0d0a037f");
    
    public static readonly (string name, string password, Guid? id, string[] roles, string firstName, string lastName)[]
        Users =
        [
            ("systemadmin@languagesaas.com", "SystemAdmin.2025!", SystemAdminId, ["admin"], "System", "Admin"),
            ("systemsupport@languagesaas.com", "SystemSupport.2025!", SystemSupportId, ["support"], "System", "Support"),
            ("systemBillingt@languagesaas.com", "SystemBilling.2025!", SystemBillingId, ["billing"], "System", "Billing"),
        ];

   

}