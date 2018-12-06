using Battleships.Interfaces;
using System.Drawing;

namespace Battleships.Services
{
    public class LocationConverter : ILocationConverter
    {
        public (bool isValid, Point point) TryGetPoint(string location, Size boardSize)
        {
            //Assumes Max board size of 26 x 99
            if (string.IsNullOrWhiteSpace(location) || location.Length < 2 || location.Length > 3)
            {
                return (isValid: false, point: default(Point));
            }

            location = location.ToLower();

            var x = location[0] - 96;
            if (x <= 0 || x > boardSize.Height)
            {
                return (isValid: false, point: default(Point));
            }

            if (!int.TryParse(location.Substring(1, location.Length - 1), out int y) || y <= 0 || y > boardSize.Width)
            {
                return (isValid: false, point: default(Point));
            }

            return (isValid: true, point: new Point(x, y));
        }
    }
}
