using System.Drawing;

namespace Sudoku_Solver
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            //SudokuBoard solver = new SudokuBoard();
            //solver.LoadNumbers(@"D:\Projects\TO DO\Sudoku Solver\example_board.txt");
            //solver.Display();

            BoardReader.SplitImageIntoCells();
        }
    }
}
