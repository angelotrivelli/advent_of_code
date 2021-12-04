using AoCHelper;

namespace AdventOfCode2021
{
    public class Day_03 : BaseDay
    {
        private readonly string input;
        private IEnumerable<UInt16> diags;

        public Day_03()
        {
            input = File.ReadAllText(InputFilePath);
            diags = input.Split("\r\n").Select(x => Convert.ToUInt16(x,2));
            // 12 bit binary numbers represented as unsigned int16's
        }

        public override ValueTask<string> Solve_1()
        {
            // int[] tally = {0,0,0,0, 0,0,0,0, 0,0,0,0};
            int[] tally = {0,0,0,0,0};

            foreach(var d in diags)
                for(int i=0 ; i<5 ; i++)
                    tally[i] += (d & (1 << i)) >> i;

            Console.WriteLine("------------");
            foreach(var t in tally){
                Console.Write(t + " ");
            }
            Console.WriteLine();
            Console.WriteLine("------------");

            var numLines = diags.Count();
            var g = tally.Select(x => x>numLines/2 ? 1 : 0).Reverse();
            var e = tally.Select(x => x>numLines/2 ? 0 : 1).Reverse();

            Console.WriteLine($"gamma = {string.Join("",g)}");
            Console.WriteLine($"epsilon = {string.Join("",e)}");

            var gamma = Convert.ToUInt16(string.Join("",g), 2);
            var epsilon = Convert.ToUInt16(string.Join("",e), 2);

            Console.WriteLine($"gamma = {gamma}");
            Console.WriteLine($"epsilon = {epsilon}");

            UInt32 product = ((UInt32)gamma)*((UInt32)epsilon);

            return new ValueTask<string>($"gamma * epsilon = {product}");
        }

        public override ValueTask<string> Solve_2()
        {
            //int[] tally = {0,0,0,0, 0,0,0,0, 0,0,0,0};
            int[] tally = {0,0,0,0,0};

            IEnumerable<UInt16> oxy = diags.ToList();
            IEnumerable<UInt16> co2 = diags.ToList();

            var numLines = diags.Count();

            for(int i=4 ; i>=0 ; i--)
            {

                if (oxy.Sum(x => (x & (1 << i)) >> i) >= oxy.Count()/2)
                {
                    oxy = oxy.Where(x => ((x & (1 << i)) >> i) == 1);
                }
                else
                {
                    oxy = oxy.Where(x => ((x & (1 << i)) >> i) == 0);
                }

                Console.WriteLine($"{i}--------- oxy count = {oxy.Count()} ");
                foreach(var o in oxy)
                {
                    Console.WriteLine(Convert.ToString(o,2).PadLeft(5,'0'));
                }
                Console.WriteLine("-----------");
            }



            return new ValueTask<string>("");
        }
    }

}
