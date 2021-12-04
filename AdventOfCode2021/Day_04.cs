using AoCHelper;
using System.Text.RegularExpressions;

namespace AdventOfCode2021
{
    public class Day_04 : BaseDay
    {
        private readonly Regex drawRegex = new(@"(\d+,)+\d+");
        private readonly Regex numberRegex = new(@"\d+");

        private readonly IEnumerable<int> draws;
        private readonly IEnumerable<int> boardNums;

        const int boardSize = 5;
        const int numSquares = boardSize * boardSize;

        private List<Board> boards = new();
        List<(int draw, Board board)> winningBoards = new();

        public Day_04()
        {
            var input = File.ReadAllText(InputFilePath);

            var drawText = drawRegex.Match(input);
            var boardText = input.Substring(drawText.Index + drawText.Length);

            draws = drawText.Value.Split(",").Select(x => int.Parse(x));
            boardNums = numberRegex.Matches(boardText).Select(m => int.Parse(m.Value));
        }


        public override ValueTask<string> Solve_1()
        {
            var numBoards = boardNums.Count() / numSquares;

            for (int n = 0; n < numBoards; n++)
            {
                boards.Add(new Board(n, boardSize, boardNums.Skip(n * numSquares).Take(numSquares)));
            }

            foreach (var d in draws)
            {
                foreach (var b in boards)
                {
                    if (b.Draw(d))
                    {
                        winningBoards.Add((d, b));
                    }
                }
                boards = boards.Where(x => !x.Bingo).ToList(); // only keep unfinished boards
            }

            var first = winningBoards.First();

            return new ValueTask<string>($"Bingo!, Board {first.board.Id}, draw={first.draw} score={first.board.Score}, score*draw={first.board.Score * first.draw}");
        }


        public override ValueTask<string> Solve_2()
        {
            var last = winningBoards.Last();
            return new ValueTask<string>($"Last Bingo!, Board {last.board.Id}, draw={last.draw} score={last.board.Score}, score*draw={last.board.Score * last.draw}");
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
        private bool bingo;
        private int score;

        public int Id { get; }
        public IList<Square> Squares { get; }
        public bool Bingo => bingo;
        public int Score => score;

        public Board(int id, int boardSize, IEnumerable<int> numbers)
        {
            Id = id;
            Squares = new List<Square>();
            bingo = false;
            score = 0;

            int k = 0;
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
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
