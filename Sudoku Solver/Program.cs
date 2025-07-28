using Sudoku_Solver.Reader;
using Sudoku_Solver.Sudoku;
using System.Drawing;

namespace Sudoku_Solver
{
    internal class Program
    {
        public static void Main(string[] args)
        {

            string screenshotPaht = ScreenshotHandler.GetLastScreenshotPath();
            List<Image> cellImages = ScreenshotHandler.SplitImageIntoCells(screenshotPaht, 9);
            string cellsFolder = "Cells";
            ScreenshotHandler.Save(cellImages, cellsFolder);

            var listOfCells = Directory.GetFiles(cellsFolder);

            SudokuCell[,] cells = new SudokuCell[9,9];
            OCR ocr = new OCR();
            for (int i = 0; i < 81; i++)
            {
                string value = "-";
                value = ocr.ReadSingleCharacter(listOfCells[i]);
                cells[i / 9, i % 9] = new SudokuCell(value);
            }

            SudokuBoard board = new SudokuBoard(cells);
            board.Display();
            return;
            board.Display();
            board.Solve();
        }
    }
}