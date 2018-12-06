using Battleships.Interfaces;
using System;

namespace Battleships.Services
{
    public class GameRenderer : IGameRenderer
    {
        public void RenderMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
