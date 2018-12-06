using Battleships.Models;

namespace Battleships.Interfaces
{
    public interface IShotService
    {
        string ShootBoardSquare(string location, GameBoard gameBoard);
    }
}