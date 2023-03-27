using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MistyDotDev.FluentOptions;

/// <summary>
/// Extension methods for applying FluentValidation to an <see cref="OptionsBuilder{TOptions}"/>.
/// </summary>
public static class FluentOptionsExtensions
{
    /// <summary>
    /// Adds FluentValidation to the <see cref="OptionsBuilder{TOptions}"/>.
    /// </summary>
    /// <typeparam name="TOptions">The options type to configure.</typeparam>
    /// <param name="builder">The options builder to add FluentValidation to.</param>
    /// <returns>The <see cref="OptionsBuilder{TOptions}"/> so that additional calls can be chained.</returns>
    public static OptionsBuilder<TOptions> ValidateFluently<TOptions>(this OptionsBuilder<TOptions> builder) where TOptions : class
    {
        builder.Services.AddSingleton<IValidateOptions<TOptions>, FluentOptionsValidator<TOptions>>();
        return builder;
    }
}