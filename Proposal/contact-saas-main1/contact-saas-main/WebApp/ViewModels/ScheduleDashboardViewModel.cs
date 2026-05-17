using App.Modules.Lab.Application.Interfaces.Service;
using App.Modules.Project.Application.DTO;
using App.Modules.Project.Application.Interfaces.Service;
using App.Shared.Contracts;

namespace WebApp.ViewModels;

public class ScheduleDashboardViewModel
{
    public IEnumerable<ScheduleListResponse> Schedules { get; set; } = new List<ScheduleListResponse>();
    public ScheduleResponse ScheduleRespone { get; set; } =  new ();
    public SaveScheduleRequest ScheduleRequest { get; set; } = new ();
    public IEnumerable<LookupItem> Labs { get; set; } = [];
    public IEnumerable<LookupItem> Equipments { get; set; } = [];
    public IEnumerable<LookupItem> Experiments { get; set; } = [];
    public static async Task<ScheduleDashboardViewModel> ForCreate(ILabService labService, IExperimentService experimentService, IEquipmentService equipmentService,
        Guid instituteId) => new()
    {
        Labs = await labService.GetActivesAsync(),
        Equipments = await equipmentService.GetActivesAsync(),
        Experiments = await experimentService.GetActivesAsync(instituteId),
    };
    public static async Task<ScheduleDashboardViewModel> ForEdit(
        SaveScheduleRequest schedule,
        ILabService labService,
        IExperimentService experimentService,
        IEquipmentService equipmentService, Guid instituteId) => new()
    {
        ScheduleRequest = schedule,
        Labs = await labService.GetActivesAsync(),
        Experiments = await experimentService.GetActivesAsync(instituteId),
        Equipments = await equipmentService.GetActivesAsync(),
    };

    public static async Task<ScheduleDashboardViewModel> ForIndex(IScheduleService scheduleService) => new()
    {
        Schedules = await scheduleService.GetAllAsync()
    };

}