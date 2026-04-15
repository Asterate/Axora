using App.Domain.Entities;

namespace WebApp.ViewModels;

public class AnalysisDashboardViewModel
{
    public IEnumerable<Result> Results { get; set; } =  new List<Result>();
}