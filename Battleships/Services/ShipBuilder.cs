using Battleships.Interfaces;
using Battleships.Models;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Battleships.Services
{
    public class ShipBuilder : IShipBuilder
    {
        public IEnumerable<Ship> Build(IEnumerable<ShipRequest> shipRequests)
        {
            return shipRequests.SelectMany(x => Enumerable.Range(1, x.Quantity).Select(y => new Ship
            {
                Type = x.Type,
                Size = x.Size,
                PointsHit = new HashSet<Point>(),
            })).ToList();
        }
    }
}
