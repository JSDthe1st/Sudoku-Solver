using System;
using Tesseract;

namespace Sudoku_Solver
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            //SudokuBoard solver = new SudokuBoard();
            //solver.LoadNumbers(@"D:\Projects\TO DO\Sudoku Solver\example_board.txt");
            //solver.Display();

            string imagePath = @"C:\Users\Sebastian\Pictures\Screenshots\Zrzut ekranu 2025-07-27 183727.png";

            using var ocr = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default);
            ocr.SetVariable("tessedit_char_whitelist", "0123456789");
            using var img = Pix.LoadFromFile(imagePath);
            using var page = ocr.Process(img, PageSegMode.SingleChar);
            Console.WriteLine(page.GetText());
        }
    }
}
