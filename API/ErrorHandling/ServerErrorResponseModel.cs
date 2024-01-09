namespace API.ErrorHandling;

public record ServerErrorResponseModel
{
    public required IEnumerable<string> Errors { get; init; }
}