using Core;

namespace Weather_Minimal_Api.Extensions;

public static class ResultExtensions
{
    public static IResult ToProblemDetails(this Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException("Can`t convert success result to problem.");
        }

        return TypedResults.Problem(
            statusCode: StatusCodes.Status400BadRequest,
            title: "Bad Request",
            type: "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            extensions: new Dictionary<string, object?>
            {
                { "errors", new[] { result.Error } }
            });
    }

    public static IResult ToBadRequestProblemDetails(this Error error)
    {
        return TypedResults.Problem(
            statusCode: StatusCodes.Status400BadRequest,
            title: "Bad Request",
            type: "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            extensions: new Dictionary<string, object?>
            {
                { "errors", new[] { error } }
            });
    }

    public static IResult ToNotFoundProblemDetails(this Error error)
    {
        if (error is not NotFoundError)
        {
            throw new InvalidOperationException("Only Not Found Error is convertable to Not Found problem details");
        }

        return TypedResults.Problem(
            statusCode: StatusCodes.Status404NotFound,
            title: "Not Found",
            type: "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
            extensions: new Dictionary<string, object?>
            {
                { "errors", new[] { error } }
            });
    }
}
