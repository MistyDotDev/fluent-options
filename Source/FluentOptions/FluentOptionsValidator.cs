using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MistyDotDev.FluentOptions;

/// <summary>
/// Validates an instance of <typeparamref name="TOptions"/> using FluentValidation.
/// </summary>
/// <typeparam name="TOptions">The options type to validate.</typeparam>
internal sealed class FluentOptionsValidator<TOptions> : IValidateOptions<TOptions> where TOptions : class
{
    private readonly IServiceScopeFactory _scopeFactory;

    public FluentOptionsValidator(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public ValidateOptionsResult Validate(string? _, TOptions options)
    {
        using var scope = _scopeFactory.CreateScope();
        var validator = scope.ServiceProvider.GetRequiredService<IValidator<TOptions>>();

        var results = validator.Validate(options);
        if (results.IsValid)
            return ValidateOptionsResult.Success;

        var name = options.GetType().Name;
        var errors = results.Errors.Select(r => $"Validation failed for {name}.{r.PropertyName}: {r.ErrorMessage}");
        return ValidateOptionsResult.Fail(errors);
    }
}