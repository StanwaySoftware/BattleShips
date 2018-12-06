using Battleships.Models;
using Battleships.Services;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace Battleships.Tests
{
    public class ShipBuilderTests
    {

        [Fact]
        public void GivenShipRequestsReturnShips()
        {
            var builder = new ShipBuilder();

            var ships = builder.Build(new[] {
                new ShipRequest
                {
                    Type = "Battleship",
                    Size = 5,
                    Quantity = 1
                },
                new ShipRequest
                {
                    Type = "Destroyer",
                    Size = 4,
                    Quantity = 2
                },
            });

            ships.Where(x => x.Type == "Battleship").Should().HaveCount(1);
            ships.Where(x => x.Type == "Destroyer").Should().HaveCount(2);
        }
    }
}
