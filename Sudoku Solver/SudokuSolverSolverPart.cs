using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver
{
    delegate void CellAction(int startRow, int startCol, int row, int col);
    public partial class SudokuSolver
    {
        public void Solve()
        {
            // remove possibilities from other cells
            // check if it is the only possibility

            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    SudokuCell currentCell = board[row, col];
                    if (currentCell.IsSet)
                        iterateRow(row, col, (startRow, startCol, r, c) => board[r, c].RemovePossibility(currentCell.Value));
                }
            }
            
        }

        void iterateRow(int startRow, int startCol, CellAction action)
        {
            int r = startRow;
            for (int c = 0; c < 9; c++)
            {
                action(startRow, startCol, r, c);
            }
        }

        void iterateColumn(int startRow, int startCol, CellAction action)
        {
            int c = startCol;
            for (int r = 0; r < 9; r++)
            {
                action(startRow, startCol, r, c);
            }
        }

        void iterateBox(int startRow, int startCol, CellAction action)
        {

        }
    }
}
