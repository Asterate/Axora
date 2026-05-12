using App.Shared.Contracts;

namespace App.Modules.Institute.Application.Interfaces;

public interface IInstituteRepository : IBaseRepository<Domain.Entities.Institute>
{
    Task<List<Domain.Entities.Institute>> GetActivesAsync();
}