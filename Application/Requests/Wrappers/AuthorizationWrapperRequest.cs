namespace Application.Requests.Wrappers;

public record AuthorizationWrapperRequest<TRequest>
{
    public string JwtToken { get; init; }
    public TRequest Request { get; init; }

    public AuthorizationWrapperRequest(string authorizationHeader, TRequest request)
    {
        JwtToken = authorizationHeader.Split(" ")[1];
        Request = request;
    }
}