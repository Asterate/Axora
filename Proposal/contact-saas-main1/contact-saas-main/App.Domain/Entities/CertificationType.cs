using System.ComponentModel.DataAnnotations;
using App.Domain;
using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities;

public class CertificationType:BaseEntity
{
    public LangStr Name { get; set; }  = default!;
    
    [StringLength(128, MinimumLength = 3)]
    public string? Description { get; set; }
}