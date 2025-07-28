using Sudoku_Solver.Reader;
using Sudoku_Solver.Sudoku;
using System.Drawing;
using System.Windows.Forms;

namespace Sudoku_Solver
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var listOfCells = CutLastScreenshot();
            //return;

            SudokuCell[,] cells = ReadValuesFromImages(listOfCells);

            SudokuBoard board = new SudokuBoard(cells);
            board.Display();

            //board.Solve(true);
        }

        public static string[] CutLastScreenshot()
        {
            string lastScreenshotPath = ScreenshotHandler.GetLastScreenshotPath();
            Image lastScreenshot = Image.FromFile(lastScreenshotPath);
            Image processedImage = ScreenshotHandler.Preprocess(lastScreenshot);
            string processedImagePath = "ProcessedImage\\preprocessedImage.png";
            processedImage.Save(processedImagePath);

            List<Image> cellImages = ScreenshotHandler.SplitImageIntoCells(processedImagePath, 9, 5);
            string cellsFolder = "CellImages";
            ScreenshotHandler.Save(cellImages, cellsFolder);

            var listOfCells = Directory.GetFiles(cellsFolder);

            return listOfCells;
        }

        public static SudokuCell[,] ReadValuesFromImages(string[] listOfCells)
        {
            SudokuCell[,] cells = new SudokuCell[9, 9];
            OCR ocr = new OCR();
            for (int i = 0; i < 81; i++)
            {
                string value = "-";
                value = ocr.ReadSingleCharacter(listOfCells[i]);
                cells[i / 9, i % 9] = new SudokuCell(value);
            }

            return cells;
        }
    }
}