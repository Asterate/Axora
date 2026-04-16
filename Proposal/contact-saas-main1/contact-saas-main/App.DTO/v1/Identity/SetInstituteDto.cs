namespace App.DTO.v1.Identity;

public class SetInstituteDto
{
    public int InstituteSelection { get; set; } // 0 = select existing, 1 = create new
    public string? InstituteId { get; set; }
    public NewInstituteDto? NewInstitute { get; set; }
}

public class NewInstituteDto
{
    public string InstituteName { get; set; } = null!;
    public string InstituteCountry { get; set; } = null!;
    public string? InstituteAddress { get; set; }
    public string? InstitutePhoneNumber { get; set; }
    public string? InstituteTypeId { get; set; }
}