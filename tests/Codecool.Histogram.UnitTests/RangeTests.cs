using System;
using System.IO;
using NUnit.Framework;

namespace Codecool.Histogram.UnitTests
{
    [TestFixture]
    public class RangeTests
    {
        private string _word;
        private int _from;
        private int _to;

        [SetUp]
        public void Setup()
        {
            _word = "Draco";
            _from = 1;
            _to = 5;
        }

        [Test]
        public void LengthInRange()
        {
            int result = _word.Length;
            Assert.GreaterOrEqual(result, _from);
            Assert.LessOrEqual(result, _to);
            
        }

        [Test]
        public void EqualToFrom()
        {
            string word = "a";
            int result = word.Length;
            Assert.AreEqual(result, _from);
        }


        [Test]
        public void EqualToTo()
        {
            int result = _word.Length;
            Assert.AreEqual(result, _to);
        }


        [Test]
        public void OutOfRangeMin()
        {
            string word = String.Empty;
            int result = word.Length;
            Assert.Less(result, _from);
        }

        [Test]
        public void OutOfRangeMax()
        {
            string word = "Draco Malfoy";
            int result = word.Length;
            Assert.Greater(result, _to);
        }


        [Test]
        public void StringsAreEquals()
        {
            string result = $"{_from} - {_to}";
            string rangeResult = new Range(_from, _to).ToString();
            Assert.AreEqual(result, rangeResult);

        }


    }
}
