using App.Modules.Lab.Application.DTO;
using App.Modules.Project.Application.DTO;

namespace WebApp.ViewModels;

public class EstablishmentsViewModel
{
    public IEnumerable<InstituteResponse>  Institutes { get; set; } = new List<InstituteResponse>();

    public IEnumerable<LabResponse>  Labs { get; set; } = new List<LabResponse>();
}