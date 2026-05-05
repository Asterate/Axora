using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domain;

namespace App.Domain.Entities;

public class ExperimentType : BaseEntity
{
    public LangStr Name { get; set; } = new();
    
    public LangStr? Description { get; set; }
}