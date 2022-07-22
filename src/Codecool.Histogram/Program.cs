using System;
using System.Collections.Generic;
using System.IO;

namespace Codecool.Histogram
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            TextReader reader = new TextReader(
                Path.GetFullPath(Path.Combine(System.AppContext.BaseDirectory, "text.txt")));
            string chapter;
            chapter = reader.Read();

            Histogram histogram = new Histogram();

            Console.WriteLine("words length histogram with generated ranges:");
            List<Range> ranges = histogram.GenerateRanges(3, 5);
            histogram.Generate(chapter, ranges);
            Console.WriteLine(histogram.ToString());

            Console.WriteLine("words length histogram with specified ranges:");
            histogram = new Histogram();
            ranges = new List<Range>{ new Range(0, 1), new Range(2, 3), new Range(4, 6), new Range(7, 10) };
            histogram.Generate(chapter, ranges);
            Console.WriteLine(histogram.ToString());

            Console.WriteLine("words length normalized histogram with specified ranges:");
            histogram = new Histogram();
            ranges = new List<Range>{new Range(0, 1), new Range(2, 3), new Range(4, 6), new Range(7, 10)};
            histogram.Generate(chapter, ranges);
            histogram.NormalizeValues();
            Console.WriteLine(histogram.ToString());
        }
    }
}
