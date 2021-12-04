using AoCHelper;
using System.Text.RegularExpressions;

namespace AdventOfCode2021
{
    public class Day_04 : BaseDay
    {
        private readonly Regex DrawRegex = new(@"(\d+,)+\d+");
        private readonly Regex NumberRegex = new Regex(@"\d+");
        private readonly IEnumerable<int> draws;
        private readonly IEnumerable<int> allNumbers;


        public Day_04()
        {
            var input = File.ReadAllText(InputFilePath);

            var drawMatch = DrawRegex.Match(input);
            var boardStart = drawMatch.Index + drawMatch.Length;
            var boardNums = input.Substring(boardStart);

            draws = drawMatch.Value.Split(",").Select(x => int.Parse(x));
            allNumbers = NumberRegex.Matches(boardNums).Select(m => int.Parse(m.Value));
        }

        public override ValueTask<string> Solve_1()
        {
            var numSquaresPerBoard = Board.size * Board.size;
            var numBoards = allNumbers.Count() / numSquaresPerBoard;

            List<Board> boards = new();

            for (int n=0; n<numBoards; n++)
            {
                boards.Add(new Board(n, allNumbers.Skip(n * numSquaresPerBoard).Take(numSquaresPerBoard)));
            }

            foreach(var d in draws)
            {
                foreach (var b in boards)
                {
                    if (b.Draw(d))
                    {
                        return new ValueTask<string>($"Bingo! Board {b.Id}, draw={d} score={b.Score}, score*draw = {b.Score * d}");
                    }
                }
            }



            return new ValueTask<string>("");
        }

        public override ValueTask<string> Solve_2()
        {
            var numSquaresPerBoard = Board.size * Board.size;
            var numBoards = allNumbers.Count() / numSquaresPerBoard;

            List<Board> boards = new();

            for (int n = 0; n < numBoards; n++)
            {
                boards.Add(new Board(n, allNumbers.Skip(n * numSquaresPerBoard).Take(numSquaresPerBoard)));
            }

            List<(int id, int draw, int score)> winningBoards = new();
            foreach (var d in draws)
            {
                foreach (var b in boards)
                {
                    if (b.Draw(d))
                    {
                        winningBoards.Add(new (b.Id, d, b.Score));
                    }
                }
                boards = boards.Where(x => !x.Bingo).ToList(); // remove finished boards
            }

            var last = winningBoards.Last();
            return new ValueTask<string>($"Last Bingo!, Board {last.id}, draw={last.draw} score={last.score}, score*draw={last.score * last.draw}");
        }
    }


    internal class Square
    {
        public int Number { get; }

        public int Row { get; }
        public int Col { get; }
        public bool IsMarked { get; set; }

        public Square(int number, int row, int col)
        {
            Number = number;
            Row = row;
            Col = col;
            IsMarked = false;
        }
    }


    internal class Board
    {
        public static int size = 5;
        private bool bingo;
        private int score;

        public int Id { get; }
        public IList<Square> Squares { get; }
        public bool Bingo => bingo;
        public int Score => score;

        public Board(int id, IEnumerable<int> numbers)
        {
            if (numbers.Count() != size * size)
                throw new InvalidOperationException($"need {size * size} numbers for a board!");

            Id = id;
            Squares = new List<Square>();
            bingo = false;
            score = 0;

            int k=0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Squares.Add(new Square(numbers.Skip(k++).Take(1).First(), i, j));
                }
            }         
        }

        public bool Draw(int number)
        {
            var matches = Squares.Where(s => s.Number == number);

            foreach (var match in matches)
            {
                match.IsMarked = true;
                
                if (Squares.Where(s => s.Row == match.Row).All(s => s.IsMarked) ||
                    Squares.Where(s => s.Col == match.Col).All(s => s.IsMarked))
                {
                    bingo = true;
                    score = Squares.Where(s => s.IsMarked == false).Sum(s => s.Number);
                }
            }
            return bingo;
        }
    }

}
