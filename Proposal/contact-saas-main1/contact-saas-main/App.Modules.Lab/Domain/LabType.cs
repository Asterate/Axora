using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domain;
using App.Shared.Domain;

namespace App.Domain.Entities;

public class LabType : BaseEntity
{
    public LangStr Name { get; set; } = new();
    
    public LangStr? Description { get; set; }
}