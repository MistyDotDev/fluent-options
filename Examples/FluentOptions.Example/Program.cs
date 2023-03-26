using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MistyDotDev.FluentOptions;

await Host.CreateDefaultBuilder()
    .ConfigureServices(s =>
    {
        // FluentOptions will pull the validator from the service container.
        s.AddScoped<IValidator<ExampleOptions>, ExampleOptionsValidator>(); // Or use assembly scanning.
        s.AddOptions<ExampleOptions>()
            .BindConfiguration("Example")
            .ValidateFluently()
            .ValidateOnStart();
    })
    .RunConsoleAsync();

public sealed class ExampleOptions
{
    public required string Message { get; init; }
}

public sealed class ExampleOptionsValidator : AbstractValidator<ExampleOptions>
{
    public ExampleOptionsValidator()
    {
        RuleFor(s => s.Message)
            .NotEmpty();
    }
}