using App.Shared.Contracts;

namespace App.Modules.Institute.Application.Interfaces;

public interface IInstituteRepository : IBaseRepository<Project.Domain.Institute>
{
    Task<List<Project.Domain.Institute>> GetActivesAsync();
}