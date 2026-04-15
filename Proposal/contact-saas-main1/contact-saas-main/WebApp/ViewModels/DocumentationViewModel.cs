using App.Domain.Entities;

namespace WebApp.ViewModels;

public class DocumentationViewModel
{
    public IEnumerable<Document> Documents { get; set; } =  new List<Document>();
}