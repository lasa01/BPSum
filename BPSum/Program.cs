using BPSum.Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BPSum
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Program Files (x86)\Steam\steamapps\common\SpaceEngineers";
            string modsPath = @"C:\Program Files (x86)\Steam\steamapps\workshop\content\244850";
            string worldPath = null;
            List<string> mods = new List<string>();
            List<string> folders = new List<string>();
            List<string> files = new List<string>();
            string blueprint = null;
            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];
                switch (arg)
                {
                    case "-":
                    case "--help":
                        Console.WriteLine(Help);
                        return;
                    case "--path":
                    case "-p":
                        if (i == args.Length - 1)
                        {
                            Console.WriteLine("You have to specify a path");
                            Environment.Exit(1);
                        }
                        path = args[i + 1];
                        i++;
                        break;
                    case "--modsPath":
                    case "-M":
                        if (i == args.Length - 1)
                        {
                            Console.WriteLine("You have to specify a mods path");
                            Environment.Exit(1);
                        }
                        modsPath = args[i + 1];
                        i++;
                        break;
                    case "--mod":
                    case "-m":
                        if (i == args.Length - 1)
                        {
                            Console.WriteLine("You have to specify a mod path");
                            Environment.Exit(1);
                        }
                        mods.Add(args[i + 1]);
                        i++;
                        break;
                    case "--world":
                    case "-w":
                        if (i == args.Length - 1)
                        {
                            Console.WriteLine("You have to specify a world path");
                            Environment.Exit(1);
                        }
                        worldPath = args[i + 1];
                        i++;
                        break;
                    case "--dataFolder":
                    case "-F":
                        if (i == args.Length - 1)
                        {
                            Console.WriteLine("You have to specify a data folder path");
                            Environment.Exit(1);
                        }
                        folders.Add(args[i + 1]);
                        i++;
                        break;
                    case "--dataFile":
                    case "-f":
                        if (i == args.Length - 1)
                        {
                            Console.WriteLine("You have to specify a data file path");
                            Environment.Exit(1);
                        }
                        files.Add(args[i + 1]);
                        i++;
                        break;
                    default:
                        if (arg.StartsWith("-"))
                        {
                            Console.WriteLine($"Invalid option {arg}");
                            Environment.Exit(1);
                        }
                        else if (blueprint == null)
                        {
                            blueprint = arg;
                        }
                        else
                        {
                            Console.WriteLine("You may specify only one blueprint file");
                            Environment.Exit(1);
                        }
                        break;
                }
            }
            if (blueprint == null)
            {
                Console.WriteLine(Help);
                return;
            }
            else
            {
                Console.WriteLine("Loading data files...");
                SummaryCalculator calc = new SummaryCalculator(path, modsPath);
                if (worldPath != null)
                {
                    calc.LoadWorld(worldPath);
                }
                foreach (string mod in mods)
                {
                    calc.LoadMod(mod);
                }
                foreach (string folder in folders)
                {
                    calc.LoadDir(folder);
                }
                foreach (string file in files)
                {
                    calc.LoadFile(file);
                }
                calc.InitializeDefinitions();
                calc.HandleUnknownBlocks = blocks =>
                {
                    Console.WriteLine($"Warning: the following block types were not found: {String.Join(", ", blocks)}");
                };
                Console.WriteLine("Calculating...");
                var result = calc.Calculate(blueprint);
                Console.WriteLine("Component summary:");
                foreach ((var component, var count) in result.OrderBy(item => item.Key.DisplayName))
                {
                    Console.WriteLine($"  {component.DisplayName}: {count}");
                }
            }
        }

        static string Help =>
@"
Space Engineers blueprint component summary calculator

Usage:
  dotnet BPSum.dll [options] <blueprint file>.sbc

Options:
  -h, --help                      Show this help
  -p, --path <game path>          Override the game install location
  -m, --mod <mod path>            Load a single mod from the specified path
  -w, --world <world file>        Load active mods from a world file (usually Sandbox.sbc)
  -M, --modsPath <mods path>      Override the mods location to use when loading mods from a world
  -F, --dataFolder <data path>    Load all .sbc data files from the specified path
  -f, --dataFile <data file>      Load a single .sbc data file
";
    }
}
