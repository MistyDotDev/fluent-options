using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MistyDotDev.FluentOptions;
using Xunit;

namespace FluentOptions.Tests.Unit;

public class FluentOptionsExtensionsTests
{
    [Fact]
    public void ValidateFluently_Should_AddFluentOptionsValidatorToTheServiceCollection()
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