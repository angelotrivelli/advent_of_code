using System.Text.RegularExpressions;
using AoCHelper;
using System.Text.RegularExpressions;

namespace AdventOfCode2021
{
    public class Day_08 : BaseDay
    {
        private bool IsExample = false;
        private readonly string input;

        private readonly Regex digitsRegex = new(@"^[\w ]+(?= \| )",RegexOptions.Multiline);
        private readonly Regex displayRegex = new(@"(?<= \| )[\w ]+");
        public Day_08()
        {
            input = IsExample
                ? Example08.Input
                : File.ReadAllText(InputFilePath);




        }


        public override ValueTask<string> Solve_1()
        {
            var displays = displayRegex.Matches(input)
                           .SelectMany(m => m.Value.Trim().Split(" "))
                           .ToList();

            // count the number of instances of 1, 4, 7, 8 in displays
            var barCount = displays.Where(d => d.Length==2 || d.Length==4 || d.Length==3 || d.Length==7);
            return new ValueTask<string>($"total number of 1's, 4's, 7's and 8's = {barCount.Count()}");
        }


        public override ValueTask<string> Solve_2()
        {
            var scrambledMessage = displayRegex.Matches(input)
                                            .Select(m => m.Value.Trim())
                                            .ToList();

            var digitsInput = digitsRegex.Matches(input)
                                         .Select(m => m.Value.Trim().Split(" "))
                                         .ToList();



            // each line is a distinct scrambling of digits with a distinct message.
            int messageSum = 0;
            for(int i = 0; i < scrambledMessage.Count; i++)
            {
                messageSum += Unscramble(GetBits(digitsInput[i]), scrambledMessage[i]);
                // Console.WriteLine($"{scrambledMessage[i]}: {message}");
            }

            return new ValueTask<string>($"sum of message = {messageSum}");

        }








        public List<(string signals, uint bits, int len)> GetBits (string[] line)
        {
            var pv = "abcdefg".ToCharArray().Reverse();
            List<(string signals, uint bits, int len)> t = new();
            foreach(var d in line)
            {
                uint digit = 0;
                int i = 0;
                foreach(var p in pv)
                {
                    digit |= (d.ToCharArray().Contains(p) ? (uint)(1 << i) : (uint)0);
                    i++;
                }
                t.Add((string.Join("",d.ToCharArray().OrderBy(x =>x)),digit,d.Length));
            }
            return t;
        }




