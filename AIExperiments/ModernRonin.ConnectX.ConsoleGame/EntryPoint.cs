using System;
using Fclp;
using ModernRonin.ConnectX.TreeSearchBot;

namespace ModernRonin.ConnectX.ConsoleGame
{
    static class EntryPoint
    {
        static void Main(string[] args)
        {
            var (configuration, opponentType) = ParseArguments(args);
            if (null == configuration) return;

            var opponent = GetOpponent(opponentType);
            var runner = new ConsoleRunner(new Game(configuration), new[] {new HumanConsolePlayer(), opponent});
            runner.Run();
            Console.WriteLine("Press <Enter> to exit...");
            Console.ReadLine();
        }
        static IPlayer GetOpponent(OpponentType opponentType)
        {
            switch (opponentType)
            {
                case OpponentType.Human: return new HumanConsolePlayer();
                case OpponentType.TreeSearch: return new TreeSearchBotPlayer(6);
            }

            return null;
        }
        static (GameConfiguration, OpponentType) ParseArguments(string[] args)
        {
            var resultConfig = new GameConfiguration();
            var resultOpponent = OpponentType.Human;

            var parser = new FluentCommandLineParser();
            parser.Setup<int>('w', "width").SetDefault(GameConfiguration.Default.BoardWidth)
                  .Callback(w => resultConfig.BoardWidth = w);
            parser.Setup<int>('h', "height").SetDefault(GameConfiguration.Default.BoardHeight)
                  .Callback(h => resultConfig.BoardHeight = h);
            parser.Setup<OpponentType>('o', "opponent").Required().Callback(o => resultOpponent = o);

            var parsed = parser.Parse(args);
            if (parsed.HasErrors)
            {
                parser.HelpOption.ShowHelp(parser.Options);
                return (null, resultOpponent);
            }

            resultConfig.InitialStonesPerPlayer = GameConfiguration.Default.InitialStonesPerPlayer;
            return (resultConfig, resultOpponent);
        }

        enum OpponentType
        {
            Human,
            TreeSearch
        }
    }
}