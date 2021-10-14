using System;

namespace Wiederholung_Kiefer
{
    /// <summary>
    /// This class contains thread arguments for the keyboard watcher.
    /// </summary>
    internal class KeyboardWatcherThreadArgs : EventArgs
    {
        /// <summary>
        /// Tells the keyboard watcher thread, when to stop.
        /// </summary>
        bool stop;

        /// <summary>
        /// The constructor of the keyboard watcher.
        /// </summary>
        public KeyboardWatcherThreadArgs()
        {
            this.stop = false;
        }
        
        /// <summary>
        /// The property of the stop field.
        /// </summary>
        public bool Stop
        {
            get
            {
                return this.stop;
            }
            set
            {
                this.stop = value;
            }
        }
    }
}