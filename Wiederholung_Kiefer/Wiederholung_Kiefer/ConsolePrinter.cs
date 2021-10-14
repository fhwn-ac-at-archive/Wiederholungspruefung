using System;
using System.Reflection;

namespace Wiederholung_Kiefer
{
    /// <summary>
    /// This class creates a console printer, that can write into the console.
    /// </summary>
    class ConsolePrinter
    {
        /// <summary>
        /// Certain output can be locked with this value.
        /// </summary>
        bool active;

        /// <summary>
        /// This creates an instance of the console printer.
        /// </summary>
        /// <param name="active">set console locking of or on.</param>
        public ConsolePrinter(bool active)
        {
            this.active = active;
        }

        /// <summary>
        /// Prints into the console. This function can be locked via the Active property.
        /// </summary>
        /// <param name="text">the text, that will be written.</param>
        /// <param name="color">the color of the text.</param>
        public void LockablePrint(string text, ConsoleColor color)
        {
            if (active)
            {
                ConsoleColor current = Console.ForegroundColor;
                Console.ForegroundColor = color;
                Console.WriteLine(text);
                Console.ForegroundColor = current;
            }
        }

        /// <summary>
        /// Prints into the console, but can not be locked via the Active property.
        /// </summary>
        /// <param name="text">the text, that will be written.</param>
        /// <param name="color">the color of the text.</param>
        public void UnlockablePrint(string text, ConsoleColor color)
        {
            ConsoleColor current = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = current;
        }

        /// <summary>
        /// Print for a memberinfo, that changes color according to the MemberType.
        /// </summary>
        /// <param name="m">the member, that will be printed.</param>
        public void PrintMember(MemberInfo m, int level)
        {
            if (m.MemberType == MemberTypes.Field)
            {
                LockablePrint(TextCreator.CreateTabAmount(level) + m.ToString(), ConsoleColor.Yellow);
            }
            else if (m.MemberType == MemberTypes.Method)
            {
                LockablePrint(TextCreator.CreateTabAmount(level) + m.ToString(), ConsoleColor.Green);
            }
            else if (m.MemberType == MemberTypes.Property)
            {
                LockablePrint(TextCreator.CreateTabAmount(level) + m.ToString(), ConsoleColor.Red);
            }
        }

        /// <summary>
        /// This property indicates, if the lockable print is disabled or not.
        /// </summary>
        public bool Active
        {
            get
            {
                return this.active;
            }
            set
            {
                this.active = value;
            }
        }
    }
}
