using Battleships.Models;

namespace Battleships.Interfaces
{
    public interface IShotService
    {
        string Shoot(string location, GameBoard gameBoard);
    }
}