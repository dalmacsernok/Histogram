using System;
using System.IO;
using System.Text;

namespace Codecool.Histogram
{
    public class TextReader
    {
        private readonly string _fileName;

        /// <summary>
        /// Constructs a TextReader with a specified file.
        /// <param name="fileName"> Name of the file to read. </param>
        /// </summary>
        public TextReader(string fileName)
        {
            _fileName = fileName;
        }

        /// <summary>
        /// Returns the text content of the file specified in constructor.
        /// <returns>  Text content of previously specified file. </returns>
        /// <exception cref="IOException"> Thrown in case of non-existing file. </exception>
        /// </summary>
        public string Read()
        {
            StringBuilder resultBuilder = new StringBuilder();
            StreamReader reader = new StreamReader(_fileName);

            string strCurrentLine;
            while ((strCurrentLine = reader.ReadLine()) != null)
            {
                resultBuilder.Append($"{strCurrentLine}{Environment.NewLine}");
            }
            return resultBuilder.ToString();
        }
    }
}
