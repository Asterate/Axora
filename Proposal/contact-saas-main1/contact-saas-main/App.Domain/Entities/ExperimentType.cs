using App.Domain;

namespace App.Domain.Entities;

public class ExperimentType : BaseEntity
{
    public LangStr ExperimentTypeName { get; set; } = new();
    public LangStr? Description { get; set; }
}