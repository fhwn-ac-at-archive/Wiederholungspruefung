using System;
using System.Threading;

namespace Wiederholung_Kiefer
{
    /// <summary>
    /// This class creates a KeyboardWatcher, that monitors keyboard input. It is not necessary for the task, but i was bored :).
    /// </summary>
    internal class KeyboardWatcher
    {
        /// <summary>
        /// The thread of the KeyboardWatcher
        /// </summary>
        Thread watcherThread;

        /// <summary>
        /// The thread arguments of the keyboard watcher.
        /// </summary>
        KeyboardWatcherThreadArgs keyboardWatcherThreadArgs;

        /// <summary>
        /// The constructor of the keyboard watcher.
        /// </summary>
        public KeyboardWatcher()
        {
            this.watcherThread = new Thread(Work);
        }

        /// <summary>
        /// This event is fired, when a key is pressed on the keyboard.
        /// </summary>
        public event EventHandler<OnKeyPressedEventArgs> onKeyPressed;

        /// <summary>
        /// Starts the keyboard watcher.
        /// </summary>
        public void Start()
        {
            keyboardWatcherThreadArgs = new KeyboardWatcherThreadArgs();
            keyboardWatcherThreadArgs.Stop = false;

            watcherThread.Start(keyboardWatcherThreadArgs);
        }

        /// <summary>
        /// Stops the keyboard watcher.
        /// </summary>
        public void Stop()
        {
            if (!keyboardWatcherThreadArgs.Stop)
            {
                this.keyboardWatcherThreadArgs.Stop = true;
            }
        }

        /// <summary>
        /// The worker thred method, that reads keyboard inputs and fires an event when pressed.
        /// </summary>
        /// <param name="o"></param>
        private void Work(object o)
        {
            if (!(o is KeyboardWatcherThreadArgs))
            {
                throw new InvalidOperationException("Work needs an KeyboardWatcherEventArgs°!");
            }
            KeyboardWatcherThreadArgs args = (KeyboardWatcherThreadArgs)o;

            while (!args.Stop)
            {
                ConsoleKeyInfo cki = Console.ReadKey();

                this.FireKeyPressed(new OnKeyPressedEventArgs(cki));
            }
        }

        /// <summary>
        /// Fires the key pressed event.
        /// </summary>
        /// <param name="args"></param>
        private void FireKeyPressed(OnKeyPressedEventArgs args)
        {
            if(this.onKeyPressed != null)
            {
                this.onKeyPressed(this, args);
            }
        }
    }
}