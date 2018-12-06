using System.Drawing;
using Battleships.Models;

namespace Battleships.Interfaces
{
    public interface IGameBoardBuilder
    {
        GameBoard Build(Size boardSize);
    }
}