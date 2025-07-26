using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sudoku_Solver
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            SudokuBoard board = new SudokuBoard();
            board.LoadNumbers(@"D:\Projects\TO DO\Sudoku Solver\example_board2.txt");

            board.Solve();
            board.Display();

            // add ocr from last screenshot from screenshot folder
            // add keyboard controll for automatic solving
        }
    }
}
