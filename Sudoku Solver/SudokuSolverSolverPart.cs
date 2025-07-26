using System;
using System.Collections.Generic;
using System.Data;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver
{
    delegate void CellAction(int row, int col);
    public partial class SudokuSolver
    {
        public void Solve()
        {
            // remove possibilities from other cells
            RemovePossibleNumbers();

            // check if a cell has only one possible number and set it
            FillInCellsWithOnePossibility();

            // check if a possible number is only in one cell



        }

        void RemovePossibleNumbers()
        {
            IterateBoard((row, col) =>
            {
                SudokuCell currentCell = board[row, col];

                if (currentCell.IsFilledIn)
                {
                    IterateRow(row, (r, c) => board[r, c].RemovePossibleNumber(currentCell.Value));
                    IterateColumn(col, (r, c) => board[r, c].RemovePossibleNumber(currentCell.Value));
                    iterateBox(row, col, (r, c) => board[r, c].RemovePossibleNumber(currentCell.Value));
                }
            });
        }

        void FillInCellsWithOnePossibility()
        {
            IterateBoard((row, col) =>
            {
                SudokuCell currentCell = board[row, col];

                if (!currentCell.IsFilledIn && currentCell.PossibleNumbers.Count == 1)
                    currentCell.Value = currentCell.PossibleNumbers[0];
            });
        }

        void IterateBoard(CellAction action)
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    action(row, col);
                }
            }
        }

        void IterateRow(int startRow, CellAction action)
        {
            int row = startRow;
            for (int col = 0; col < 9; col++)
            {
                action(row, col);
            }
        }

        void IterateColumn(int startCol, CellAction action)
        {
            int col = startCol;
            for (int row = 0; row < 9; row++)
            {
                action(row, col);
            }
        }

        void iterateBox(int startRow, int startCol, CellAction action)
        {
            int rowOffset = startRow - (startRow % 3);
            int colOffset = startCol - (startCol % 3);

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    action(rowOffset + row, colOffset + col);
                }
            }
        }
    }
}
