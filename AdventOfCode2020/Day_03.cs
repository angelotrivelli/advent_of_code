using AoCHelper;

namespace AdventOfCode2020
{
    public class Day_03 : BaseDay
    {
        private bool IsExample = false;
        private List<string> field;

        public Day_03()
        {
            var input = IsExample 
                ? Example.Input 
                : File.ReadAllText(InputFilePath);

            field = input.Split("\r\n").ToList();
        }

        public override ValueTask<string> Solve_1()
        {
            var path = TraversePath(3,1);
            return new ValueTask<string>($"path = {path}, numTrees={path.ToCharArray().Count(c => c.Equals('#'))}");
        }


        public override ValueTask<string> Solve_2()
        {
            var paths = new List<string>
            {
                TraversePath(1, 1),
                TraversePath(3, 1),
                TraversePath(5, 1),
                TraversePath(7, 1),
                TraversePath(1, 2)
            };

            long treeProduct = paths
                                .Select(p => p.ToCharArray()
                                .Count(c => c.Equals('#')))
                                .Aggregate(1L, (x, y) => x * y);

            return new ValueTask<string>($"tree product = {treeProduct}");
        }


        // recursively determine what was encountered in path
        private string TraversePath(int dx, int dy, int x = 0, int y = 0, string path="")
        {
            if (y >= field.Count())
            {
                return path;
            }

            path += field[y].Substring(x % field[y].Length, 1);
            x += dx;
            y += dy;

            return TraversePath(dx,dy,x,y,path);
        }
    }

    public static class Example
    {
        public static string Input =  "..##......." + "\r\n" +
                                      "#...#...#.." + "\r\n" +
                                      ".#....#..#." + "\r\n" +
                                      "..#.#...#.#" + "\r\n" +
                                      ".#...##..#." + "\r\n" +
                                      "..#.##....." + "\r\n" +
                                      ".#.#.#....#" + "\r\n" +
                                      ".#........#" + "\r\n" +
                                      "#.##...#..." + "\r\n" +
                                      "#...##....#" + "\r\n" +
                                      ".#..#...#.#";
    }
}
