using Mari.Contracts.Common.Routes.Server;
using Mari.Http.Common.Classes;
using Mari.Http.Models;
using Mari.Http.Requests;

namespace Mari.Contracts.Comments.PostRequests;

public class CreateCommentRequest : PostRequest<VoidResponse>
{
    public const string ConstRouteTemplate = $"{ServerRoutes.Controllers.Comment}";
    public override string RouteTemplate => ConstRouteTemplate;

    public CreateCommentRequest(Body body) : base(body: body)
    {
    }
    
    public record Body(
        Guid releaseId,
        int userId,
        string userName,
        string message
        ):RequestBody;
}
