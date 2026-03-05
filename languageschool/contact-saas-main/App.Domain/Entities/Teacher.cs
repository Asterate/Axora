using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities;

public class Teacher : BaseEntity
{
    public Guid CompanyId { get; set; }
    public Company? Company { get; set; }
    public Guid ComapanyUserId { get; set; }
    public CompanyUser? ComapanyUser { get; set; }
    [StringLength(128, MinimumLength = 3)]
    public string TeacherFirstName { get; set; } = default!;
    [StringLength(128, MinimumLength = 3)]
    public string TeacherLastName { get; set; } =  default!;
    [StringLength(128, MinimumLength = 10)]
    public string TeacherEmail { get; set; } = default!;
    [StringLength(50, MinimumLength = 8)]
    public string TeacherPhone { get; set; } = default!;
    [StringLength(128, MinimumLength = 3)]
    public string TeacherAddress { get; set; } = default!;
    [StringLength(128, MinimumLength = 3)]
    public string TeacherNativeLanguage {get; set;} = default!;
    [StringLength(128, MinimumLength = 3)]
    public string TeacherNationality { get; set; } = default!;
    [StringLength(50, MinimumLength = 3)]
    public string TeacherGender { get; set; } = default!;
}