using App.Domain.Entities;
using App.Modules.Project.Application.DTO;

namespace WebApp.ViewModels;

public class AnalysisDashboardViewModel
{
    public IEnumerable<ResultListResponse> Results { get; set; } =  new List<ResultListResponse>();
}