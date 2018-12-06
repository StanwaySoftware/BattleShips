using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Battleships.Models
{
    public class Ship
    {
        public string Type { get; set; }
        public int Size { get; set; }
        public HashSet<Point> PointsHit { get; set; }
        public bool Afloat
        {
            get
            {
                return (Size - PointsHit.Count()) > 0;
            }
        }
    }
}
