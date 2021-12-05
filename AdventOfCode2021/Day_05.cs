using AoCHelper;
using System.Text.RegularExpressions;

namespace AdventOfCode2021
{
    public class Day_05 : BaseDay
    {
        private bool IsExample = false;
        private readonly Regex linesRegex 
            = new(@"^(?<x0>\d+),(?<y0>\d+) -> (?<x1>\d+),(?<y1>\d+)", RegexOptions.Multiline);
        private IEnumerable<Vent> vents;
        private readonly string input;


        public Day_05()
        {
            input = IsExample
                ? Example.Input
                : File.ReadAllText(InputFilePath);
        }

        public override ValueTask<string> Solve_1()
        {

            Vent.Points = new HashSet<Point>();
            vents = linesRegex
                        .Matches(input)
                        .Select(m => new Vent(int.Parse(m.Groups["x0"].Value),
                                              int.Parse(m.Groups["y0"].Value),
                                              int.Parse(m.Groups["x1"].Value),
                                              int.Parse(m.Groups["y1"].Value)));

            var c = vents.Count();
            var v = Vent.Points;
            
            var vx = v.Max(p => p.x);
            var vy = v.Max(p => p.y);

            return new ValueTask<string>($"num intersections = {Vent.Points.Where(p => p.n>1).Count()}");
        }


        public override ValueTask<string> Solve_2()
        {
            Vent.Points = new HashSet<Point>();
            vents = linesRegex
                        .Matches(input)
                        .Select(m => new Vent(int.Parse(m.Groups["x0"].Value),
                                              int.Parse(m.Groups["y0"].Value),
                                              int.Parse(m.Groups["x1"].Value),
                                              int.Parse(m.Groups["y1"].Value), 
                                              includeDiags:true));

            var c = vents.Count();
            var v = Vent.Points;

            var vx = v.Max(p => p.x);
            var vy = v.Max(p => p.y);

            return new ValueTask<string>($"num intersections (counting diagonals) = {Vent.Points.Where(p => p.n > 1).Count()}");
        }
    }


    public class Point
    {
        public int x { get; set; }
        public int y { get; set; }
        public int n { get; set; }

        public override bool Equals(object obj)
        {
            var otherPoint = obj as Point;
            if (otherPoint is null) return false;
            return x == otherPoint.x && y == otherPoint.y;
        }

        public override int GetHashCode()
        {
            var hashcode = 23;
            hashcode = (hashcode * 37) + x;
            hashcode = (hashcode * 37) + y;
            return hashcode;
        }
    }

    public class Vent
    {
        public static HashSet<Point> Points = new();

        public Vent(int x0, int y0, int x1, int y1, bool includeDiags = false)
        {
            if (x0 != x1 && y0 == y1)
            {
                for (int x = Math.Min(x0, x1); x <= Math.Max(x0, x1); x++)
                {
                    var p = new Point { x = x, y = y0, n = 1 };
                    if (Points.TryGetValue(p, out var pt))
                    {
                        pt.n++;
                    }
                    else
                    {
                        Points.Add(p);
                    }
                }
                return;
            }

            if (x0 == x1 && y0 != y1)
            {
                for (int y = Math.Min(y0, y1); y <= Math.Max(y0, y1); y++)
                {
                    var p = new Point { x = x0, y = y, n = 1 };
                    if (Points.TryGetValue(p, out var pt))
                    {
                        pt.n++;
                    }
                    else
                    {
                        Points.Add(p);
                    }
                }
                return;
            }


            if (includeDiags && x0 != x1 && y0 != y1)
            {
                var numPoints = Math.Max(x0, x1) - Math.Min(x0, x1) + 1;
                var x = Enumerable.Range(Math.Min(x0, x1), numPoints).ToList();
                var y = Enumerable.Range(Math.Min(y0, y1), numPoints).ToList();

                if (x[0] != x0) x.Reverse();
                if (y[0] != y0) y.Reverse();

                var index = Enumerable.Range(0, numPoints);

                foreach (var i in index)
                {
                    var p = new Point { x = x[i], y = y[i], n = 1 };

                    if (Points.TryGetValue(p, out var pt))
                    {
                        pt.n++;
                    }
                    else
                    {
                        Points.Add(p);
                    }
                }
            }
        }
    }

    public static class Example
    {
        public static string Input = "0,9 -> 5,9" + "\r\n" +
                                     "8,0 -> 0,8" + "\r\n" +
                                     "9,4 -> 3,4" + "\r\n" +
                                     "2,2 -> 2,1" + "\r\n" +
                                     "7,0 -> 7,4" + "\r\n" +
                                     "6,4 -> 2,0" + "\r\n" +
                                     "0,9 -> 2,9" + "\r\n" +
                                     "3,4 -> 1,4" + "\r\n" +
                                     "0,0 -> 8,8" + "\r\n" +
                                     "5,5 -> 8,2";
    }
}
