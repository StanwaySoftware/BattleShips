using Battleships.Models;
using Battleships.Services;
using FluentAssertions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Xunit;

namespace Battleships.Tests
{
    public class ShipPlacementServiceTest
    {
        [Theory]
        [ClassData(typeof(PlacementTestData))]
        public void GivenOneFreeSpacePlaceShip(Point[] emptySquares)
        {
            var shipPlacementService = new ShipPlacementService();
            var boardSize = new Size(4, 4);

            var gameBoard = new GameBoard()
            {
                Squares = BuildSquares(emptySquares, boardSize),
                Size = boardSize,
            };

            shipPlacementService.PlaceShips(new[] {
                    new Ship
                    {
                        Type = "TestShip",
                        Size = 2,
                    }
                }, gameBoard);

            var testShipPoints = gameBoard.Squares.Where(x => x.Value.Ship.Type == "TestShip").Select(x => x.Key).ToList();

            testShipPoints.Should().BeEquivalentTo(emptySquares);
        }

        private Dictionary<Point, BoardSquare> BuildSquares(Point[] emptySquares, Size boardSize)
        {
            var boardSquares = new Dictionary<Point, BoardSquare>();
            var dummyShip = new Ship();

            for (var i = 1; i <= boardSize.Width; i++)
            {
                for (var j = 1; j <= boardSize.Height; j++)
                {
                    var point = new Point(i, j);
                    var ship = emptySquares.Contains(point) ? null : dummyShip;
                    boardSquares.Add(point, new BoardSquare
                    {
                        Ship = ship
                    });
                }
            }

            return boardSquares;
        }

        public class PlacementTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new []
                    {
                        new Point(4,4),
                        new Point(4,3),
                    }
                };
                yield return new object[] { new []
                    {
                        new Point(1,1),
                        new Point(1,2),
                    }
                };
                yield return new object[] { new []
                    {
                        new Point(4,4),
                        new Point(3,4),
                    }
                };
                yield return new object[] { new []
                    {
                        new Point(1,1),
                        new Point(2,1),
                    }
                };
                yield return new object[] { new []
                    {
                        new Point(2,2),
                        new Point(2,3),
                    }
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}