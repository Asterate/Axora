using App.Shared.Domain;

namespace App.Domain.Entities;

public class DocumentType : BaseEntity
{
    public LangStr Name { get; set; } = new();
    
    public LangStr? Description { get; set; }
    public DateTime? DeletedAt { get; set; }
}