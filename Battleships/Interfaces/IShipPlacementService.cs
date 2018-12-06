using Battleships.Models;
using System.Collections.Generic;

namespace Battleships.Interfaces
{
    public interface IShipPlacementService
    {
        void PlaceShips(IEnumerable<Ship> ships, GameBoard gameBoard);
    }
}
