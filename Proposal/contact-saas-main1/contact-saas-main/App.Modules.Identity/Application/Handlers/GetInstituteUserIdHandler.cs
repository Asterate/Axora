using App.Modules.Identity.Application.Interfaces;
using App.Shared.Contracts.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App.Modules.Identity.Application.Handlers;

public class GetInstituteUserIdHandler
{
    public class GetInstituteIdByUserIdHandler : IRequestHandler<InstituteUserEvent.GetInstituteIdByUserIdQuery, Guid?>
    {
        private readonly IInstituteUserService _repo;
        private readonly ILogger _logger;

        public GetInstituteIdByUserIdHandler(IInstituteUserService repo,   ILogger<InstituteUserHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<Guid?> Handle(InstituteUserEvent.GetInstituteIdByUserIdQuery request, CancellationToken ct)
        {
            var instituteUser = await _repo.GetByUserIdAsync(request.UserId);
            return instituteUser?.InstituteId;
        }
    }
}