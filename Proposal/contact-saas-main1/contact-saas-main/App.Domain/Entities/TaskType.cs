using App.Domain;

namespace App.Domain.Entities;

public class TaskType : BaseEntity
{
    public LangStr TaskTypeName { get; set; } = new();
    public LangStr? TaskTypeDescription { get; set; }
}