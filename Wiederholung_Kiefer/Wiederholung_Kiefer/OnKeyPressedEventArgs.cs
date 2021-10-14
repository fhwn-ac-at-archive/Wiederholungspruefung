using System;

namespace Wiederholung_Kiefer
{
    /// <summary>
    /// This class contains event args for when a key is pressed on the keyboard.
    /// </summary>
    public class OnKeyPressedEventArgs
    {
        /// <summary>
        /// The pressed key.
        /// </summary>
        ConsoleKeyInfo keyPressed;

        /// <summary>
        /// The constructor of the key pressed event args.
        /// </summary>
        /// <param name="cki"></param>
        public OnKeyPressedEventArgs(ConsoleKeyInfo cki)
        {
            this.keyPressed = cki;
        }

        /// <summary>
        /// The property of the keyPressed field.
        /// </summary>
        public ConsoleKeyInfo KeyPressed
        {
            get
            {
                return this.keyPressed;
            }
        }
    }
}