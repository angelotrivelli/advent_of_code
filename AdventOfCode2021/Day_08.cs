using AoCHelper;
using System.Text.RegularExpressions;

namespace AdventOfCode2021
{
    public class Day_08 : BaseDay
    {
        private bool IsExample = true;
        private readonly string input;

        private readonly Regex digitsRegex = new(@"(?<= \| )[\w ]+(?=\r\n|$)", RegexOptions.Multiline);

        public Day_08()
        {
            input = IsExample
                ? Example08.Input
                : File.ReadAllText(InputFilePath);

        }
        public override ValueTask<string> Solve_1()
        {

            var lines = digitsRegex.Matches(input).Select(l => l.Value);

            var lines_str = string.Join(" ", lines);
            var all_digits = lines_str
                                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                                .Where(d => d.Length == 2 || d.Length == 4 || d.Length == 3 || d.Length == 7);



            return new ValueTask<string>($"num of 1's, 4's, 7's 8's = {all_digits.Count()}");
        }

        public override ValueTask<string> Solve_2()
        {
            var lines = digitsRegex.Matches(input).Select(l => l.Value);

            foreach(var line in lines)
            {

            }

            var d_1 = all_digits.Where(d => d.Length == 2);
            var d_4 = all_digits.Where(d => d.Length == 4);
            var d_7 = all_digits.Where(d => d.Length == 3);
            var d_8 = all_digits.Where(d => d.Length == 7);



            return new ValueTask<string>($"");
        }


        public class Decoder
        {
            public Dictionary<string, int> Digits { get; }

            public Decoder()
            {
                Digits = new()
                {
                    { "abcefg", 0 },
                    { "cf", 1 },
                    { "acdeg", 2 },
                    { "acdfg", 3 },
                    { "bcdf", 4 },

                    { "abdfg", 5 },
                    { "abdefg", 6 },
                    { "acf", 7 },
                    { "abcdefg", 8 },
                    { "abcdfg", 9 },
                };
            }
        }
    }
}
