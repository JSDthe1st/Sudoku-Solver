using Sudoku_Solver.Sudoku;

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
                RemovePossibleNumbers();

                // check if a cell has only one possible number and set it
                FillInCellsWithOnePossibility();

                // check if a possible number is only in one cell
                FillInCellsThatHoldOnlyPossibility();

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

        void FillInCellsThatHoldOnlyPossibility()
        {
            throw new NotImplementedException();
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

                IterateRow(row, (row, c) => 
                { 
                    if (board[row, c].Value == currentCell.Value) 
                    { 
                        isCorrect = false;
                        if (conflictMesseges)
                            Console.WriteLine($"Row conflict at x:{row} y:{col}");
                    }
                });

                IterateColumn(col, (row, c) => 
                { 
                    if (board[row, c].Value == currentCell.Value)
                    {
                        isCorrect = false;
                        if (conflictMesseges)
                            Console.WriteLine($"Column conflict at x:{row} y:{col}");
                    }
                });

                IterateBox(row, col, (row, c) => 
                { 
                    if (board[row, c].Value == currentCell.Value)
                    {
                        isCorrect = false;
                        if (conflictMesseges)
                            Console.WriteLine($"Box conflict at x:{row} y:{col}");
                    }
                });
            });

            if (isCorrect) 
                Console.WriteLine("No conflicts founs.");
            
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
