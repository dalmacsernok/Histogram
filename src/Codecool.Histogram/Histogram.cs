using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Codecool.Histogram
{
    /// <summary>
    /// Class to generate diagram of word distribution in text based on their length.
    /// </summary>
    public class Histogram
    {
        private Dictionary<Range, int> _histogram;


        public Histogram()
        {
            _histogram = new Dictionary<Range, int>();
        }

        /// <summary>
        /// Get the already generated histogram
        /// where the key is the range
        /// and the value is the count of words in the given range
        /// <returns> Histogram as a Dictionary of types Range, int </returns>
        /// </summary>
        public Dictionary<Range, int> GetHistogram()
        {
            return _histogram;
        }

        /// <summary>
        /// Generate x amount of step width ranges.
        /// <param name="step"> Required range width. </param>
        /// <param name="amount"> Required amount of ranges. </param>
        /// <returns> List of ranges. </returns>
        /// </summary>
        public List<Range> GenerateRanges(int step, int amount)
        {
            if (step < 0)
            {
                throw new ArgumentException("Step must be positive.");
            }

            if (amount < 0)
            {
                throw new ArgumentException("Amount must be positive.");
            }

            List<Range> ranges = new List<Range>();

            for (int i = 0; i < amount; i++)
            {
                ranges.Add(new Range(i * step + 1, i * step + step));
            }
            return ranges;
        }

        /// <summary>
        /// Calculates the distribution of words in a given text based on provided ranges.
        /// <param name="text"> Text to analyze. </param>
        /// <param name="ranges"> Words' length distribution ranges. </param>
        /// <returns> Counts of words in given ranges. </returns>
        /// </summary>
        public Dictionary<Range, int> Generate(string text, List<Range> ranges)
        {
            if (text == null || ranges == null)
            {
                throw new ArgumentException();
            }

            _histogram = new Dictionary<Range, int>();
            if ("".Equals(text))
            {
                return _histogram;
            }
            // remove punctuation and split by whitespace
            string[] words = Regex.Replace(text, "[^a-zA-Z \\r?\\n]", "").Split(" ");

            foreach (string word in words)
            {
                foreach (Range range in ranges)
                {
                    if (range.IsInRange(word))
                    {
                        int count = _histogram.GetValueOrDefault(range, 0);
                        _histogram[range] = count + 1;
                    }
                }
            }
            return _histogram;
        }

        /// <summary>
        /// Normalize the values for better understanding.
        /// For every feature, the minimum value of that feature gets transformed into a 0,
        /// the maximum value gets transformed into a 100,
        /// and every other value gets transformed into an int between 0 and 100.
        /// The following formula applied to each range:
        ///      `V' = (V - min) * 100 / (max - min)`
        /// </summary>
        public void NormalizeValues()
        {
            foreach (KeyValuePair<Range, int> item in _histogram)
            {
                _histogram[item.Key] = (item.Value - GetMinValue()) * 100 / (GetMaxValue() - GetMinValue());
            }

        }


        public int GetMaxValue()
        {
            return _histogram.Values.Max();
        }

        public int GetMinValue()
        {
            return _histogram.Values.Min();
        }

        /// <summary>
        /// Returns with the string representation of the generated histogram
        /// where every range is in separate line and the bars represented as asterisks.
        /// <returns> String representation of histogram. </returns> 
        /// </summary>
        public override string ToString()
        {
            StringBuilder resultBuilder = new StringBuilder();

            foreach (KeyValuePair<Range, int> pair in _histogram)
            {
                int count = pair.Value;
                string line = $"{pair.Key.ToString()} | {new string('*', count)}{Environment.NewLine}";
                resultBuilder.Append(line);
            }

            return resultBuilder.ToString();

        }
    }
}
