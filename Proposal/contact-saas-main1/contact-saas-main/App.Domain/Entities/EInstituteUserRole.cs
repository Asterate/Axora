namespace App.Domain.Entities;

public enum EInstituteUserRole
{
    Employee,
    Manager, //should not exist
    Administrator,
    Guest,
    Technician, //no need to exist
    Owner
}