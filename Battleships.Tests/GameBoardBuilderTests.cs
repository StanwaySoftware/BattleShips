using Battleships.Services;
using FluentAssertions;
using System.Drawing;
using Xunit;

namespace Battleships.Tests
{
    public class GameBoardBuilderTests
    {
        [Fact]
        public void GivenSizeCreatesGameBoard()
        {
            var builder = new GameBoardBuilder();

            var board = builder.Build(new Size(2, 2));

            board.Squares.Should().ContainKeys(
                new Point(1, 1),
                new Point(1, 2),
                new Point(2, 1),
                new Point(2, 2)
            );
        }
    }
}
