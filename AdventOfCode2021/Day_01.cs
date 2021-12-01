using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day_01 : BaseDay
    {
        private readonly string input;
        private readonly int[] depths;

        public Day_01()
        {
            input = File.ReadAllText(InputFilePath);

            depths = input
                        .Split("\r\n")
                        .Select(d => int.Parse(d))
                        .ToArray();
        }

        public override ValueTask<string> Solve_1()
        {
            var largerTally = 0;
            for (int i=1 ; i<depths.Length ; i++ )
            {
                largerTally += (depths[i] > depths[i-1]) ? 1 : 0;
            }
            return new ValueTask<string>($"Number of times a depth measurement increases: {largerTally}");
        }

        public override ValueTask<string> Solve_2()
        {
            var largerTally = 0;
            for (int i=0 ; i<depths.Length - 3 ; i++)
            {
                largerTally += (depths.Skip(i+1).Take(3).Sum() > depths.Skip(i).Take(3).Sum()) ? 1 : 0;
            }
             return new ValueTask<string>($"Number of times a depth measurement increases: {largerTally}");
        }
    }
}
