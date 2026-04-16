namespace WebApp.ViewModels;

public class DocumentTypeViewModel
{
    public Guid Id { get; set; }
        
    // English fields
    public string NameEn { get; set; } = string.Empty;
    public string? DescriptionEn { get; set; }
        
    // Estonian fields
    public string NameEt { get; set; } = string.Empty;
    public string? DescriptionEt { get; set; }
}