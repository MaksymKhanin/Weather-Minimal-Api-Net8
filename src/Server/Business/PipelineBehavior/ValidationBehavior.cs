using Core;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Business.PipelineBehavior;
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidationBehavior<TRequest, TResponse>> logger)
    {
        _validators = validators;
        _logger = logger;
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var failures = _validators
            .Select(x => x.Validate(request))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(validationFailure => validationFailure != null)
            .ToList();

        if (failures.Any())
        {
            var error = string.Join("\r\n", failures);

            _logger.LogError($"Validation failed with error: {error}");
            var result = Result.Fail(new ValidationFailedError(error));

            return (dynamic)Task.FromResult(result);
        }

        return next();
    }
}
