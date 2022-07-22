using System;

namespace Codecool.Histogram
{
    /// <summary>
    /// The Range class represents numbers between upper and lower limits.
    /// </summary>
    public class Range
    {
        private int _from;
        private int _to;

        /// <summary>
        /// Constructs a Range with specified lower and upper limit.
        /// Lower and upper limits cannot be smaller than 0.
        /// <param name="from"> Lower limit. </param>
        /// <param name="to"> Upper limit. </param>
        /// </summary>
        public Range(int from, int to)
        {
            if (from < 0 || to < from)
            {
                throw new ArgumentException();
            }

            _from = from;
            _to = to;
        }

        /// <summary>
        /// Returns that the given word belongs to the particular range or not.
        /// <param name="word"> Text to investigate. </param>
        /// <returns> The word belongs to the particular range or not. </returns> 
        /// </summary>
        public bool IsInRange(string word)
        {
            return word.Length >= _from && word.Length <= _to;
        }

        public override string ToString()
        {
            return $"{_from} - {_to}";
        }

        public override bool Equals(object o)
        {
            if (this == o) return true;
            if (o == null || GetType() != o.GetType()) return false;
            return _from == ((Range)o)._from && _to == ((Range)o)._to;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_from, _to);
        }
    }
}
