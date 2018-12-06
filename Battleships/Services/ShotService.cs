using Battleships.Interfaces;
using Battleships.Models;
using System.Drawing;

namespace Battleships.Services
{
    public class ShotService : IShotService
    {
        private readonly ILocationConverter _locationConverter;

        public static readonly string InvalidLocation = "Invalid location, please try again";
        public static readonly string AlreadyShot = "You have already shot this square.";
        public static readonly string Miss = "Miss.";
        public static readonly string Hit = "Hit.";
        public static readonly string Sunk = "You Sunk";

        public ShotService(ILocationConverter locationConverter)
        {
            _locationConverter = locationConverter;
        }

        public string Shoot(string location, GameBoard gameBoard)
        {
            (bool isValid, Point point) = _locationConverter.TryGetPoint(location, gameBoard.Size);

            if (!isValid || !gameBoard.Squares.TryGetValue(point, out BoardSquare boardSquare))
            {
                return InvalidLocation;
            }

            if (boardSquare.Shot)
            {
                return AlreadyShot;
            }

            boardSquare.Shot = true;

            if (boardSquare.Ship == null)
            {
                return Miss;
            }

            boardSquare.Ship.PointsHit.Add(point);
            if (!boardSquare.Ship.Afloat)
            {
                return $"{Sunk} {boardSquare.Ship.Type}";
            }

            return Hit;
        }
    }
}
