using System.Drawing;

namespace Battleships.Interfaces
{
    public interface ILocationConverter
    {
        (bool isValid, Point point) TryGetPoint(string location, Size boardSize);
    }
}