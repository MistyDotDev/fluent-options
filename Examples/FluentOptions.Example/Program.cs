using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MistyDotDev.FluentOptions;

await Host.CreateDefaultBuilder()
    .ConfigureServices(s =>
    {
        // FluentOptions relies on the validator being registered in the service container. 
        // Please make sure it's registered, either via assembly scanning or manual registration.
        s.AddScoped<IValidator<ExampleOptions>, ExampleOptionsValidator>();

        // When options are added to the service collection, a builder is provided.
        // This is where we can configure binding and validation.
        s.AddOptions<ExampleOptions>()
            .BindConfiguration("Example") // Binds to the "Example" section in appsettings.json.
            .ValidateFluently() // Performs FluentValidation on the options instance.
            .ValidateOnStart(); // Automatically performs validation on startup.
    })
    .RunConsoleAsync();

/// <summary>
/// This is the options type which will be validated. 
/// </summary>
public sealed class ExampleOptions
{
    public required string Message { get; init; }
}

/// <summary>
/// This is the FluentValidation validator that will be queried.
/// </summary>
public sealed class ExampleOptionsValidator : AbstractValidator<ExampleOptions>
{
    public ExampleOptionsValidator()
    {
        // Here we just require that Message contains at least one letter.
        // However, we can do very deep validation here.
        RuleFor(s => s.Message)
            .NotEmpty();
    }
}