using Mari.Contracts.Common.Routes.Server;
using Mari.Contracts.Releases.Dto;
using Mari.Http.Common.Classes;
using Mari.Http.Requests;

namespace Mari.Contracts.Releases.PutRequests;

public class UpdateDraftReleaseRequest : PutRequest
{
    public const string ConstRouteTemplate = $"{ServerRoutes.Controllers.Release}/draft";
    public override string RouteTemplate => ConstRouteTemplate;

    public UpdateDraftReleaseRequest(Body body) : base(body: body)
    {
    }

    public record Body(
        Guid Id,
        ReleaseVersionDto? Version,
        string? PlatformName,
        string? Status,
        DateTime? CompleteDate,
        string? MainIssue,
        string? Description)
        : RequestBody;
}
