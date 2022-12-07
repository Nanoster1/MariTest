using ErrorOr;
using Mari.Application.Common.Interfaces.Persistence;
using MediatR;
using Mari.Domain.Common;
using Mari.Domain.Releases.Enums;
using Mari.Application.Common.Interfaces.CommonServices;

namespace Mari.Application.Releases.Commands.SetInWorkStatus;

public class SetInWorkStatusCommandHandler : IRequestHandler<SetInWorkStatusCommand, ErrorOr<Updated>>
{
    private readonly IReleaseRepository _releaseRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public SetInWorkStatusCommandHandler(
        IReleaseRepository releaseRepository,
        IDateTimeProvider dateTimeProvider)
    {
        _releaseRepository = releaseRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<ErrorOr<Updated>> Handle(SetInWorkStatusCommand request, CancellationToken cancellationToken)
    {
        var release = await _releaseRepository.GetById(request.ReleaseId, cancellationToken);
        if (release is null) return Errors.Release.ReleaseNotFound;
        var result = release.ChangeStatus(ReleaseStatus.Developing, _dateTimeProvider.UtcNow);
        if (result.IsError) return result.Errors;
        await _releaseRepository.Update(release, cancellationToken);
        return Result.Updated;
    }
}
