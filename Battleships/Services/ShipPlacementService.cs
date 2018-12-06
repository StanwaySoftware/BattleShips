using Battleships.Interfaces;
using Battleships.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Battleships.Services
{
    public class ShipPlacementService : IShipPlacementService
    {
        private static readonly Random _random = new Random();

        public void PlaceShips(IEnumerable<Ship> ships, GameBoard gameBoard)
        {
            var placementTypes = new[] {
                    new Placement
                    {
                        PointConversionFunc = (index, block) => new Point(index, block),
                        BlockCount = gameBoard.Size.Width,
                        BlockSize = gameBoard.Size.Height,
                    },
                    new Placement
                    {
                        PointConversionFunc = (index, block) => new Point(block, index),
                        BlockCount = gameBoard.Size.Height,
                        BlockSize = gameBoard.Size.Width,
                    },
                };

            foreach (var ship in ships)
            {
                var orderedPlacementTypes = placementTypes.OrderBy(x => Guid.NewGuid()).ToList();

                var shipPlaced = false;
                foreach (var placementType in orderedPlacementTypes)
                {
                    (bool spaceFound, int blockIndex, int startIndex) = FindSpace((index, block) => gameBoard.Squares[placementType.PointConversionFunc(index, block)].Ship != null, placementType.BlockCount, placementType.BlockSize, ship.Size);

                    if (!spaceFound)
                    {
                        continue;
                    }

                    for (var i = 0; i < ship.Size; i++)
                    {
                        var point = placementType.PointConversionFunc(startIndex + i, blockIndex);
                        gameBoard.Squares[point].Ship = ship;
                    }

                    shipPlaced = true;
                    break;
                }

                if (!shipPlaced)
                {
                    throw new Exception("Error placing ship");
                }
            }
        }

        private (bool spaceFound, int blockIndex, int startIndex) FindSpace(Func<int, int, bool> hasShipFunc, int blockCount, int blockSize, int shipSize)
        {
            var startBlock = _random.Next(1, blockCount);

            for (var i = 0; i < blockCount; i++)
            {
                //Uses Modulus to loop round available blocks;
                var blockIndex = (((startBlock - 1) + i) % blockCount) + 1;

                var emptySpaces = Enumerable.Range(1, blockSize).Where(x => !hasShipFunc(x, blockIndex)).ToList();
                var emptyGroup = GetEmptyRange(emptySpaces, shipSize);

                if (emptyGroup == null)
                {
                    continue;
                }

                var startIndex = _random.Next(emptyGroup.Start, emptyGroup.End - shipSize + 1);

                return (spaceFound: true, blockIndex, startIndex);
            }

            return (spaceFound: false, blockIndex: 0, startIndex: 0);
        }

        private EmptyRange GetEmptyRange(List<int> emptySpaces, int shipSize)
        {
            if (!emptySpaces.Any())
            {
                return null;
            }

            //Identifys range of spaces big enough for the ship and randomly selects one
            return emptySpaces.Select((val, ind) => new { val, group = val - ind })
                                            .GroupBy(v => v.group, v => v.val)
                                            .Where(x => x.Count() >= shipSize)
                                            .Select(x => new EmptyRange
                                            {
                                                Start = x.Min(),
                                                End = x.Max()
                                            })
                                            .OrderBy(x => Guid.NewGuid())
                                            .FirstOrDefault();
        }

        private class EmptyRange
        {
            public int Start { get; set; }
            public int End { get; set; }
        }

        private class Placement
        {
            public Func<int, int, Point> PointConversionFunc { get; set; }
            public int BlockCount { get; set; }
            public int BlockSize { get; set; }
        }
    }
}
