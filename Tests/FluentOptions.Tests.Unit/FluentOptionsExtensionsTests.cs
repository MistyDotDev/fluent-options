using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace MistyDotDev.FluentOptions.Tests.Unit;

public sealed class FluentOptionsExtensionsTests
{
    [Fact]
    public void ValidateFluently_Should_AddFluentOptionsValidatorToTheServiceContainer()
    {
        // Arrange
        var services = new ServiceCollection();
        var builder = new OptionsBuilder<MockOptions>(services, null);

        // Act
        builder.ValidateFluently();

        // Assert
        services.Should().ContainSingle(x =>
            x.Lifetime == ServiceLifetime.Singleton &&
            x.ServiceType == typeof(IValidateOptions<MockOptions>) &&
            x.ImplementationType == typeof(FluentOptionsValidator<MockOptions>)
        );
    }
}