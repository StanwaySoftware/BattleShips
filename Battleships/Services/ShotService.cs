using Battleships.Interfaces;
using Battleships.Models;
using System.Drawing;

namespace Battleships.Services
{
    public class ShotService : IShotService
    {
        private readonly ILocationConverter _locationConverter;

        public ShotService(ILocationConverter locationConverter)
        {
            _locationConverter = locationConverter;
        }

        public string ShootBoardSquare(string location, GameBoard gameBoard)
        {
            (bool isValid, Point point) = _locationConverter.TryGetPoint(location, gameBoard.Size);

            if (!isValid || !gameBoard.Squares.TryGetValue(point, out BoardSquare boardSquare))
            {
                return "Invalid location, please try again";
            }

            if (boardSquare.Shot)
            {
                return "You have already shot this square.";
            }

            boardSquare.Shot = true;

            if (boardSquare.Ship == null)
            {
                return "Miss.";
            }

            boardSquare.Ship.PointsHit.Add(point);
            if (!boardSquare.Ship.Afloat)
            {
                return $"You sunk a {boardSquare.Ship.Type}.";
            }

            return "Hit.";
        }
    }
}
