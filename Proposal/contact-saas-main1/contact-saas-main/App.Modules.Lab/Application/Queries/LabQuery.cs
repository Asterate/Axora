// App.Modules.Lab/Application/Queries/GetLabByIdQuery.cs

using App.Modules.Lab.Application.Interfaces;
using App.Shared.Contracts;
using MediatR;

namespace App.Modules.Lab.Application.Queries;

public record GetLabByIdQuery(Guid Id) : IRequest<LookupItem?>;

public class GetLabByIdHandler : IRequestHandler<GetLabByIdQuery, LookupItem?>
{
    private readonly ILabRepository _repo;
    public GetLabByIdHandler(ILabRepository repo) => _repo = repo;

    public async Task<LookupItem?> Handle(GetLabByIdQuery request, CancellationToken ct)
    {
        var lab = await _repo.GetByIdAsync(request.Id);
        if (lab == null) return null;
        return new LookupItem { Id = lab.Id, Name = lab.LabName.Translate() ?? String.Empty };
    }
}