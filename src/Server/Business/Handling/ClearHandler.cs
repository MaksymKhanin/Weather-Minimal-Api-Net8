using AutoMapper;
using Business.Commands;
using Business.Ports;
using Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Business.Handling;
internal class ClearHandler(ILogger<ClearHandler> logger, IMapper mapper, IStorage storage) : IRequestHandler<ClearCommand, Result>
{
    private readonly ILogger<ClearHandler> _logger = logger;
    private readonly IMapper _mapper = mapper;
    private readonly IStorage _storage = storage;

    public async Task<Result> Handle(ClearCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Trying to clear the storage.");

        await _storage.ClearAsync(cancellationToken);

        _logger.LogInformation("Storaged was cleared successfully.");

        return Result.Success();
    }
}
