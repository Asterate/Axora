using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities;

public class Student : BaseEntity
{
    public Guid CompanyId { get; set; } = default!;
    public Company Company { get; set; } = default!;
    public Guid CompanyUserId { get; set; } = default!;
    public CompanyUser CompanyUser { get; set; } = default!;
    [StringLength(128, MinimumLength = 3)]
    public string StudentFirstName { get; set; } = default!;
    [StringLength(128, MinimumLength = 3)]
    public string StudentLastName { get; set; } =  default!;
    [StringLength(128, MinimumLength = 10)]
    public string StudentEmail { get; set; } = default!;
    [StringLength(50, MinimumLength = 8)]
    public string StudentPhone { get; set; } = default!;
    [StringLength(128, MinimumLength = 3)]
    public string StudentAddress { get; set; } = default!;
    [StringLength(128, MinimumLength = 3)]
    public string StudentNativeLanguage {get; set;} = default!;
    [StringLength(128, MinimumLength = 3)]
    public string StudentNationality { get; set; } = default!;
    [StringLength(50, MinimumLength = 3)]
    public string StudentGender { get; set; } = default!;
    
}