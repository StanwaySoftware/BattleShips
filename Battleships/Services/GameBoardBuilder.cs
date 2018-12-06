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
            for (var i = 1; i <= boardSize.Height; i++)
            {
                for (var j = 1; j <= boardSize.Width; j++)
                {
                    squares.Add(new Point(i, j), new BoardSquare());
                }
            }

            return new GameBoard
            {
                Size = boardSize,
                Squares = squares,
            };
        }
    }
}
