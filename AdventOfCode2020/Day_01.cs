using AoCHelper;

namespace AdventOfCode2020
{
    // https://adventofcode.com/2020/day/1
    public class Day_01 : BaseDay
    {
        private IEnumerable<int> entries;
        private int size;

        public Day_01()
        {
            entries = File.ReadAllText(InputFilePath)
                          .Split("\r\n")
                          .Select(x => int.Parse(x));

            size = entries.Count();
        }


        public override ValueTask<string> Solve_1()
        {
            for (int i = 0; i < size - 1; i++)
            {
                for (int j = i + 1; j < size; j++)
                {
                    if (entries.Skip(i).First() + entries.Skip(j).First() == 2020)
                    {
                        return new ValueTask<string>((entries.Skip(i).First() * entries.Skip(j).First()).ToString());
                    }
                }
            }

            return new ValueTask<string>("no answer");
        }


        public override ValueTask<string> Solve_2()
        {
            for (int i = 0; i < size - 2; i++)
            {
                for (int j = i + 1; j < size - 1; j++)
                {
                    for (int k = i + 2; k < size; k++)
                    {
                        if (entries.Skip(i).First() + entries.Skip(j).First() + entries.Skip(k).First() == 2020)
                        {
                            return new ValueTask<string>((entries.Skip(i).First() * entries.Skip(j).First() * entries.Skip(k).First()).ToString());
                        }
                    }
                }
            }
            return new ValueTask<string>($"no answer");
        }
    }
}
