using App.Domain.Entities;
using App.Shared.Contracts;

namespace WebApp.ViewModels;

public class DocumentationViewModel
{
    public IEnumerable<DocumentResponse> Documents { get; set; } =  new List<DocumentResponse>();
    public UpdateDocumentRequest  Request { get; set; } = new();
    public List<LookupItem> DocumentTypes { get; set; } = new();
}