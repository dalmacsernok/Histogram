using System;
using System.Collections;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace Codecool.Histogram.UnitTests
{
    [TestFixture]
    public class TextReaderTests
    {
        private TextReader _multipleLineText;
        private TextReader _oneLineText;
        private TextReader _emptyText;

        [SetUp]
        public void Setup()
        {
            _multipleLineText = new TextReader(
                Path.GetFullPath(Path.Combine(System.AppContext.BaseDirectory, "text.txt")));
            _oneLineText = new TextReader(
                Path.GetFullPath(Path.Combine(System.AppContext.BaseDirectory, "test.txt")));
            _emptyText = new TextReader(
                Path.GetFullPath(Path.Combine(System.AppContext.BaseDirectory, "empty.txt")));
        }

        [Test]
        public void FileIsEmpty()
        {
            string result = string.Empty;
            string emptyText = _emptyText.Read();
            Assert.AreEqual(result, emptyText);
        }

        [Test]
        public void FileIsOneLine()
        {
            StreamReader reader = new StreamReader(
                Path.GetFullPath(Path.Combine(System.AppContext.BaseDirectory, "test.txt")));
            string result = reader.ReadLine() + Environment.NewLine;
            string oneLineText = _oneLineText.Read();
            Assert.AreEqual(result, oneLineText);

        }

        [Test]
        public void FileIsMultipleLine()
        {
            int result = File.ReadLines(Path.Combine(AppContext.BaseDirectory, "text.txt")).Count() + 1;
            int multipleLineText = _multipleLineText.Read().Split("\n").Length;
            Assert.AreEqual(result, multipleLineText);
        }

        [Test]
        public void FileNameIsValid()
        {
            string fileName = "text.txt";
            string[] files = { "text.txt", "test.txt", "empty.txt" };
            if (!File.Exists(Path.GetFullPath(Path.Combine(System.AppContext.BaseDirectory, fileName))))
            {
                throw new FileNotFoundException(string.Format("Cannot find the file."));
            }
            Assert.Contains(fileName, files);
            
            

        }

        
    }

    
}
