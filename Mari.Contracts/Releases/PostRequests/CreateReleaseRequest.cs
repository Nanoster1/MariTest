using Mari.Contracts.Common.Routes.Server;
using Mari.Http.Common.Classes;
using Mari.Http.Models;
using Mari.Http.Requests;
using static Mari.Contracts.Releases.PostRequests.CreateReleaseRequest;

namespace Mari.Contracts.Releases.PostRequests;

public class CreateReleaseRequest : PostRequest<EmptyRoute, EmptyQuery, Body, VoidResponse>
{
    public const string ConstRouteTemplate = ServerRoutes.Controllers.Release;
    public override string RouteTemplate => ConstRouteTemplate;

    public CreateReleaseRequest(Body body) : base(new(), new(), body)
    {
    }

    public record Body(
        string MainIssue,
        DateTime CompleteDate,
        string PlatformName,
        int VersionMajor,
        int VersionMinor,
        int VersionPatch,
        string Description)
        : RequestBody;
}