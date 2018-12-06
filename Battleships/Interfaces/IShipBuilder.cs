using System.Collections.Generic;
using Battleships.Models;

namespace Battleships.Interfaces
{
    public interface IShipBuilder
    {
        IEnumerable<Ship> Build(IEnumerable<ShipRequest> shipRequests);
    }
}