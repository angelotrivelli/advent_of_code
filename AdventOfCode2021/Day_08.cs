using System.Text.RegularExpressions;
using AoCHelper;

namespace AdventOfCode2021
{
    public class Day_08 : BaseDay
    {
        private bool IsExample = true;
        private readonly string input;

        private readonly Regex digitsRegex = new(@"^[\w ]+(?= \| )",RegexOptions.Multiline);
        private readonly Regex displayRegex = new(@"(?<= \| )[\w ]+");
        public Day_08()
        {
            input = IsExample
                ? Example08.Input
                : File.ReadAllText(InputFilePath);




        }


        public override ValueTask<string> Solve_1()
        {
            var displays = displayRegex.Matches(input)
                           .SelectMany(m => m.Value.Trim().Split(" "))
                           .ToList();

            // count the number of instances of 1, 4, 7, 8 in displays
            var barCount = displays.Where(d => d.Length==2 || d.Length==4 || d.Length==3 || d.Length==7);
            return new ValueTask<string>($"total number of 1's, 4's, 7's and 8's = {barCount.Count()}");
        }


        public override ValueTask<string> Solve_2()
        {
            var displaysInput = displayRegex.Matches(input)
                                            .Select(m => m.Value.Trim().Split(" "))
                                            .ToList();

            var digitsInput = digitsRegex.Matches(input)
                                         .Select(m => m.Value.Trim().Split(" "))
                                         .ToList();


            // apply logic relations to identify digits.

            // Construct a bool array for each digit and display char, where the place values represent "abcdefg".
            // Insert a true if the digit has that letter and a false if it does not.



            var original = new string[] {"abcefg","cf","acdeg","acdfg","bcdf","abdfg","abdefg","acf","abcdefg","abcdfg"};
            var ordered = decode(original);

            Console.WriteLine("*******");
            foreach(var x in ordered)
            {
                Console.WriteLine(x.signals.PadLeft(7,' ') + "  [" + Convert.ToString(x.bits,2).PadLeft(7,'0') + "]" + $" {x.len}");
            }
            Console.WriteLine("*******");
            Console.WriteLine("");


            // pick a line
            var line = digitsInput[0];
            var disordered = decode(line);

            Console.WriteLine("*******");
            foreach(var x in disordered)
            {
                Console.WriteLine(x.signals.PadLeft(7,' ') + "  [" + Convert.ToString(x.bits,2).PadLeft(7,'0') + "]" + $" {x.len}");
            }
            Console.WriteLine("*******");
            Console.ReadLine();
            return new ValueTask<string>($"");
        }


        public List<(string signals, uint bits, int len)> decode (string[] line)
        {
            var pv = "abcdefg".ToCharArray().Reverse();
            List<(string signals, uint bits, int len)> t = new();
            foreach(var d in line)
            {
                uint digit = 0;
                int i = 0;
                foreach(var p in pv)
                {
                    digit |= (d.ToCharArray().Contains(p) ? (uint)(1 << i) : (uint)0);
                    i++;
                }
                t.Add((string.Join("",d.ToCharArray().OrderBy(x =>x)),digit,d.Length));
            }
            return t;
        }
    }
}
