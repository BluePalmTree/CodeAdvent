using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTests
{
    public class BingoBoard
    {
        public bint[,] board = new bint[5, 5];
        public bool HasWon = false;
        public int Number;
    }

    [System.Diagnostics.DebuggerDisplay("{Number}, {Marked}")]
    public struct bint
    {
        public bint(int number)
        {
            Number = number;
            Marked = false;
        }

        public int Number;
        public bool Marked;

        public override string ToString()
        {
            return $"{this.Number} {(this.Marked ? "*" : "")}";
        }
    }

    public class Day4
    {
        public static void Day4_1()
        {
            string[] inputs = System.IO.File.ReadAllLines(@"C:\Examples\CodeAdvent\Inputs\2021\Day4.txt");
            int[] numbers = Array.ConvertAll(inputs[0].Split(','), s => int.Parse(s));

            List<BingoBoard> bingoBoards = new List<BingoBoard>();
            BingoBoard curBoard = new BingoBoard();
            int countBoards = 0;
            int countRows = 0;

            for (int i = 2; i < inputs.Length; i++)
            {
                int[] boardNumbers = Array.ConvertAll(inputs[i].Split(' ',StringSplitOptions.RemoveEmptyEntries), s => int.Parse(s));
                for (int n = 0; n < boardNumbers.Length; n++)
                {
                    curBoard.board[countRows, n] = new bint( boardNumbers[n]);
                }
                countRows++;

                if (inputs[i] == "" || i == inputs.Length - 1)
                {
                    countRows = 0;
                    bingoBoards.Add(curBoard);
                    curBoard = new BingoBoard();
                    continue;
                }
            }
                 
            BingoBoard winBoard = null;
            int lastNumber = 0;

            foreach (int number in numbers)
            {
                Console.WriteLine($"-----{number}-------");

                foreach(var board in bingoBoards)
                {
                    int boundR = board.board.GetUpperBound(0);
                    int boundC = board.board.GetUpperBound(1);

                    int markedR = 0;
                    int markedC = 0;

                    for (int r = 0; r <= boundR; r++)
                    {
                        for (int c = 0; c <= boundC; c++)
                        {
                            if (board.board[r, c].Number == number)
                            {
                                board.board[r, c].Marked = true;                                
                            }
                        }
                    }

                    if (CheckBoard(board))
                    {
                        winBoard = board;
                        break;
                    }
                }

                PrintBoards(bingoBoards);

                if (winBoard != null)
                {
                    lastNumber = number;
                    break;
                }
            }

            Console.WriteLine("we have a winner" + Environment.NewLine);
            PrintBoards(new List<BingoBoard>() { winBoard });
            int sumUnchecked = GetSumUncheckedBoard(winBoard);
            Console.WriteLine($"Sum unchecked: {sumUnchecked}");
            Console.WriteLine($"Result: {sumUnchecked} * {lastNumber} = {sumUnchecked * lastNumber}");
        }

        public static void Day4_2()
        {
            string[] inputs = System.IO.File.ReadAllLines(@"C:\Examples\CodeAdvent\Inputs\2021\Day4.txt");
            int[] numbers = Array.ConvertAll(inputs[0].Split(','), s => int.Parse(s));

            List<BingoBoard> bingoBoards = new List<BingoBoard>();
            BingoBoard curBoard = new BingoBoard();            
            int countRows = 0;
            int boardCounter = 1;

            for (int i = 2; i < inputs.Length; i++)
            {
                int[] boardNumbers = Array.ConvertAll(inputs[i].Split(' ', StringSplitOptions.RemoveEmptyEntries), s => int.Parse(s));
                for (int n = 0; n < boardNumbers.Length; n++)
                {
                    curBoard.board[countRows, n] = new bint(boardNumbers[n]);
                }
                countRows++;

                if (inputs[i] == "" || i == inputs.Length - 1)
                {
                    countRows = 0;
                    boardCounter++;
                    bingoBoards.Add(curBoard);
                    curBoard = new BingoBoard();
                    curBoard.Number = boardCounter;
                    continue;
                }
            }

            BingoBoard winBoard = null;
            int lastNumber = 0;

            for (int nu = 0; nu < numbers.Length; nu++)
            {
                int number = numbers[nu];

                Console.WriteLine($"-----{number}-------");

                foreach (var board in bingoBoards)
                {
                    if (board.HasWon) continue;

                    int boundR = board.board.GetUpperBound(0);
                    int boundC = board.board.GetUpperBound(1);

                    for (int r = 0; r <= boundR; r++)
                    {
                        for (int c = 0; c <= boundC; c++)
                        {
                            if (board.board[r, c].Number == number)
                            {
                                board.board[r, c].Marked = true;
                            }
                        }
                    }

                    if (CheckBoard(board))
                    {                        
                        board.HasWon = true;

                        if (bingoBoards.All(bb => bb.HasWon))
                        {
                            winBoard = board;
                            break;
                        }
                    }
                }

                PrintBoards(bingoBoards);

                if (winBoard != null)
                {
                    lastNumber = number;
                    break;
                }
            }

            Console.WriteLine("we have a winner" + Environment.NewLine);
            PrintBoards(new List<BingoBoard>() { winBoard });
            int sumUnchecked = GetSumUncheckedBoard(winBoard);
            Console.WriteLine($"Sum unchecked: {sumUnchecked}");
            Console.WriteLine($"Result: {sumUnchecked} * {lastNumber} = {sumUnchecked * lastNumber}");
        }

        public static int GetSumUncheckedBoard(BingoBoard board)
        {
            var sum = 0;

            int boundR = board.board.GetUpperBound(0);
            int boundC = board.board.GetUpperBound(1);
           

            for (int r = 0; r <= boundR; r++)
            {                
                for (int c = 0; c <= boundC; c++)
                {
                    if (board.board[r,c].Marked == false)
                    {
                        sum += board.board[r, c].Number;
                    }
                }
            }

            return sum;
        }

        public static bool CheckBoard(BingoBoard board)
        {
            int boundR = board.board.GetUpperBound(0);
            int boundC = board.board.GetUpperBound(1);

            int markedR = 0;
            int markedC = 0;
            List < bint > bnChecked = new List<bint>();

            for (int r = 0; r <= boundR; r++)
            {
                markedC = 0;
                bnChecked = new List<bint>();
                for (int c = 0; c <= boundC; c++)
                {
                    var bn = board.board[r, c];
                    bnChecked.Add(bn);
                    if (bn.Marked)
                    {
                        markedC++;
                    }
                }

                if (bnChecked.All(b => b.Marked))
                {
                    return true;
                }
            }

            for (int r = 0; r <= boundR; r++)
            {
                markedC = 0;
                bnChecked = new List<bint>();
                for (int c = 0; c <= boundC; c++)
                {
                    var bn = board.board[c, r];
                    bnChecked.Add(bn);
                    if (bn.Marked)
                    {
                        markedC++;
                    }
                }

                if (bnChecked.All(b => b.Marked))
                {
                    return true;
                }
            }

            return false;
        }

        public static void PrintBoards(List<BingoBoard> boards)
        {
            foreach (var board in boards)
            {
                int boundR = board.board.GetUpperBound(0);
                int boundC = board.board.GetUpperBound(1);

                for (int r = 0; r <= boundR; r++)
                {
                    for (int c = 0; c <= boundC; c++)
                    {
                        if (board.board[r, c].Marked)
                            Console.Write(board.board[r, c].Number + "* ");
                        else
                            Console.Write(board.board[r, c].Number + " ");
                    }

                    Console.WriteLine("");
                }

                Console.WriteLine("");
            }
        }
    }
}
