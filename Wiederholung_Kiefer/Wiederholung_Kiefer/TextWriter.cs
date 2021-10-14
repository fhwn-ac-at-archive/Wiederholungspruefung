using System.IO;
using System.Text;

namespace Wiederholung_Kiefer
{
    /// <summary>
    /// This class contains logic to write text into a file.
    /// </summary>
    class TextWriter
    {
        /// <summary>
        /// This method writes an input string into a file.
        /// </summary>
        /// <param name="input">an input text</param>
        public static void WriteToFile(string input)
        {
            using (FileStream stream = new FileStream("output.txt", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
            {
                byte[] buffer = Encoding.ASCII.GetBytes(input);

                stream.Seek(0, SeekOrigin.Begin);
                stream.Write(buffer, 0, buffer.Length);
            }
        }
    }
}
