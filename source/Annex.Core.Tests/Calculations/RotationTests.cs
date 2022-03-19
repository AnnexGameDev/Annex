using Annex.Core.Calculations;
using FluentAssertions;
using Xunit;

namespace Annex.Core.Tests.Calculations
{
    public class RotationTests
    {
        [Theory]
        [InlineData(0, 0, 0, 0, 0)]
        [InlineData(0, 0, 1, 1, 45)]
        [InlineData(0, 0, 0, 1, 90)]
        [InlineData(0, 0, -1, 1, 135)]
        [InlineData(0, 0, -1, 0, 180)]
        [InlineData(0, 0, -1, -1, -135)]
        [InlineData(0, 0, 0, -1, -90)]
        [InlineData(0, 0, 1, -1, -45)]
        public void Given_When_Then(float x1, float y1, float x2, float y2, float theExpectedResult) {
            // Arrange
            // Act
            var theActualDegrees = Rotation.ComputeRotation(x1, y1, x2, y2);

            // Assert
            theActualDegrees.Should().Be(theExpectedResult);
        }
    }
}
