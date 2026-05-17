using App.Modules.Equipment.Application.Interfaces;
using App.Shared.Contracts;
using MediatR;

namespace App.Modules.Lab.Application.Queries;

public record GetEquipmentByIdQuery(Guid Id) : IRequest<LookupItem?>;

public class GetEquipmentByIdHandler : IRequestHandler<GetEquipmentByIdQuery, LookupItem?>
{
    private readonly IEquipmentRepository _repo;
    public GetEquipmentByIdHandler(IEquipmentRepository repo) => _repo = repo;

    public async Task<LookupItem?> Handle(GetEquipmentByIdQuery request, CancellationToken ct)
    {
        var lab = await _repo.GetByIdAsync(request.Id);
        if (lab == null) return null;
        return new LookupItem { Id = lab.Id, Name = lab.EquipmentName.Translate()  ??  String.Empty };
    }
}