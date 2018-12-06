using Battleships.Interfaces;
using Battleships.Models;
using System.Collections.Generic;
using System.Drawing;

namespace Battleships.Services
{
    public class GameBoardBuilder : IGameBoardBuilder
    {
        public GameBoard Build(Size boardSize)
        {
            var squares = new Dictionary<Point, BoardSquare>();
            for (var x = 1; x <= boardSize.Height; x++)
            for (var y = 1; y <= boardSize.Width; y++)
            {
                squares.Add(new Point(x, y), new BoardSquare());
            }

            return new GameBoard
            {
                Size = boardSize,
                Squares = squares,
            };
        }
    }
}
