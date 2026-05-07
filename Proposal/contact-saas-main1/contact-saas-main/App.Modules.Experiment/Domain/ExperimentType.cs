using App.Shared.Domain;

namespace App.Domain.Entities;

public class ExperimentType : BaseEntity
{
    public LangStr Name { get; set; } = new();
    
    public LangStr? Description { get; set; }
}