using System.Collections.Generic;
using System.Drawing;

namespace Battleships.Models
{
    public class GameBoard
    {
        public Size Size { get; set; }
        public Dictionary<Point, BoardSquare> Squares { get; set; }
    }
}
