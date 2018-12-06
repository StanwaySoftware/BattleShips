using Battleships.Interfaces;
using Battleships.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Battleships.Services
{
    public class GameEngine : IGameEngine
    {
        private readonly IGameBoardBuilder _gameBoardBuilder;
        private readonly IGameRenderer _gameRenderer;
        private readonly IGameActionReader _gameActionReader;
        private readonly IShipBuilder _shipBuilder;
        private readonly IShipPlacementService _shipPlacementService;
        private readonly IShotService _shotService;
        private readonly ILogger _logger;

        private GameBoard _gameBoard;
        private IEnumerable<Ship> _ships;

        public GameEngine(
            IGameBoardBuilder gameBoardBuilder,
            IGameRenderer gameRenderer,
            IGameActionReader gameActionReader,
            IShipBuilder shipBuilder,
            IShipPlacementService shipPlacementService,
            IShotService shotService,
            ILogger<GameEngine> logger
            )
        {
            _gameBoardBuilder = gameBoardBuilder;
            _gameRenderer = gameRenderer;
            _gameActionReader = gameActionReader;
            _shipBuilder = shipBuilder;
            _shipPlacementService = shipPlacementService;
            _shotService = shotService;
            _logger = logger;
        }

        public void Play(Size boardSize, IEnumerable<ShipRequest> shipRequests)
        {
            try
            {
                Initialise(boardSize, shipRequests);
                _gameRenderer.RenderMessage("Game started.");
                GameLoop();
                _gameRenderer.RenderMessage("You win.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error playing game");
            }

            _gameActionReader.GetAction("Game Over. Press Enter to close.");
        }

        private void GameLoop()
        {
            while (_ships.Where(x => x.Afloat).Count() > 0)
            {
                _logger.LogDebug(LogBoard());

                //Could add action to quit
                var location = _gameActionReader.GetAction("Enter a location:");
                var response = _shotService.ShootBoardSquare(location, _gameBoard);

                _gameRenderer.RenderMessage(response);
            }
        }

        private void Initialise(Size boardSize, IEnumerable<ShipRequest> shipRequests)
        {
            _logger.LogDebug("Build board");
            _gameBoard = _gameBoardBuilder.Build(boardSize);

            _logger.LogDebug("Build ships");
            _ships = _shipBuilder.Build(shipRequests);

            _logger.LogDebug("Place ships");
            _shipPlacementService.PlaceShips(_ships, _gameBoard);
        }

        private string LogBoard()
        {
            var boardRender = new StringBuilder();
            for (var i = 1; i <= _gameBoard.Size.Height; i++)
            {
                for (var j = 1; j <= _gameBoard.Size.Width; j++)
                {
                    var ship = _gameBoard.Squares[new Point(i, j)].Ship;
                    boardRender.Append(ship == null ? "_" : ship.Type[0].ToString());
                }
                boardRender.AppendLine();
            }

            return boardRender.ToString();
        }
    }
}
