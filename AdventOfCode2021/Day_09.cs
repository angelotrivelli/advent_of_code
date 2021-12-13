using AoCHelper;
using System.Text.RegularExpressions;

namespace AdventOfCode2021
{

    public class Day_09 : BaseDay
    {
        private static bool IsExample = true;
        private readonly string input;

        

        public Day_09()
        {
            input = IsExample
                        ? Example09.Input
                        : File.ReadAllText(InputFilePath);


        }


        public override ValueTask<string> Solve_1()
        {


            return new ValueTask<string>($"{rows},{cols}");
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

            RowDim = data.Count();
            ColDim = data[0].Value.Length;

            Map = data.Select(x => int.Parse(x.Value)).ToArray();
            rows = Enumerable.Range(1, RowDim - 1);
            cols = Enumerable.Range(1, ColDim - 2);
        }


        private int GetIndex(int row, int col)
        {
            return (RowDim * (row / RowDim) +  ColDim);
        }

        public List<int> GetLowPoints()
        {

           // first interior points

            foreach(var r in rows)
            {
                foreach(var c in cols)
                {

                }
            }


        }


    }



}
