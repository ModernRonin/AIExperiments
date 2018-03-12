using System;
using Fclp;

namespace ModernRonin.ConnectX.ConsoleGame
{
    static class EntryPoint
    {
        static void Main(string[] args)
        {
            var configuration = ParseArguments(args);
            if (null == configuration) return;

            var runner = new ConsoleRunner(new Game(configuration),
                new[] {new HumanConsolePlayer(), new HumanConsolePlayer()});
            runner.Run();
            Console.WriteLine("Press <Enter> to exit...");
            Console.ReadLine();
        }
        static GameConfiguration ParseArguments(string[] args)
        {
            var result= new GameConfiguration();
            var parser = new FluentCommandLineParser();
            parser.Setup<int>('w', "width").SetDefault(GameConfiguration.Default.BoardWidth)
                  .Callback(w => result.BoardWidth = w);
            parser.Setup<int>('h', "height").SetDefault(GameConfiguration.Default.BoardHeight)
                  .Callback(h => result.BoardHeight = h);
            
            var parsed = parser.Parse(args);
            if (parsed.HasErrors)
            {
                parser.HelpOption.ShowHelp(parser.Options);
                return null;
            }

            result.InitialStonesPerPlayer = GameConfiguration.Default.InitialStonesPerPlayer;
            return result;
        }
    }
}