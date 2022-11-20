using System.Net.Http.Json;
using Mari.Http.Common;
using Mari.Http.Models;
using Mari.Http.Requests;
using Throw;

namespace Mari.Http.Services;

public class HttpSender
{
    private readonly IHttpClientFactory _httpClientFactory;

    private string? clientName;

    public HttpSender(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public HttpClient Client { get; set; } = null!;

    public string? HttpClientName
    {
        get => clientName;
        set => Client = string.IsNullOrWhiteSpace(clientName = value)
            ? _httpClientFactory.CreateClient()
            : _httpClientFactory.CreateClient(clientName);
    }

    public async Task<ProblemOr<TResponse>> GetAsync<TRoute, TQuery, TResponse>(
        GetRequest<TRoute, TQuery, TResponse> request,
        CancellationToken cancellationToken = default)
        where TResponse : notnull
    {
        var absoluteRoute = CreateUri(request, Client.BaseAddress);
        var response = await Client.GetAsync(absoluteRoute, cancellationToken);
        return await HandleHttpResponse<TResponse>(response, cancellationToken);
    }

    public async Task<ProblemOr<TResponse>> PostAsync<TRoute, TQuery, TBody, TResponse>(
        PostRequest<TRoute, TQuery, TBody, TResponse> request,
        CancellationToken cancellationToken = default)
        where TResponse : notnull
    {
        var absoluteRoute = CreateUri(request, Client.BaseAddress);
        var response = await Client.PostAsJsonAsync(absoluteRoute, request, cancellationToken);
        return await HandleHttpResponse<TResponse>(response, cancellationToken);
    }

    public async Task<ProblemOr<VoidResponse>> PutAsync<TRoute, TQuery, TBody>(
        PutRequest<TRoute, TQuery, TBody> request,
        CancellationToken cancellationToken = default)
    {
        var absoluteRoute = CreateUri(request, Client.BaseAddress);
        var response = await Client.PutAsJsonAsync(absoluteRoute, cancellationToken);
        return await HandleHttpResponse<VoidResponse>(response, cancellationToken);
    }

    public async Task<ProblemOr<TResponse>> DeleteAsync<TRoute, TQuery, TResponse>(
        DeleteRequest<TRoute, TQuery, TResponse> request,
        CancellationToken cancellationToken = default)
        where TResponse : notnull
    {
        var absoluteRoute = CreateUri(request, Client.BaseAddress);
        var response = await Client.DeleteAsync(absoluteRoute, cancellationToken);
        return await HandleHttpResponse<TResponse>(response, cancellationToken);
    }

    public async Task<ProblemOr<TResponse>> PatchAsync<TRoute, TQuery, TBody, TResponse>(
        PatchRequest<TRoute, TQuery, TBody, TResponse> request,
        CancellationToken cancellationToken = default)
        where TResponse : notnull
    {
        var absoluteRoute = CreateUri(request, Client.BaseAddress);
        var response = await Client.PatchAsJsonAsync(absoluteRoute, request, cancellationToken);
        return await HandleHttpResponse<TResponse>(response, cancellationToken);
    }

    private async Task<ProblemOr<TResponse>> HandleHttpResponse<TResponse>(
        HttpResponseMessage httpResponse,
        CancellationToken cancellationToken)
        where TResponse : notnull
    {
        ProblemOr<TResponse> result;

        if (httpResponse.IsSuccessStatusCode)
        {
            var response = await httpResponse.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken);
            response.ThrowIfNull();
            result = new(
                httpResponse: httpResponse,
                response: response);
        }

        var problem = await httpResponse.Content.ReadFromJsonAsync<ProblemDetails>(cancellationToken: cancellationToken);
        problem.ThrowIfNull();
        result = new(
            httpResponse: httpResponse,
            problem: problem);

        return result;
    }

    private Uri CreateUri<TResponse>(IRequest<TResponse> request, Uri? baseAddress = null)
        where TResponse : notnull
    {
        var routeUri = new Uri(request.GetRoute(), UriKind.RelativeOrAbsolute);
        var builderUri = routeUri.IsAbsoluteUri ? routeUri : new Uri(baseAddress!, routeUri);
        var builder = new UriBuilder(builderUri);
        builder.Query = request.GetQueryString();
        return builder.Uri;
    }
}