using App.Domain.Entities;

namespace WebApp.ViewModels;

public class AnalysisDashboardViewModel
{
    public IEnumerable<ResultListResponse> Results { get; set; } =  new List<ResultListResponse>();
}