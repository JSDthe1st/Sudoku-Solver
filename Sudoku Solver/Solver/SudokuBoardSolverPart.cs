using Sudoku_Solver.Sudoku;
using System;
using System.Reflection.Metadata.Ecma335;

namespace Sudoku_Solver
{
    delegate void CellAction(int row, int col);

    public partial class SudokuBoard
    {
        public bool Solve(bool displayProgress = false)
        {
            List<(int, int)> history = new List<(int, int)> ();

            RemoveAllImpossibleNumbers();

            if (FindCellsWithNoPossibility())
                return false;

            while (!IsSolved())
            {
                FillInCellsWithOnePossibility();

                FillInCellsThatHoldsOnlyPossibility();

                if (FindCellsWithNoPossibility())
                    return false;

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
                        var minCellPossibilities = FindCellWithMinPossibilities();
                        foreach (char possibility in board[minCellPossibilities.row, minCellPossibilities.col].PossibleNumbers)
                        {
                            SudokuBoard newBoard = new SudokuBoard(this);
                            newBoard.board[minCellPossibilities.row, minCellPossibilities.col].Value = possibility;
                            bool solved = newBoard.Solve(displayProgress);
                            if (solved)
                            {
                                board = newBoard.board;
                                return true;
                            }
                        }

                        Console.WriteLine("Couldn't solve this sudoku :c");
                        return false;
                    }
                }
            }

            return true;
        }

        bool FindCellsWithNoPossibility()
        {
            bool found = false;

            IterateBoard((row, col) =>
            {
                SudokuCell currentCell = board[row, col];
                if (!currentCell.IsFilledIn && currentCell.PossibleNumbers.Count == 0)
                {
                    found = true;
                    return;
                }
            });

            return found;
        }

        (int row, int col) FindCellWithMinPossibilities()
        {
            (int row, int col) least = (0, 0); // temp value, should get replaced

            IterateBoard((row, col) =>
            {
                if (!board[row, col].IsFilledIn)
                {
                    least = (row, col);
                    return;
                }
            });

            if (board[least.row, least.col].IsFilledIn)
                throw new Exception("No empty cells found in the board.");

            IterateBoard((row, col) =>
            {
                int minCount = board[least.row, least.col].PossibleNumbers.Count;
                int currentCount = board[row, col].PossibleNumbers.Count;
                if (currentCount < minCount && !board[row, col].IsFilledIn)
                    least = (row, col);
            });

            return least;
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
                {
                    currentCell.Value = currentCell.PossibleNumbers[0];
                    RemoveImpossibleNumbers(row, col);
                }
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
                        currentCell.Value = currentPossibleNumber;
                        RemoveImpossibleNumbers(row, col);
                        FillInCellsWithOnePossibility();
                        break;
                    }

                    counter = 0;
                    IterateRow(row, (r, c) =>
                    {
                        if (board[r, c].PossibleNumbers.Contains(currentPossibleNumber))
                            counter++;
                    });
                    if (counter == 1)
                    {
                        currentCell.Value = currentPossibleNumber;
                        RemoveImpossibleNumbers(row, col);
                        FillInCellsWithOnePossibility();
                        break;
                    }

                    counter = 0;
                    IterateColumn(col, (r, c) =>
                    {
                        if (board[r, c].PossibleNumbers.Contains(currentPossibleNumber))
                            counter++;
                    });
                    if (counter == 1)
                    {
                        currentCell.Value = currentPossibleNumber;
                        RemoveImpossibleNumbers(row, col);
                        FillInCellsWithOnePossibility();
                        break;
                    }
                }
            });
        }

        public bool IsSolved()
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
