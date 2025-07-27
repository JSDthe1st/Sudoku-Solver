
using Sudoku_Solver.Sudoku;

namespace Sudoku_Solver
{
    public partial class SudokuBoard
    {
        SudokuCell[,] board = new SudokuCell[9,9];

        public SudokuBoard()
        {

        }
        
        public SudokuBoard(SudokuCell[,] board)
            : this()
        {
            this.board = board;
        }

        public void LoadNumbers(SudokuCell[,] board)
        {
            this.board = board;
        }

        public void LoadNumbers(string path)
        {
            string raw = File.ReadAllText(path);
            raw = raw.Trim();
            string[] rows = raw.Split('\n');

            for (int i = 0; i < 9; i++)
            {
                string[] rowElements = rows[i].Split(' ');

                for (int j = 0; j < 9; j++)
                {
                    board[i, j] = new SudokuCell(rowElements[j]);
                }
            }
        }

        public void Display()
        {
            if (board is null)
                return;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(board[i, j] + " ");

                    if (j == 2 || j == 5)
                        Console.Write("| ");
                }
                Console.WriteLine();

                if (i == 2 || i == 5)
                    Console.WriteLine("------+-------+------");
            }
        }
    }
}