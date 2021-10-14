using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AssemblyLoader
{
    public class Logger
    {
        private string filePath;
        public bool logConsole;
        public bool logFile;

        public Logger(string filePath)
        {
            this.filePath = filePath;
            this.logConsole = false;
            this.logFile = false;
        }

        /// <summary>
        /// resets the output file
        /// </summary>
        public void DeleteOutputFile()
        {
            File.Delete(Path.GetFullPath(filePath));

            var lines = new List<string>();
            lines.Add("___LOG___");

            try
            {
                File.AppendAllLines(Path.GetFullPath(filePath), lines);
            }
            catch
            {
                this.LogConsole(ConsoleColor.Red, "Could not write into Output.txt!");
            }
        }

        public void Log(ConsoleColor color, string message)
        {
            if (this.logConsole)
                this.LogConsole(color, message);

            if (this.logFile)
                this.LogFile(message);
        }

        /// <summary>
        /// logs messages into console
        /// </summary>
        /// <param name="color"></param>
        /// <param name="message"></param>
        public void LogConsole(ConsoleColor color, string message)
        {
            var currentColor = Console.ForegroundColor;

            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = currentColor;
        }

        /// <summary>
        /// logs message into file
        /// </summary>
        /// <param name="message"></param>
        public void LogFile(string message)
        {
            var lines = new List<string>();
            lines.Add(message);

            try
            {
                File.AppendAllLines(Path.GetFullPath(filePath), lines);
            }
            catch
            {
                this.LogConsole(ConsoleColor.Red, "Could not write into Output.txt!");
            }
        }
    }
}
