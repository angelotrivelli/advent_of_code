using AoCHelper;

namespace AdventOfCode2021
{
    public class Day_08 : BaseDay
    {
        private bool IsExample = true;
        private readonly string input;

        public Day_08()
        {
            input = IsExample
                ? Example08.Input
                : File.ReadAllText(InputFilePath);

        }
        public override ValueTask<string> Solve_1()
        {
            return new ValueTask<string>($"");
        }

        public override ValueTask<string> Solve_2()
        {
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
