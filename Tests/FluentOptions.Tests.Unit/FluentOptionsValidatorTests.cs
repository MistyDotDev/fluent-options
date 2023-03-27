using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit;

namespace MistyDotDev.FluentOptions.Tests.Unit;

public sealed class FluentOptionsValidatorTests
{
    private readonly MockOptions _mockOptions = new();
    private readonly IValidator<MockOptions> _validator = Substitute.For<IValidator<MockOptions>>();
    private readonly FluentOptionsValidator<MockOptions> _systemUnderTest;

    public FluentOptionsValidatorTests()
    {
        var services = new ServiceCollection().AddSingleton(_validator).BuildServiceProvider();
        var scopeFactory = services.GetRequiredService<IServiceScopeFactory>();
        _systemUnderTest = new FluentOptionsValidator<MockOptions>(scopeFactory);
    }

    [Fact]
    public void Validate_Should_ReturnSuccess_When_ValidationPasses()
    {
        // Arrange
        _validator.Validate(_mockOptions).Returns(new ValidationResult());

        // Act
        var result = _systemUnderTest.Validate("", _mockOptions);

        // Assert
        result.Succeeded.Should().BeTrue();
        result.Skipped.Should().BeFalse();
        result.Failed.Should().BeFalse();
    }

    [Fact]
    public void Validate_Should_ReturnFailed_When_ValidationFails()
    {
        // Arrange
        _validator.Validate(_mockOptions).Returns(
            new ValidationResult(new[] { new ValidationFailure("Message", "Test Message") })
        );

        // Act
        var result = _systemUnderTest.Validate("", _mockOptions);

        // Assert
        result.Succeeded.Should().BeFalse();
        result.Skipped.Should().BeFalse();
        result.Failed.Should().BeTrue();
        result.Failures.Should().HaveCount(1);
        result.Failures.Should().ContainSingle(f => f.Contains("MockOptions.Message") && f.Contains("Test Message"));
    }
}