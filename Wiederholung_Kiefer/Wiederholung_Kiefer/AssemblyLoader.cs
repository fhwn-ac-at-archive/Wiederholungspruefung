using System;
using System.Reflection;

namespace Wiederholung_Kiefer
{
    /// <summary>
    /// This class contains logic, that loads assemblies into a variable.
    /// </summary>
    public class AssemblyLoader
    {
        /// <summary>
        /// This function loads an assembly from a file and returns true, if it worked.
        /// </summary>
        /// <param name="path">the path of the assembly.</param>
        /// <param name="loaded">true if successfull, false if exception was thrown.</param>
        /// <returns>the loaded asssembly.</returns>
        public static Assembly GetAssemblyFromPath(string path, out bool loaded)
        {
            try
            {
                Assembly a = Assembly.LoadFile(path);
                loaded = true;
                return a;
            }
            catch(Exception)
            {
                loaded = false;
                return null;
            }
        }
    }
}