using App.Domain.Entities;

namespace WebApp.ViewModels;

public class EstablishmentsViewModel
{
    public IEnumerable<Institute>  Institutes { get; set; } = new List<Institute>();
    public IEnumerable<Lab>  Labs { get; set; } = new List<Lab>();
}