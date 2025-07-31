using Sudoku_Solver.Sudoku;
using System;
using System.Reflection.Metadata.Ecma335;

namespace Sudoku_Solver
{
    delegate void CellAction(int row, int col);
    public partial class SudokuBoard
    {
        public void Solve(bool displayProgress = false)
        {
            List<(int, int)> history = new List<(int, int)> ();

            while (!IsSolved())
            {
                // remove possibilities from other cells
                RemoveAllImpossibleNumbers();

                // check if a cell has only one possible number and set it
                FillInCellsWithOnePossibility();

                // check if a possible number is only in one cell
                FillInCellsThatHoldsOnlyPossibility();

                // recursive algorithm for multiple possibilities
                
                if (displayProgress)
                {
                    Display();
                    Console.WriteLine();
                }

                //tmeout
                history.Add((NumberOfFilledCells(), NumberOfPossibleNumbers()));
                if (history.Count > 3)
                {
                    history.RemoveAt(0);

                    if (history[0] == history[1] &&
                        history[0] == history[2])
                    {
                        Console.WriteLine("Timedout!");
                        break;
                    }
                }

                return;
            }
        }

        void RemoveAllImpossibleNumbers()
        {
            IterateBoard((row, col) =>
            {
                RemoveImpossibleNumbers(row, col);
            });
        }

        void RemoveImpossibleNumbers(int row, int col)
        {
            SudokuCell currentCell = board[row, col];

            CellAction removePossibleNumber = (r, c) => board[r, c].RemovePossibleNumber(currentCell.Value);

            if (currentCell.IsFilledIn)
            {
                IterateRow(row, removePossibleNumber);
                IterateColumn(col, removePossibleNumber);
                IterateBox(row, col, removePossibleNumber);
            }
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

        void FillInCellsThatHoldsOnlyPossibility()
        {
            IterateBoard((row, col) =>
            {
                SudokuCell currentCell = board[row, col];

                for (int i = 0; i < currentCell.PossibleNumbers.Count; i++)
                {
                    char currentPossibleNumber = currentCell.PossibleNumbers[i];

                    int counter = 0;
                    IterateBox(row, col, (r, c) =>
                    {
                        if (board[r, c].PossibleNumbers.Contains(currentPossibleNumber))
                            counter++;
                    });
                    if (counter == 1)
                    {
                        {
                            currentCell.Value = currentPossibleNumber;
                            RemoveAllImpossibleNumbers();
                            FillInCellsWithOnePossibility();
                            break;
                        }
                    }

                }
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

        public bool IsCorrect(bool conflictMesseges = true)
        {
            bool isCorrect = true;

            IterateBoard((row, col) =>
            {
                SudokuCell currentCell = board[row, col];

                string messageStart = null;
                CellAction action = (r, c) =>
                {
                    if (board[r, c].Value == currentCell.Value && (row, col) != (r, c) && currentCell.Value != '-')
                    {
                        isCorrect = false;
                        if (conflictMesseges)
                            Console.WriteLine(messageStart + $" conflict at x:{col} y:{row}");
                    }
                };

                messageStart = "Row"; 
                IterateRow(row, action);

                messageStart = "Column";
                IterateColumn(col, action);

                messageStart = "Box";
                IterateBox(row, col, action);
            });

            if (isCorrect) 
                Console.WriteLine("No conflicts found.");
            
            return isCorrect;
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
