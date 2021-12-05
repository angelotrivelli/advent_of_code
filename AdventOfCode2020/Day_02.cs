using AoCHelper;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public class Day_02 : BaseDay
    {
        private readonly Regex passwdRegex = new(@"^(?<min>\d+)\-(?<max>\d+) (?<char>\w):\s(?<passwd>\w+)", RegexOptions.Multiline);

        private readonly string input;
        private readonly MatchCollection matches;

        public Day_02()
        {
            input = File.ReadAllText(InputFilePath);
            matches = passwdRegex.Matches(input);
        }

        public override ValueTask<string> Solve_1()
        {
            var validPasswords = 0;
            foreach (Match m in matches)
            {
                var min = int.Parse(m.Groups["min"].Value);
                var max = int.Parse(m.Groups["max"].Value);
                var character = char.Parse(m.Groups["char"].Value);
                var passwd = m.Groups["passwd"].Value.ToCharArray();
                var instances = 0;
                foreach (char c in passwd)
                {
                    if (c == character) instances++;
                }

                if (instances >= min && instances <= max) validPasswords++;

            }
            return new ValueTask<string>(validPasswords.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            // different interpretation of min and max.
            var validPasswords = 0;
            foreach (Match m in matches)
            {
                var char1 = int.Parse(m.Groups["min"].Value);
                var char2 = int.Parse(m.Groups["max"].Value);
                var character = char.Parse(m.Groups["char"].Value);
                var passwd = m.Groups["passwd"].Value.ToCharArray();
                var instances = 0;

                // Should I check for validity of indexes?
                // It's advent, let's take a leap of faith: No!
                if (passwd[char1 - 1] == character) instances++;
                if (passwd[char2 - 1] == character) instances++;
                if (instances == 1) validPasswords++;

            }
            return new ValueTask<string>(validPasswords.ToString());
        }
    }
}
