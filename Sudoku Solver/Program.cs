using Sudoku_Solver.Reader;
using Sudoku_Solver.Sudoku;
using Sudoku_Solver.InputWriter;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Sudoku_Solver
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string[] listOfCells = CutLastScreenshot();

            SudokuCell[,] cells = ReadValuesFromImages(listOfCells);

            SudokuBoard board = new SudokuBoard(cells);
            board.Display();
            Console.WriteLine();

            board.Solve(true);
            board.IsCorrect();

            //return;
            if (board.IsSolved())
                EnterSolution(board.Board);
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
            ocr.Dispose();

            return cells;
        }

        public static void EnterSolution(SudokuCell[,] board)
        {
            Console.WriteLine("Place your cursor on the first cell.");
            Console.WriteLine("Starting in...");
            for(int i = 5; i > 0; i--)
            {
                Console.WriteLine(i + "...");
                Thread.Sleep(1000);
            }
            Console.WriteLine("Script activated!");

            KeyboardOperations keyboard = new();
            MouseOperations mouse = new();

            Point startPosition = mouse.GetMousePosition();
            int cellWidth = Image.FromFile(ScreenshotHandler.GetLastScreenshotPath()).Width / 9;
            int cellHeight = Image.FromFile(ScreenshotHandler.GetLastScreenshotPath()).Height / 9;
            
            for (int row = 0; row < 9; row++)
            {
                
                for (int col = 0; col < 9; col++)
                {
                    int currentNumber = Convert.ToInt32(board[row, col].Value.ToString());
                    mouse.ClickLeft();
                    keyboard.PressKey(KeyboardOperations.KeyCodes[currentNumber]);                    

                    mouse.MoveRight((short)(75));

                    Thread.Sleep(10);
                }

                mouse.MoveTo((short)startPosition.X, (short)(startPosition.Y + (row + 1) * cellHeight));
            }

        }
    }
}