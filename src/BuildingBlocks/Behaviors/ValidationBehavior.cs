using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.Behaviors;

public class ValidationBehavior<TReuqest, TResponse>
    (IEnumerable<IValidator<TReuqest>> validators)
    : IPipelineBehavior<TReuqest, TResponse>
    where TReuqest : ICommand<TResponse>
{
    public async Task<TResponse> Handle(TReuqest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TReuqest>(request);
        var validationRules =
            await Task.WhenAll(validators.Select(x => x.ValidateAsync(context, cancellationToken)));

        var failures = validationRules
            .Where(r => r.Errors.Any())
            .SelectMany(r=> r.Errors)
            .ToList();

        if (failures.Any())
            throw new ValidationException(failures);

        return await next();
    }
}