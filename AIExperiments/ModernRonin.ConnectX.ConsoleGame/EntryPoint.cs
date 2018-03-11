using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fclp;

namespace ModernRonin.ConnectX.ConsoleGame
{
    static class EntryPoint
    {
        static void Main(string[] args)
        {
            var configuration = ParseArguments(args);
            if (null==configuration) return;

            Console.WriteLine(configuration);

            Console.WriteLine("Press <Enter> to exit...");
            Console.ReadLine();
        }
        static GameConfiguration ParseArguments(string[] args)
        {
            var parser = new FluentCommandLineParser<GameConfiguration>();

            parser.Setup(c => c.BoardWidth).As('w', "width").SetDefault(GameConfiguration.Default.BoardWidth);
            parser.Setup(c => c.BoardHeight).As('h', "height").SetDefault(GameConfiguration.Default.BoardHeight);

            var parsed = parser.Parse(args);
            if (parsed.HasErrors)
            {
                parser.HelpOption.ShowHelp(parser.Options);
                return null;
            }

            parser.Object.InitialStonesPerPlayer = GameConfiguration.Default.InitialStonesPerPlayer;
            return parser.Object;
        }
    }
}
