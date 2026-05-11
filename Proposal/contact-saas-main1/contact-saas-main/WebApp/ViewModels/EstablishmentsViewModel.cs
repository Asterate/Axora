using App.Domain.Entities;

namespace WebApp.ViewModels;

public class EstablishmentsViewModel
{
    public IEnumerable<InstituteListResponse>  Institutes { get; set; } = new List<InstituteListResponse>();

    public IEnumerable<LabListResponse>  Labs { get; set; } = new List<LabListResponse>();
}