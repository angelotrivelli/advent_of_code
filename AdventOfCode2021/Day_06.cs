using AoCHelper;

namespace AdventOfCode2021
{
    public class Day_06 : BaseDay
    {
        private bool IsExample = true;
        private List<LFish> fishes;
        public Day_06()
        {
            var input = IsExample
                            ? Example.Input
                            : File.ReadAllText(InputFilePath);

            fishes = input.Split(",").Select(c => new LFish(int.Parse(c))).ToList();
        }

        public override ValueTask<string> Solve_1()
        {
            for (int day = 1; day <= 18; day++)
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

            return new ValueTask<string>($"number of Lanternfish = {fishes.Count()}");
        }

        public override ValueTask<string> Solve_2()
        {
            ulong totalFish = 0;
            int numDays = 18;
            var dayOffsets = fishes.Select(f => f.Counter).ToList();

            foreach(var f in fishes)
            {
                totalFish += (ulong)Math.Pow(2,Math.Floor((double)(numDays - f.Counter-3)/9));
            }

            return new ValueTask<string>($"number of Lanternfish (80 days) = {(long) totalFish} ");
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
