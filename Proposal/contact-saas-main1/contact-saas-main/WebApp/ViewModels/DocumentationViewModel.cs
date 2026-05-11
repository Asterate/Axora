using App.Domain.Entities;

namespace WebApp.ViewModels;

public class DocumentationViewModel
{
    public IEnumerable<DocumentListResponse> Documents { get; set; } =  new List<DocumentListResponse>();
}