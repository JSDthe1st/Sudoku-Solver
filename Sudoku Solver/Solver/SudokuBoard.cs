using Sudoku_Solver.Sudoku;

namespace Sudoku_Solver
{
    public partial class SudokuBoard
    {
        SudokuCell[,] board;
        public SudokuCell[,] Board { get => board; }

        public SudokuBoard()
        {
            board = new SudokuCell[9, 9];
        }
        
        public SudokuBoard(SudokuCell[,] otherBoard)
            : this()
        {
            LoadNumbers(otherBoard);
        }

        public SudokuBoard(SudokuBoard otherSucokuBoard)
             : this()
        {
            LoadNumbers(otherSucokuBoard.board);
        }

        public void LoadNumbers(SudokuCell[,] otherBoard)
        {
            IterateBoard((r, c) =>
            {
                board[r, c] = new SudokuCell(otherBoard[r, c]);
            });
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

            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    Console.Write(board[row, col] + " ");

                    if (col == 2 || col == 5)
                        Console.Write("| ");
                }
                Console.WriteLine();

                if (row == 2 || row == 5)
                    Console.WriteLine("------+-------+------");
            }
        }
    }
}