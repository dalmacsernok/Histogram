using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Codecool.Histogram.UnitTests
{
    [TestFixture]
    public class HistogramTests
    {
        private int _step;
        private int _amount;
        private List<Range> _ranges;
        private string _text;
        private Dictionary<Range, int> _dictRanges;
        private Dictionary<Range, int> _dictOutRanges;


        [SetUp]
        public void Setup()
        {
            _step = 3;
            _amount = 3;
            _ranges = new List<Range>();
            _dictRanges = new Dictionary<Range, int>();
            _dictOutRanges = new Dictionary<Range, int>();
            for (int i = 0; i < _amount; i++)
            {
                _ranges.Add(new Range(i * _step + 1, i * _step + _step));
            }

            _text =
                "Harry Potter and the Sorcerer's Stone Mr. and Mrs. Dursley, of number four, Privet Drive, were proud to say that they were perfectly normal, thank you very much.";
            

        }

        [Test]
        public void PositiveRanges()
        {

            List<Range> result = new Histogram().GenerateRanges(_step, _amount);
            Assert.AreEqual(_ranges, result);
        }

        [Test]
        public void NegativeStep()
        {
            _step = -1;
            Assert.Less(_step, 0);

        }

        [Test]
        public void NegativeAmount()
        {
            _amount = -1;
            Assert.Less(_amount, 0);
        }

        [Test]
        public void TextWithinTheGivenRange()
        {
            string[] words = Regex.Replace(_text, "[^a-zA-Z \\r?\\n]", "").Split(" ");

            foreach (string word in words)
            {
                foreach (Range range in _ranges)
                {
                    if (range.IsInRange(word))
                    {
                        int count = _dictRanges.GetValueOrDefault(range, 0);
                        _dictRanges[range] = count + 1;
                    }
                }
            }
            Dictionary<Range, int> result = new Histogram().Generate(_text, _ranges);
            Assert.AreEqual(_dictRanges, result);
        }

        [Test]
        public void TextOutsideTheGiveRange()
        {
            string[] words = Regex.Replace(_text, "[^a-zA-Z \\r?\\n]", "").Split(" ");
            
            foreach (string word in words)
            {
                foreach (Range range in _ranges)
                {
                    if (!range.IsInRange(word))
                    {
                        int count = _dictRanges.GetValueOrDefault(range, 0);
                        _dictOutRanges[range] = count + 1;
                    }
                }
            }
            Dictionary<Range, int> result = new Histogram().Generate(_text, _ranges);
            Assert.AreNotEqual(_dictOutRanges, result);

        }

        [Test]
        public void NullText()
        {
            _text = null;
            Assert.Throws<ArgumentException>(() => new Histogram().Generate(_text, _ranges));

        }


        [Test]
        public void EmptyText()
        {
            Dictionary<Range, int> expected = new Dictionary<Range, int>();
            _text = String.Empty;
            Dictionary<Range, int> result = new Histogram().Generate(_text, _ranges);
            Assert.AreEqual(expected, result);

        }

        [Test]
        public void NullRanges()
        {
            _ranges = null;
            Assert.Throws<ArgumentException>(() => new Histogram().Generate(_text, _ranges));
        }

        [Test]
        public void BeforeGenerating()
        {
            Assert.AreEqual(_dictRanges, new Histogram().GetHistogram());
        }

        [Test]
        public void AfterGenerating()
        {
            Histogram histogram = new Histogram();
            _dictRanges = histogram.Generate(_text, _ranges);
            Assert.AreEqual(_dictRanges, histogram.GetHistogram());
        }

        [Test]
        public void AfterGeneratingMultiple()
        {
            Histogram histogram = new Histogram();
            _dictRanges = histogram.Generate(_text, _ranges);
            _dictRanges = histogram.Generate(_text, _ranges);
            _dictRanges = histogram.Generate(_text, _ranges);
            Assert.AreEqual(_dictRanges, histogram.GetHistogram());

        }


        [Test]
        public void HistogramToStringBeforeGenerating()
        {
            Histogram histogram = new Histogram();
            string resultBuilder = "";
            Assert.AreEqual(resultBuilder, histogram.ToString());
        }


        [Test]
        public void HistogramToStringAfterGenerating()
        {
            Histogram histogram = new Histogram();
            histogram.Generate(_text, _ranges);
            StringBuilder resultBuilder = new StringBuilder();

            foreach (KeyValuePair<Range, int> pair in histogram.GetHistogram())
            {
                int count = pair.Value;
                string line = $"{pair.Key.ToString()} | {new string('*', count)}{Environment.NewLine}";
                resultBuilder.Append(line);
            }

            Assert.AreEqual(resultBuilder.ToString(), histogram.ToString());
        }

        [Test]
        public void GetMinTest()
        {
            Histogram histogram = new Histogram();
            histogram.Generate(_text, _ranges);
            Assert.AreEqual(histogram.GetMinValue(), histogram.GetHistogram().Values.Min());
        }


        [Test]
        public void GetMaxTest()
        {
            Histogram histogram = new Histogram();
            histogram.Generate(_text, _ranges);
            Assert.AreEqual(histogram.GetMaxValue(), histogram.GetHistogram().Values.Max());
        }

        
    }
}
