using App.Domain.Entities;

namespace WebApp.ViewModels;

public class EntityLogViewModel
{
    public string EntityName { get; set; } = default!;
    public string EntityIcon { get; set; } = "fas fa-user";
    public string LogPageAction { get; set; } = default!;
    public string LogPageController { get; set; } = default!;
    public List<EntityLogItemViewModel> Items { get; set; } = new List<EntityLogItemViewModel>();
    public string? NoItemsMessage { get; set; }
}

    public class EntityLogItemViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string? Subtitle { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }
        public string? BadgeText { get; set; }
        public string? BadgeClass { get; set; }
        public Dictionary<string, string>? AdditionalData { get; set; }
        public string LogActionText { get; set; } = "View Log";
        public string LogActionIcon { get; set; } = "fas fa-list";
        public IDictionary<string, string>? RouteValues { get; set; }
    }