using App.Domain;

namespace App.Domain.Entities;

public class InstituteType : BaseEntity
{
    public LangStr Name { get; set; } = new();
    public LangStr? Description { get; set; }
}