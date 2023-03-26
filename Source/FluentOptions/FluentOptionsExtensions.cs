using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MistyDotDev.FluentOptions;

/// <summary>
/// Extension methods for registering FluentOptions validation.
/// </summary>
public static class FluentOptionsExtensions
{
    /// <summary>
    /// Register an options instance for validation via FluentValidation.
    /// </summary>
    /// <typeparam name="TOptions">The options type to be configured.</typeparam>
    /// <param name="builder">The options builder to add the services to.</param>
    /// <returns>The <see cref="OptionsBuilder{TOptions}"/> so that additional calls can be chained.</returns>
    public static OptionsBuilder<TOptions> ValidateFluently<TOptions>(this OptionsBuilder<TOptions> builder)
        where TOptions : class
    {
        builder.Services.AddSingleton<IValidateOptions<TOptions>, FluentOptionsValidator<TOptions>>();
        return builder;
    }
}