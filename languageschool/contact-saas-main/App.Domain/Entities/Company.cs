using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities;

public class Company : BaseEntity
{
    [StringLength(128, MinimumLength = 3)]
    public required string CompanyName { get; set; } = default!;
    [StringLength(128, MinimumLength = 10)]
    public required string CompanyAddress { get; set; } = default!;
    [StringLength(50, MinimumLength = 8)]
    public required string CompanyPhoneNumber { get; set; } = default!;
    [StringLength(128, MinimumLength = 10)]
    public required string CompanyEmail { get; set; } = default!;
}