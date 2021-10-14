using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wiederholung_Kiefer
{
    /// <summary>
    /// This class contains methods for string creatin.
    /// </summary>
    class TextCreator
    {
        /// <summary>
        /// Creates a string with an amount of tabs in it.
        /// </summary>
        /// <param name="amount">the amount of tabs.</param>
        /// <returns>the number of tabs.</returns>
        public static string CreateTabAmount(int amount)
        {
            string output = string.Empty;

            int i = 0;
            while (i < amount)
            {
                output += "\t";
                i++;
            }
            return output;
        }
    }
}
