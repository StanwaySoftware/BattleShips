using Battleships.Services;
using FluentAssertions;
using System.Drawing;
using Xunit;

namespace Battleships.Tests
{
    public class LocationConverterTests
    {
        [Theory]
        [InlineData("a1", 1, 1)]
        [InlineData("a10", 1, 10)]
        [InlineData("j2", 10, 2)]
        [InlineData("b3", 2, 3)]
        public void GivenValidLocationReturnPoint(string location, int x, int y)
        {
            var converter = new LocationConverter();

            (bool isValid, Point point) = converter.TryGetPoint(location, new Size(10,10));

            isValid.Should().BeTrue();

            point.Should().Be(new Point(x, y));
        }

        [Theory]
        [InlineData("a11")]
        [InlineData("a-1")]
        [InlineData("k1")]
        [InlineData("")]
        [InlineData(null)]
        public void GivenInvalidLocationReturnsNotValid(string location)
        {
            var converter = new LocationConverter();

            (bool isValid, Point point) = converter.TryGetPoint(location, new Size(10, 10));

            isValid.Should().BeFalse();
        }
    }
}
