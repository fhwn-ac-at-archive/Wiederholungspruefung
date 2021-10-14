using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyLoader
{
    public class CommandSeperator
    {

        private Logger logger;

        public CommandSeperator(Logger logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Seperates the user input
        /// </summary>
        /// <param name="enteredCommand"></param>
        /// <returns></returns>
        public Command Seperate(string enteredCommand)
        {
            var commands = enteredCommand.Split();

            if (!commands.Any())
            {
                return null;
            }

            string executeCommand = string.Empty;
            switch (commands[0].ToLower())
            {
                case "show":
                    executeCommand = "show";
                    break;

                case "consoleoff":
                    executeCommand = "consoleoff";
                    break;

                case "consoleon":
                    executeCommand = "consoleon";
                    break;

                case "fileon":
                    executeCommand = "fileon";
                    break;

                case "fileoff":
                    executeCommand = "fileoff";
                    break;

                case "exit":
                    executeCommand = "exit";
                    break;

                default:
                    this.logger.Log(ConsoleColor.Red, "This command does not exist");
                    break;
            }

            if (executeCommand == "show" && commands.Length > 1)
            {
                return new Command(executeCommand, commands[1]);
            }
            else
            {
                return new Command(enteredCommand);
            }
        }
    }
}
