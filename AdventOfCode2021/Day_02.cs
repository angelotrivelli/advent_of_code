using AoCHelper;
using System.Text.RegularExpressions;

namespace AdventOfCode2021
{
    public class Day_02 : BaseDay
    {

        private readonly Regex cmdRegex = new Regex(@"^(?<cmd>up|down|forward) (?<arg>\d+)", RegexOptions.Multiline);
        private readonly MatchCollection moves;


        public Day_02()
        {
            moves = cmdRegex.Matches(File.ReadAllText(InputFilePath));
        }


        public override ValueTask<string> Solve_1()
        {
            var horizontalPosition = 0;
            var depth = 0;

            foreach (Match m in moves)
            {
                switch (m.Groups["cmd"].Value)
                {
                    case ("up"):
                        depth -= int.Parse(m.Groups["arg"].Value);
                        break;
                    case ("down"):
                        depth += int.Parse(m.Groups["arg"].Value);
                        break;
                    case ("forward"):
                        horizontalPosition += int.Parse(m.Groups["arg"].Value);
                        break;
                    default:
                        break;
                }
            }
            return new ValueTask<string>("horizontal_distance*depth =" + (horizontalPosition * depth).ToString());
        }


        public override ValueTask<string> Solve_2()
        {

            var horizontalPosition = 0L;
            var depth = 0L;
            var aim = 0L;

            foreach (Match m in moves)
            {
                switch (m.Groups["cmd"].Value)
                {
                    case ("up"):
                        aim -= int.Parse(m.Groups["arg"].Value);
                        break;
                    case ("down"):
                        aim += int.Parse(m.Groups["arg"].Value);
                        break;
                    case ("forward"):
                        horizontalPosition += int.Parse(m.Groups["arg"].Value);
                        depth += aim * int.Parse(m.Groups["arg"].Value);
                        break;
                    default:
                        break;
                }
            }
            return new ValueTask<string>("horizontal_distance*depth =" + (horizontalPosition * depth).ToString());
        }
    }

}
