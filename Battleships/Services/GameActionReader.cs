using Battleships.Interfaces;
using System;

namespace Battleships.Services
{
    public class GameActionReader : IGameActionReader
    {
        public string GetAction(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }
    }
}
