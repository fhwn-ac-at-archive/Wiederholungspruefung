using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Wiederholung_Kiefer
{
    /// <summary>
    /// This class represents the application, which lets users read assemblies.
    /// </summary>
    public class AssemblyLoadApp
    {
        /// <summary>
        /// A keyboard Watcher, for monitoring user input.
        /// </summary>
        KeyboardWatcher watcher;

        /// <summary>
        /// A console printer, that can write into the console with different colors.
        /// </summary>
        ConsolePrinter printer;

        /// <summary>
        /// Capsules the user input.
        /// </summary>
        string inputField;

        /// <summary>
        /// a flag, which indicates, if output gets written into a file or not.
        /// </summary>
        bool outputFile;

        /// <summary>
        /// The constructor of AssemblyLoadApp.
        /// </summary>
        public AssemblyLoadApp()
        {
            this.watcher = new KeyboardWatcher();
            this.watcher.onKeyPressed += OnKeyPressed;

            outputFile = false;

            this.printer = new ConsolePrinter(true);
        }

        /// <summary>
        /// This method starts the application.
        /// </summary>
        public void Start()
        {
            this.inputField = string.Empty;
            watcher.Start();
            Console.Write("> ");
        }

        /// <summary>
        /// Event, for when a key is pressed. is used on the keyboard watcher.
        /// </summary>
        /// <param name="sender">sender of the event.</param>
        /// <param name="args">event arguments.</param>
        private void OnKeyPressed(object sender, OnKeyPressedEventArgs args)
        {
            if(args.KeyPressed.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                InitiateCommand(inputField);
                inputField = string.Empty;
                Console.Write("> ");
            }
            else if(args.KeyPressed.Key == ConsoleKey.Backspace)
            {
                if (inputField.Length > 0)
                {
                    inputField = inputField.Remove(inputField.Length - 1, 1);
                }
            }
            else
            {
                inputField += args.KeyPressed.KeyChar;
            }
        }

        /// <summary>
        /// This method initates a command written by the user. checks if the command exists or not.
        /// </summary>
        /// <param name="s">the command string</param>
        private void InitiateCommand(string s)
        {
            var parts = s.Split(' ');
            string command = parts[0];

            switch (command)
            {
                case "show":
                    if( parts.Length > 1)
                    {
                        bool loaded;
                        string parameter = parts[1];

                        Assembly a = AssemblyLoader.GetAssemblyFromPath(parameter, out loaded);

                        if (loaded)
                        {
                            this.ShowAssembly(a);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Could not load assembly");
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                    }
                    break;
                case "consoleon":
                    this.printer.Active = true;
                    printer.UnlockablePrint("Console output activated.", ConsoleColor.White);
                    break;
                case "consoleoff":
                    this.printer.Active = false;
                    printer.UnlockablePrint("Console output deactivated.", ConsoleColor.White);
                    break;
                case "fileon":
                    this.outputFile = true;
                    printer.UnlockablePrint("File output activated.", ConsoleColor.White);
                    break;
                case "fileoff":
                    this.outputFile = false;
                    printer.UnlockablePrint("File output deactivated.", ConsoleColor.White);
                    break;
                default:
                    printer.UnlockablePrint("Seems like you inserted an invalid command.", ConsoleColor.Red);
                    break;
            }
        }

        /// <summary>
        /// This method iterates through all types of an assembly and outputs the text on the console, or in a text file.
        /// </summary>
        /// <param name="a"></param>
        private void ShowAssembly(Assembly a)
        {
            try
            {
                string assemblytext = string.Empty;
                foreach (Type t in a.GetTypes())
                {
                    assemblytext += this.GetTypeInfoV2(t, 0);
                }

                if (outputFile)
                {
                    TextWriter.WriteToFile(assemblytext);
                }
            }
            catch (Exception)
            {
                printer.UnlockablePrint("There seems to be a problem when reading the assembly.", ConsoleColor.Red);
            }
        }

        /// <summary>
        /// This is the second version of GetTypeInfo, which is part of the "Befriedigend" requirements.
        /// </summary>
        /// <param name="t">the type, that will be read.</param>
        /// <param name="level">this holds information about the level in the tree.</param>
        /// <returns>a string containing the read type information</returns>
        private string GetTypeInfoV2(Type t, int level)
        {
            string output = string.Empty;

            ConsoleColor typeColor = ConsoleColor.Gray;
            if (t.IsClass)
            {
                typeColor = ConsoleColor.Blue;
            }
            else if (t.IsInterface)
            {
                typeColor = ConsoleColor.Cyan;
            }
            this.printer.LockablePrint(TextCreator.CreateTabAmount(level) + t.Name, typeColor);
            output += TextCreator.CreateTabAmount(level) + t.Name + "\n";

            IEnumerable<MemberInfo> abstractMembers = t.GetMembers(BindingFlags.NonPublic | BindingFlags.Instance).OrderByDescending(m => m.Name);
            IEnumerable<MemberInfo> staticMembers = t.GetMembers(BindingFlags.Static).OrderByDescending(m => m.Name);
            IEnumerable<MemberInfo> otherMembers = t.GetMembers().Where(m => !abstractMembers.Contains(m) && !staticMembers.Contains(m)).OrderByDescending(m => m.Name);

            foreach(MemberInfo m in abstractMembers)
            {
                printer.PrintMember(m, level + 1);
                output += TextCreator.CreateTabAmount(level + 1) + m.ToString() + "\n";
            }

            foreach(MemberInfo m in staticMembers)
            {
                printer.PrintMember(m, level + 1);
                output += TextCreator.CreateTabAmount(level + 1) + m.ToString() + "\n";
            }

            foreach(MemberInfo m in otherMembers)
            {
                printer.PrintMember(m, level + 1);
                output += TextCreator.CreateTabAmount(level + 1) + m.ToString() + "\n";
            }

            foreach(Type i in t.GetInterfaces())
            {
                output += GetTypeInfoV2(i, level + 1);
            }

            foreach(Type nt in t.GetNestedTypes())
            {
                output += GetTypeInfoV2(nt, level + 1);
            }
            return output;
        }

        /// <summary>
        /// This is the first version of GetTypeInfo, which is part of the "Genügend" requirements.
        /// </summary>
        /// <param name="t">the type that will be read</param>
        /// <param name="level">this holds information about the level in the tree.</param>
        /// <returns>a string containing the read type information.</returns>
        private string GetTypeInfo(Type t, int level)
        {
            string output = string.Empty;

            ConsoleColor typeColor = ConsoleColor.Gray;
            if (t.IsClass)
            {
                typeColor = ConsoleColor.Blue;
            }
            else if (t.IsInterface)
            {
                typeColor = ConsoleColor.Cyan;
            }
            this.printer.LockablePrint(TextCreator.CreateTabAmount(level) + t.Name, typeColor);
            output += TextCreator.CreateTabAmount(level) + t.Name + "\n";

            foreach (FieldInfo f in t.GetFields())
            {
                this.printer.LockablePrint(TextCreator.CreateTabAmount(level + 1) + f.ToString(), ConsoleColor.Yellow);
                output += TextCreator.CreateTabAmount(level + 1) + f.ToString() + "\n";
            }

            foreach (MethodInfo m in t.GetMethods())
            {
                this.printer.LockablePrint(TextCreator.CreateTabAmount(level + 1) + m.ToString(), ConsoleColor.Green);
                output += TextCreator.CreateTabAmount(level + 1) + m.ToString() + "\n";
            }

            foreach(PropertyInfo p in t.GetProperties())
            {
                this.printer.LockablePrint(TextCreator.CreateTabAmount(level + 1) + p.ToString(), ConsoleColor.Red);
                output += TextCreator.CreateTabAmount(level + 1) + p.ToString() + "\n";
            }

            foreach(Type i in t.GetInterfaces())
            {
                output += GetTypeInfo(i, level + 1);
            }
            foreach(Type nt in t.GetNestedTypes())
            {
                output += GetTypeInfo(nt, level + 1);
            }

            return output;
        }
    }
}