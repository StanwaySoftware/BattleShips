using Battleships.Interfaces;
using Battleships.Models;
using Battleships.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Drawing;

namespace Battleships
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var gameEngine = serviceProvider.GetService<IGameEngine>();

            //Ships types could be strongly typed
            gameEngine.Play(new Size(10, 10), new[] {
                new ShipRequest
                {
                    Type = "Battleship",
                    Size = 5,
                    Quantity = 1
                },
                new ShipRequest
                {
                    Type = "Destroyer",
                    Size = 4,
                    Quantity = 2
                },
            });
        }

        private static void ConfigureServices(ServiceCollection serviceCollection)
        {
            serviceCollection
                .AddLogging(configure => configure.AddConsole())
                .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Information)
                .AddTransient<IGameBoardBuilder, GameBoardBuilder>()
                .AddTransient<IGameEngine, GameEngine>()
                .AddTransient<IShotService, ShotService>()
                .AddTransient<IGameRenderer, GameRenderer>()
                .AddTransient<IGameActionReader, GameActionReader>()
                .AddTransient<ILocationConverter, LocationConverter>()
                .AddTransient<IShipBuilder, ShipBuilder>()
                .AddTransient<IShipPlacementService, ShipPlacementService>();
        }
    }
}
