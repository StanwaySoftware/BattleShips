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

            var response = service.Shoot("", new GameBoard());

            response.Should().BeEquivalentTo(ShotService.InvalidLocation);
        }

        [Fact]
        public void GivenLocationAlreadyShotReturnShotMessage()
        {
            var shotPoint = new Point(1, 1);
            var service = BuildShotService(shotPoint);

            var response = service.Shoot("", BuildGameBoard(shotPoint, new BoardSquare() { Shot = true }));

            response.Should().BeEquivalentTo(ShotService.AlreadyShot);
        }

        [Fact]
        public void GivenLocationMissReturnMissMessage()
        {
            var shotPoint = new Point(1, 1);
            var service = BuildShotService(shotPoint);

            var response = service.Shoot("", BuildGameBoard(shotPoint, new BoardSquare()));

            response.Should().BeEquivalentTo(ShotService.Miss);
        }

        [Fact]
        public void GivenLocationHitReturnHitMessage()
        {
            var shotPoint = new Point(1, 1);
            var service = BuildShotService(shotPoint);

            var boardSquare = new BoardSquare()
            {
                Ship = new Ship()
                {
                    Size = 2,
                    PointsHit = new HashSet<Point>(),
                }
            };

            var response = service.Shoot("", BuildGameBoard(shotPoint, boardSquare));
            response.Should().BeEquivalentTo(ShotService.Hit);
        }

        [Fact]
        public void GivenLocationHitAllReturnSunkMessage()
        {
            var shotPoint = new Point(1, 1);
            var service = BuildShotService(shotPoint);

            var boardSquare = new BoardSquare()
            {
                Ship = new Ship()
                {
                    Size = 1,
                    PointsHit = new HashSet<Point>(),
                }
            };

            var response = service.Shoot("", BuildGameBoard(shotPoint, boardSquare));

            response.Should().ContainEquivalentOf(ShotService.Sunk);
        }

        [Fact]
        public void GivenLocationHitCheckBoardUpdated()
        {
            var shotPoint = new Point(1, 1);
            var service = BuildShotService(shotPoint);

            var boardSquare = new BoardSquare() {
                Ship = new Ship()
                        {
                            Size = 1,
                            PointsHit = new HashSet<Point>(),
                        }
            };

            var response = service.Shoot("", BuildGameBoard(shotPoint, boardSquare));

            boardSquare.Shot.Should().BeTrue();
        }

        [Fact]
        public void GivenLocationHitCheckShipUpdated()
        {
            var shotPoint = new Point(1, 1);
            var service = BuildShotService(shotPoint);

            var ship = new Ship()
            {
                Size = 1,
                PointsHit = new HashSet<Point>(),
            };

            var boardSquare = new BoardSquare() { Ship = ship };

            var response = service.Shoot("", BuildGameBoard(shotPoint, boardSquare));

            ship.PointsHit.Should().HaveCount(1);
            ship.Afloat.Should().BeFalse();
        }

        private GameBoard BuildGameBoard(Point shotPoint, BoardSquare boardSquare)
        {
            return new GameBoard()
            {
                Squares = new Dictionary<Point, BoardSquare>
                {
                    { shotPoint, boardSquare}
                }
            };
        }

        private ShotService BuildShotService(Point shotPoint)
        {
            var mockLocationConverter = new Mock<ILocationConverter>();
            mockLocationConverter.Setup(x => x.TryGetPoint(It.IsAny<string>(), It.IsAny<Size>())).Returns((true, shotPoint));

            return new ShotService(mockLocationConverter.Object);
        }
    }
}
