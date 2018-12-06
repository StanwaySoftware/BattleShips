using Battleships.Models;
using System.Collections.Generic;
using System.Drawing;

namespace Battleships.Interfaces
{
    public interface IGameEngine
    {
        void Play(Size boardSize, IEnumerable<ShipRequest> shipRequests);
    }
}
