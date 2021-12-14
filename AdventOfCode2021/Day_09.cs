using AoCHelper;
using System.Text.RegularExpressions;

namespace AdventOfCode2021
{

    public class Day_09 : BaseDay
    {
        private static bool IsExample = false;
        private readonly string input;

        

        public Day_09()
        {
            input = IsExample
                        ? Example09.Input
                        : File.ReadAllText(InputFilePath);


        }


        public override ValueTask<string> Solve_1()
        {
            var h = new HeightMap(input);
            // int i = 0;
            // Console.WriteLine($"i={i}, (r,c) = {h.Coords(i)}, i = {h.Coords( h.Coords(i))}");
            // i = 3;
            // Console.WriteLine($"i={i}, (r,c) = {h.Coords(i)}, i = {h.Coords(h.Coords(i))}");
            // i = 4;
            // Console.WriteLine($"i={i}, (r,c) = {h.Coords(i)}, i = {h.Coords(h.Coords(i))}");
            // i = 5;
            // Console.WriteLine($"i={i}, (r,c) = {h.Coords(i)}, i = {h.Coords(h.Coords(i))}");
            // i = 7;
            // Console.WriteLine($"i={i}, (r,c) = {h.Coords(i)}, i = {h.Coords(h.Coords(i))}");
            // i = 9;
            // Console.WriteLine($"i={i}, (r,c) = {h.Coords(i)}, i = {h.Coords(h.Coords(i))}");
            // i = 10;
            // Console.WriteLine($"i={i}, (r,c) = {h.Coords(i)}, i = {h.Coords(h.Coords(i))}");
            // i = 11;
            // Console.WriteLine($"i={i}, (r,c) = {h.Coords(i)}, i = {h.Coords(h.Coords(i))}");
            // i = 12;
            // Console.WriteLine($"i={i}, (r,c) = {h.Coords(i)}, i = {h.Coords(h.Coords(i))}");
            // i = 49;
            // Console.WriteLine($"i={i}, (r,c) = {h.Coords(i)}, i = {h.Coords(h.Coords(i))}");
            // 
            // Console.WriteLine($"rows,cols = {h.RowDim},{h.ColDim}");

            var sum_of_low_points_plus_1 = h.GetLowPoints().Sum(x => x.height + 1);
            var lows = h.GetLowPoints();
            foreach(var l in lows)
            {
                Console.WriteLine($"i= {l.i}, (r,c)= {h.Coords(l.i)}, h= {l.height}");
            }

            return new ValueTask<string>($"Sum of (low points + 1) = {sum_of_low_points_plus_1}");
        }

        public override ValueTask<string> Solve_2()
        {
            return new ValueTask<string>($"");
        }

    }

    public class HeightMap
    {
        private readonly Regex heightRegex = new(@"\d{1}");

        public int RowDim { get; }
        public int ColDim { get; }

        private IEnumerable<int> rows;
        private IEnumerable<int> cols;

        public int[] Map { get; }



        public HeightMap(string input)
        {
            var data = heightRegex.Matches(input);

            RowDim = input.Split("\r\n").Count();
            ColDim = input.Split("\r\n")[0].Length;

            Map = data.Select(x => int.Parse(x.Value)).ToArray();
            rows = Enumerable.Range(1, RowDim - 2);
            cols = Enumerable.Range(1, ColDim - 2);
        }


        public int Coords((int row, int col) i)
        {
            return ( i.row*ColDim +  i.col);
        }

        public (int row, int col) Coords(int i)
        {
            return (i/ColDim, i % ColDim);
        }

        public List<(int i, int height)> GetLowPoints()
        {
            List<(int i, int height)> lowPoints = new();

           // first interior points
            foreach(var r in rows)
            {
                foreach(var c in cols)
                {   
                    if ((Map[Coords((r, c))] < Map[Coords((r - 1, c))]) &&
                        (Map[Coords((r, c))] < Map[Coords((r + 1, c))]) &&
                        (Map[Coords((r, c))] < Map[Coords((r, c + 1))]) &&
                        (Map[Coords((r, c))] < Map[Coords((r, c - 1))]))
                    {
                        lowPoints.Add((Coords((r,c)),Map[Coords((r, c))]));
                    }
                }
            }


            // corners (0,0)
            if ((Map[Coords((0, 0))] < Map[Coords((1, 0))]) &&
                (Map[Coords((0, 0))] < Map[Coords((0, 1))]))
            {
                lowPoints.Add((Coords((0, 0)), Map[Coords((0, 0))]));
            }
            // corners (0,ColDim)
            if ((Map[Coords((0, ColDim - 1))] < Map[Coords((1, ColDim - 1))]) &&
                (Map[Coords((0, ColDim - 1))] < Map[Coords((0, ColDim - 2))]))
            {
                lowPoints.Add((Coords((0, ColDim - 1)), Map[Coords((0, ColDim - 1))]));
            }
            // corners (RowDim,0)
            if ((Map[Coords((RowDim - 1, 0))] < Map[Coords((RowDim - 2, 0))]) &&
                (Map[Coords((RowDim - 1, 0))] < Map[Coords((RowDim - 1, 1))]))
            {
                lowPoints.Add((Coords((RowDim - 1, ColDim - 1)), Map[Coords((RowDim - 1, 0))]));
            }
            // corners (RowDim,ColDim)
            if ((Map[Coords((RowDim - 1, ColDim - 1))] < Map[Coords((RowDim - 2, ColDim - 1))]) &&
                (Map[Coords((RowDim - 1, ColDim - 1))] < Map[Coords((RowDim - 1, ColDim - 2))]))
            {
                lowPoints.Add((Coords((RowDim - 1, ColDim - 1)), Map[Coords((RowDim - 1, ColDim - 1))]));
            }


            //upper
            foreach(int c in cols)
            {
                if ((Map[Coords((0, c))] < Map[Coords((0, c - 1))]) &&
                    (Map[Coords((0, c))] < Map[Coords((1, c))])     &&
                    (Map[Coords((0, c))] < Map[Coords((0, c + 1))]))
                {
                    lowPoints.Add((Coords((0, c)), Map[Coords((0, c))]));
                }
            }

            //lower
            foreach (int c in cols)
            {
                if ((Map[Coords((RowDim - 1, c))] < Map[Coords((RowDim - 1, c - 1))]) &&
                    (Map[Coords((RowDim - 1, c))] < Map[Coords((RowDim - 2, c))]) &&
                    (Map[Coords((RowDim - 1, c))] < Map[Coords((RowDim - 1, c + 1))]))
                {
                    lowPoints.Add((Coords((RowDim - 1, c)), Map[Coords((RowDim - 1, c))]));
                }
            }


            //left
            foreach (int r in rows)
            {
                if ((Map[Coords((r, 0))] < Map[Coords((r - 1, 0))]) &&
                    (Map[Coords((r, 0))] < Map[Coords((r, 1))]) &&
                    (Map[Coords((r, 0))] < Map[Coords((r + 1, 0))]))
                {
                    lowPoints.Add((Coords((r, 0)),Map[Coords((r, 0))]));
                }
            }


            //right
            foreach (int r in rows)
            {
                if ((Map[Coords((r, ColDim - 1))] < Map[Coords((r - 1, ColDim - 1))]) &&
                    (Map[Coords((r, ColDim - 1))] < Map[Coords((r, ColDim - 2))]) &&
                    (Map[Coords((r, ColDim - 1))] < Map[Coords((r + 1, ColDim - 1))]))
                {
                    lowPoints.Add((Coords((r, ColDim - 1)), Map[Coords((r, ColDim - 1))]));
                }
            }


            return lowPoints;

        }


    }



}
