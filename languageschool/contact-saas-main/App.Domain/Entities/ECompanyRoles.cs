namespace App.Domain.Entities;
[Flags]
public enum ECompanyRoles
{
    None    = 0,
    Owner    = 1,
    Admin   = 1 << 1,
    Manager = 1 << 2,
    Teacher = 1 << 3,
    Student = 1 << 4
}