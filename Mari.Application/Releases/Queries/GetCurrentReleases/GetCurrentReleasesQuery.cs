using ErrorOr;
using Mari.Application.Common.Shared.Paging;
using Mari.Application.Releases.Results;
using MediatR;

namespace Mari.Application.Releases.Queries.GetCurrentReleases;

public record GetCurrentReleasesQuery(int page, int pageSize) :
    PageRequest(page, pageSize),
    IRequest<ErrorOr<IEnumerable<ReleaseResult>>>;
