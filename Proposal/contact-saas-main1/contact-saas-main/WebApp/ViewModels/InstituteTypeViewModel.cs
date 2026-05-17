using App.Modules.Project.Application.DTO;

namespace WebApp.ViewModels
{
    public class InstituteTypeViewModel
    {
        public InstituteTypeResponse InstituteTypesResponse { get; set; } =  new ();
        public SaveInstituteTypeRequest InstituteTypesRequest { get; set; } =  new ();
    }
}
