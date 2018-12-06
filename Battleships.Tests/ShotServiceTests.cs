using Battleships.Interfaces;
using Battleships.Models;
using Battleships.Services;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Drawing;
using Xunit;

namespace Battleships.Tests
{
    public class ShotServiceTests
    {
        [Fact]
        public void GivenInvalidLocationReturnInvalidMessage()
        {
            var mockLocationConverter = new Mock<ILocationConverter>();
            mockLocationConverter.Setup(x => x.TryGetPoint(It.IsAny<string>(), It.IsAny<Size>())).Returns((false, default(Point)));

            var service = new ShotService(mockLocationConverter.Object);

            var response = service.ShootBoardSquare("", new GameBoard());

            response.Should().Be("Invalid location, please try again");
        }

        [Fact]
        public void GivenLocationAlreadyShotReturnShotMessage()
        {
            var mockLocationConverter = new Mock<ILocationConverter>();
            var shotPoint = new Point(1, 1);
            mockLocationConverter.Setup(x => x.TryGetPoint(It.IsAny<string>(), It.IsAny<Size>())).Returns((true, shotPoint));

            var service = new ShotService(mockLocationConverter.Object);

            var response = service.ShootBoardSquare("", new GameBoard()
            {
                Squares = new Dictionary<Point, BoardSquare>
                {
                    { shotPoint, new BoardSquare() { Shot = true } }
                }
            });

            response.Should().Be("You have already shot this square.");
        }

        [Fact]
        public void GivenLocationMissReturnMissMessage()
        {
            var mockLocationConverter = new Mock<ILocationConverter>();
            var shotPoint = new Point(1, 1);
            mockLocationConverter.Setup(x => x.TryGetPoint(It.IsAny<string>(), It.IsAny<Size>())).Returns((true, shotPoint));

            var service = new ShotService(mockLocationConverter.Object);

            var response = service.ShootBoardSquare("", new GameBoard()
            {
                Squares = new Dictionary<Point, BoardSquare>
                {
                    { shotPoint, new BoardSquare() }
                }
            });

            response.Should().Be("Miss.");
        }

        [Fact]
        public void GivenLocationHitReturnHitMessage()
        {
            var mockLocationConverter = new Mock<ILocationConverter>();
            var shotPoint = new Point(1, 1);
            mockLocationConverter.Setup(x => x.TryGetPoint(It.IsAny<string>(), It.IsAny<Size>())).Returns((true, shotPoint));

            var service = new ShotService(mockLocationConverter.Object);

            var ship = new Ship() {
                Size = 2,
                PointsHit = new HashSet<Point>(),
            };

            var response = service.ShootBoardSquare("", new GameBoard()
            {
                Squares = new Dictionary<Point, BoardSquare>
                {
                    { shotPoint, new BoardSquare() { Ship = ship } }
                }
            });

            response.Should().Be("Hit.");
        }

        [Fact]
        public void GivenLocationHitAllReturnSunkMessage()
        {
            var mockLocationConverter = new Mock<ILocationConverter>();
            var shotPoint = new Point(1, 1);
            mockLocationConverter.Setup(x => x.TryGetPoint(It.IsAny<string>(), It.IsAny<Size>())).Returns((true, shotPoint));

            var service = new ShotService(mockLocationConverter.Object);

            var ship = new Ship()
            {
                Size = 1,
                PointsHit = new HashSet<Point>(),
            };

            var response = service.ShootBoardSquare("", new GameBoard()
            {
                Squares = new Dictionary<Point, BoardSquare>
                {
                    { shotPoint, new BoardSquare() { Ship = ship } }
                }
            });

            response.Should().ContainEquivalentOf("Sunk");
        }
    }
}
