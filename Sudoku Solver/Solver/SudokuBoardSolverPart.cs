using Sudoku_Solver.Sudoku;
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
    public partial class SudokuBoard
    {
        public void Solve()
        {
            while (!IsSolved())
            {
                // remove possibilities from other cells
                RemovePossibleNumbers();

                // check if a cell has only one possible number and set it
                FillInCellsWithOnePossibility();

                // check if a possible number is only in one cell

                // recursive algorithm for multiple possibilities
                Display();
                Console.WriteLine();
            }
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
                    IterateBox(row, col, (r, c) => board[r, c].RemovePossibleNumber(currentCell.Value));
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

        bool IsSolved()
        {
            bool solved = true;
            IterateBoard((row, col) =>
            {
                SudokuCell currentCell = board[row, col];

                if (!currentCell.IsFilledIn)
                    solved = false;
            });

            return solved;
        }

        int NumberOfFilledCells()
        {
            int count = 0;

            IterateBoard((row, col) =>
            {
                SudokuCell currentCell = board[row, col];

                if (currentCell.IsFilledIn)
                    count++;
            });

            return count;
        }

        int NumberOfPossibleNumbers()
        {
            int count = 0;

            IterateBoard((row, col) =>
            {
                SudokuCell currentCell = board[row, col];

                count += currentCell.PossibleNumbers.Count;
            });

            return count;
        }

        void IterateBoard(CellAction action)
        {
            for (int row = 0; row < 9; row++)
                for (int col = 0; col < 9; col++)    
                    action(row, col);
        }

        void IterateRow(int startRow, CellAction action)
        {
            int row = startRow;

            for (int col = 0; col < 9; col++)
                action(row, col);
        }

        void IterateColumn(int startCol, CellAction action)
        {
            int col = startCol;

            for (int row = 0; row < 9; row++)
                action(row, col);
        }

        void IterateBox(int startRow, int startCol, CellAction action)
        {
            int rowOffset = startRow - (startRow % 3);
            int colOffset = startCol - (startCol % 3);

            for (int row = 0; row < 3; row++)
                for (int col = 0; col < 3; col++)
                    action(rowOffset + row, colOffset + col);
        }
    }
}
