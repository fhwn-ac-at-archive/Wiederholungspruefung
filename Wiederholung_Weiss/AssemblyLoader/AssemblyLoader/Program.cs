using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace AssemblyLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;

            var logger = new Logger("output.txt");
            var commandsemerator = new CommandSeperator(logger);

            var loader = new Loader(logger, commandsemerator);
            loader.Start();
        }
    }
}
