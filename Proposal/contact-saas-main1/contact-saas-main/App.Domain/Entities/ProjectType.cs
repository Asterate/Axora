using App.Domain;

namespace App.Domain.Entities;

public class ProjectType : BaseEntity
{
    public LangStr Name { get; set; } = new();
    public LangStr? Description { get; set; }
}