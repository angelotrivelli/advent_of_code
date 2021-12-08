using AoCHelper;

namespace AdventOfCode2021
{

    public class Day_07 : BaseDay
    {
        private bool IsExample = false;

        private readonly string input;
        private readonly List<int> initialPositions;

        public Day_07()
        {
            input = IsExample
                ? Example07.Input
                : File.ReadAllText(InputFilePath);

            initialPositions = input.Split(",").Select(x => int.Parse(x)).ToList();
        }


        public override ValueTask<string> Solve_1()
        {

            List<int> fuel = new();

            var candidatePositions = Enumerable.Range(0, initialPositions.Max() + 1).ToList();

            foreach (var c in candidatePositions)
            {
                fuel.Add(initialPositions.Sum(x => Math.Abs(x - c)));
            }


            // var output = $"candidate : {string.Join(" ", candidatePositions)}\r\n";
            // output +=    $"fuel cost : {string.Join(" ", fuel)}\r\n";
            var output = "";
            output +=    $"lowest fuel = {fuel.Min()}";
            return new ValueTask<string>($"{output}");

        }

        public override ValueTask<string> Solve_2()
        {
            var candidatePositions = Enumerable.Range(0, initialPositions.Max() + 1).ToList();

            var minFuel = initialPositions.Sum(x => Math.Abs(x - candidatePositions[0]) * (Math.Abs(x - candidatePositions[0]) + 1) / 2);
            foreach (var c in candidatePositions.Skip(1))
            {
                var fuel = initialPositions.Sum(x => Math.Abs(x - c) * (Math.Abs(x - c)+1)/2);
                minFuel = minFuel > fuel
                    ? fuel
                    : minFuel;
            }


            // var output = $"candidate : {string.Join(" ", candidatePositions)}\r\n";
            // output +=    $"fuel cost : {string.Join(" ", fuel)}\r\n";
            var output = "";
            output += $"lowest fuel = {minFuel}";
            return new ValueTask<string>($"{output}");
        }
    }
}