        private int Unscramble(List<(string signals, uint bits, int len)> digits, string message)
        {
            // (string signals, uint bits, int len) digit_0;
            (string signals, uint bits, int len) digit_1 = digits.First(x => x.len == 2);
            // (string signals, uint bits, int len) digit_2;
            // (string signals, uint bits, int len) digit_3;
            (string signals, uint bits, int len) digit_4 = digits.First(x => x.len == 4);
            // (string signals, uint bits, int len) digit_5;
            // (string signals, uint bits, int len) digit_6;
            (string signals, uint bits, int len) digit_7 = digits.First(x => x.len == 3);
            (string signals, uint bits, int len) digit_8 = digits.First(x => x.len == 7);
            // (string signals, uint bits, int len) digit_9;


            // Use some relations to determine other chars.


            // Collect the 5-segment digits, there are only 3.
            var o5 = digits.Where(x => x.len == 5);

            // digit_4 OR digit_2 is digit_8, gives us digit_2
            var digit_2 = o5.First(x => (x.bits | digit_4.bits) == digit_8.bits);

            // digit_1 AND digit_3 is digit_1, gives us digit_3
            var digit_3 = o5.First(x => (x.bits & digit_1.bits) == digit_1.bits);

            // the remaining 5-segment digit must then be digit_5
            var digit_5 = o5.First(x => (x != digit_2) && (x != digit_3));


            // Collect the 6-segment digits, there are only 3
            var o6 = digits.Where(x => x.len == 6);

            // (4 or 0 = 8, gives digit_0)
            var digit_0 = o6.First(x => ((x.bits & digit_4.bits) != digit_4.bits) && ((x.bits & digit_7.bits) == digit_7.bits));

            // (1 OR 6 = 8, gives digit_6)
            var digit_6 = o6.First(x => (x.bits | digit_1.bits) == digit_8.bits);

            // (4 OR 9 = 9) gives digit_9
            var digit_9 = o6.First(x => (x.bits & digit_4.bits) == digit_4.bits);

            // Console.WriteLine($"0 = {digit_0.signals.PadLeft(7,' ')} [{Convert.ToString(digit_0.bits, 2).PadLeft(7, '0')}] {digit_0.bits} ");
            // Console.WriteLine($"1 = {digit_1.signals.PadLeft(7,' ')} [{Convert.ToString(digit_1.bits, 2).PadLeft(7, '0')}] {digit_1.bits} *");
            // Console.WriteLine($"2 = {digit_2.signals.PadLeft(7,' ')} [{Convert.ToString(digit_2.bits, 2).PadLeft(7, '0')}] {digit_2.bits} ");
            // Console.WriteLine($"3 = {digit_3.signals.PadLeft(7,' ')} [{Convert.ToString(digit_3.bits, 2).PadLeft(7, '0')}] {digit_3.bits} ");
            // Console.WriteLine($"4 = {digit_4.signals.PadLeft(7,' ')} [{Convert.ToString(digit_4.bits, 2).PadLeft(7, '0')}] {digit_4.bits} *");
            // Console.WriteLine($"5 = {digit_5.signals.PadLeft(7,' ')} [{Convert.ToString(digit_5.bits, 2).PadLeft(7, '0')}] {digit_5.bits} ");
            // Console.WriteLine($"6 = {digit_6.signals.PadLeft(7,' ')} [{Convert.ToString(digit_6.bits, 2).PadLeft(7, '0')}] {digit_6.bits} ");
            // Console.WriteLine($"7 = {digit_7.signals.PadLeft(7,' ')} [{Convert.ToString(digit_7.bits, 2).PadLeft(7, '0')}] {digit_7.bits} ");
            // Console.WriteLine($"8 = {digit_8.signals.PadLeft(7,' ')} [{Convert.ToString(digit_8.bits, 2).PadLeft(7, '0')}] {digit_8.bits} *");
            // Console.WriteLine($"9 = {digit_9.signals.PadLeft(7,' ')} [{Convert.ToString(digit_9.bits, 2).PadLeft(7, '0')}] {digit_9.bits} ");
            // Console.WriteLine();
            // Console.WriteLine($"0 = {digits[9].signals.PadLeft(7, ' ')} [{Convert.ToString(digits[9].bits, 2).PadLeft(7, '0')}] {digits[9].bits} ");
            // Console.WriteLine($"1 = {digits[8].signals.PadLeft(7, ' ')} [{Convert.ToString(digits[8].bits, 2).PadLeft(7, '0')}] {digits[8].bits} *");
            // Console.WriteLine($"2 = {digits[7].signals.PadLeft(7, ' ')} [{Convert.ToString(digits[7].bits, 2).PadLeft(7, '0')}] {digits[7].bits} ");
            // Console.WriteLine($"3 = {digits[6].signals.PadLeft(7, ' ')} [{Convert.ToString(digits[6].bits, 2).PadLeft(7, '0')}] {digits[6].bits} ");
            // Console.WriteLine($"4 = {digits[5].signals.PadLeft(7, ' ')} [{Convert.ToString(digits[5].bits, 2).PadLeft(7, '0')}] {digits[5].bits} *");
            // Console.WriteLine($"5 = {digits[4].signals.PadLeft(7, ' ')} [{Convert.ToString(digits[4].bits, 2).PadLeft(7, '0')}] {digits[4].bits} ");
            // Console.WriteLine($"6 = {digits[3].signals.PadLeft(7, ' ')} [{Convert.ToString(digits[3].bits, 2).PadLeft(7, '0')}] {digits[3].bits} ");
            // Console.WriteLine($"7 = {digits[2].signals.PadLeft(7, ' ')} [{Convert.ToString(digits[2].bits, 2).PadLeft(7, '0')}] {digits[2].bits} ");
            // Console.WriteLine($"8 = {digits[1].signals.PadLeft(7, ' ')} [{Convert.ToString(digits[1].bits, 2).PadLeft(7, '0')}] {digits[1].bits} *");
            // Console.WriteLine($"9 = {digits[0].signals.PadLeft(7, ' ')} [{Convert.ToString(digits[0].bits, 2).PadLeft(7, '0')}] {digits[0].bits} ");





            var converter = new Dictionary<string, string>() 
            {
                { digit_0.signals, "0" },
                { digit_1.signals, "1" },
                { digit_2.signals, "2" },
                { digit_3.signals, "3" },
                { digit_4.signals, "4" },
                { digit_5.signals, "5" },
                { digit_6.signals, "6" },
                { digit_7.signals, "7" },
                { digit_8.signals, "8" },
                { digit_9.signals, "9" }

            };

            string m = "";
            foreach (var d in message.Split(" "))
            {
                var x = string.Concat(d.ToCharArray().OrderBy(x => x));
                m += converter[x];
            }

            // Console.WriteLine("-------------");
            // Console.WriteLine($"{string.Join(" ",digits.Select(x => x.signals))}");
            // Console.WriteLine(message);
            // Console.WriteLine(m);
            // Console.WriteLine("-------------");


            return int.Parse(m);
        }
    }
}
