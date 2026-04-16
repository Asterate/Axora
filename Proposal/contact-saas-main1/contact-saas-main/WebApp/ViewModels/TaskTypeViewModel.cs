using System;

namespace WebApp.ViewModels
{
    public class TaskTypeViewModel
    {
        public Guid Id { get; set; }
        
        // English fields
        public string TaskTypeNameEn { get; set; } = string.Empty;
        public string? TaskTypeDescriptionEn { get; set; }
        
        // Estonian fields
        public string TaskTypeNameEt { get; set; } = string.Empty;
        public string? TaskTypeDescriptionEt { get; set; }
    }
}