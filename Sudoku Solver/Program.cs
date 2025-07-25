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
            SudokuSolver solver = new SudokuSolver();
            solver.LoadNumbers(@"D:\Projects\TO DO\Sudoku Solver\example_board.txt");
            solver.Display();
        } 
    }
}
