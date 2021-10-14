using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Reflection;

namespace AssemblyLoader
{
    public class Loader
    {
        private Logger logger;
        private CommandSeperator commandSeperator;

        private bool exit;

        public Loader(Logger logger, CommandSeperator commandSeperator)
        {
            this.logger = logger;
            this.commandSeperator = commandSeperator;
            this.exit = false;
        }

        public void Start()
        {
            while (!this.exit)
            {
                this.WaitForUserInput();
            }
        }

        private void WaitForUserInput()
        {
            var spacer = "> ";

            Console.Write(spacer);
            var userInput = Console.ReadLine();

            if (this.logger.logFile)
                this.logger.LogFile(spacer + userInput);

            var command = this.commandSeperator.Seperate(userInput);

            this.ExecuteComand(command);
        }

        /// <summary>
        /// Executes the comand
        /// </summary>
        /// <param name="command"></param>
        private void ExecuteComand(Command command)
        {
            switch (command.ExecutionCommand)
            {
                case "show":
                    LoadAssembly(command.Args);
                    break;

                case "consoleoff":
                    this.logger.logConsole = false;
                    break;

                case "consoleon":
                    this.logger.logConsole = true;
                    break;

                case "fileon":
                    this.logger.DeleteOutputFile();
                    this.logger.logFile = true;
                    break;

                case "fileoff":
                    this.logger.logFile = false;
                    break;

                case "exit":
                    this.exit = true;
                    break;
            }
        }

        private void Log(ConsoleColor color, string message)
        {
            this.logger.Log(color, message);
        }

        /// <summary>
        /// Loads the assembly from the file
        /// </summary>
        /// <param name="path"></param>
        private void LoadAssembly(string path)
        {
            if (path == null)
            {
                this.Log(ConsoleColor.Red, "You have to enter a path");
                return;
            }

            if (!File.Exists(Path.GetFullPath(path)))
            {
                this.Log(ConsoleColor.Red, "The entered path does not exist");
                return;
            }

            try
            {
                var assembly = Assembly.LoadFrom(Path.GetFullPath(path));
                var types = assembly.GetTypes();

                var alreadyShownList = new List<ShowType>();
                foreach (Type type in types)
                {
                    alreadyShownList.Add(new ShowType(type));
                }

                this.ShowAssembly(alreadyShownList);
            }
            catch
            {
                this.Log(ConsoleColor.Red, "Could not load assembly");
            }
        }

        private void ShowAssembly(List<ShowType> types)
        {
            foreach (var type in types)
            {
                if (!type.AlreadyShown)
                    this.ShowClass(type, types, string.Empty);
            }
        }

        private void ShowClass(ShowType type, List<ShowType> types, string spacer)
        {
            type.AlreadyShown = true;

            spacer += "   ";
            ConsoleColor color;

            if (type.Type.IsInterface)
                color = ConsoleColor.Cyan;
            else
                color = ConsoleColor.Blue;

            this.Log(color, spacer + type.Type.Name.ToString());

            this.ShowMembers(type.Type, spacer);

            foreach (ShowType nestedType in types)
            {
                if (!nestedType.AlreadyShown)
                    if (nestedType.Type.DeclaringType == type.Type)
                    {
                        nestedType.AlreadyShown = true;
                        ShowClass(nestedType, types, spacer);
                    }
            }

            
        }

        private void ShowMembers(Type type, string spacer)
        {
            spacer += "   ";

            foreach(var member in type.GetMembers())
            {
                if (member.MemberType == MemberTypes.Method)
                    this.Log(ConsoleColor.Green, spacer + member.ToString());

                if (member.MemberType == MemberTypes.Field)
                    this.Log(ConsoleColor.Yellow, spacer + member.ToString());

                if (member.MemberType == MemberTypes.Property)
                    this.Log(ConsoleColor.Red, spacer + member.ToString());
            }
        }
    }
}
