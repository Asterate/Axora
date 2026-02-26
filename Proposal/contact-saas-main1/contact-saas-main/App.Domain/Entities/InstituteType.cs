using App.Domain;
using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities;

public class InstituteType : BaseEntity
{
    [StringLength(128, MinimumLength = 3)]
    public string Name { get; set; }  = default!;
    [StringLength(128, MinimumLength = 3)]
    public string? Description { get; set; }
}