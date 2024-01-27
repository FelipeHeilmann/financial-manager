namespace FinancialManager.API.Extension;
public static class ResultExtension
{
    public static IResult ToProblemDetail(this Domain.Abstraction.Result result)
    {
        return Results.Problem(
            statusCode: GetStatusCode(result.GetError().Type),
            title: GetTitle(result.GetError().Type),
            extensions: new Dictionary<string, object?>
            {
                { "error", new[] {result.GetError()} }
            });
    }

    static int GetStatusCode(Domain.Abstraction.ErrorType errorType) =>
        errorType switch
        {
            Domain.Abstraction.ErrorType.Validation => 400,
            Domain.Abstraction.ErrorType.NotFound => 404,
            Domain.Abstraction.ErrorType.Conflict => 409,
            _ => 500
        };

    static string GetTitle(Domain.Abstraction.ErrorType errorType) =>
        errorType switch
        {
            Domain.Abstraction.ErrorType.Validation => "Validation Error",
            Domain.Abstraction.ErrorType.NotFound => "Not found",
            Domain.Abstraction.ErrorType.Conflict => "Conflict",
            _ => "Internal error"
        };
}

