using System.Linq;
using AoCHelper;

namespace AdventOfCode2021
{
    public class Day_06 : BaseDay
    {
        private bool IsExample = false;
        public Day_06()
        {
        }

        public override ValueTask<string> Solve_1()
        {
            int numDays = 80;

            var input = IsExample
                            ? Example.Input
                            : File.ReadAllText(InputFilePath);

            var fishes = input
                            .Split(",")
                            .Select(c => new LFish(int.Parse(c)))
                            .ToList();

            for (int day = 1; day <= numDays; day++)
            {
                var newFishes = new List<LFish>();
                foreach(var f in fishes)
                {
                    var newFish = f.DayPasses();
                    if (newFish is not null)
                    {
                        newFishes.Add(newFish);
                    }
                }
                fishes.AddRange(newFishes);
            }
            return new ValueTask<string>($"number of Lanternfish ({numDays} days) = {fishes.Count()}");
        }

        public override ValueTask<string> Solve_2()
        {
            int numDays = 256;

            var input = IsExample
                        ? Example.Input
                        : File.ReadAllText(InputFilePath);

            var fishes = new Dictionary<int,ulong>();
            foreach(int gen in Enumerable.Range(0,9))
            {
                fishes.Add(gen,0);
            }

            var initialFishes = input.Split(",").Select(x => int.Parse(x)).ToList();
            foreach(var f in initialFishes)
            {
                fishes[f] += 1;
            }

            for (int i=1 ; i<=numDays ; i++)
            {
                ulong newGen = 0;
                foreach(int gen in Enumerable.Range(0,9))
                {
                    if (gen==0)
                    {
                        newGen = fishes[gen];
                        continue;
                    }
                    fishes[gen-1] = fishes[gen];
                }
                fishes[6] += newGen;
                fishes[8] = newGen;
            }

            ulong totalFishes = 0;
            foreach(int gen in fishes.Keys)
            {
                totalFishes += fishes[gen];
            }

            return new ValueTask<string>($"number of Lanternfish ({numDays} days) = {totalFishes} ");
        }
    }


    public class LFish
    {
        public int Counter { get; set; }

        public LFish(int counter)
        {
            Counter = counter;
        }

        public LFish DayPasses()
        {
            if (Counter == 0)
            {
                Counter = 6;
                return new LFish(8);
            }

            Counter--;
            return null;
        }
    }
}
