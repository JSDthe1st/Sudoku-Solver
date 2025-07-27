using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace Sudoku_Solver.Reader
{
    public class OCR
    {
        static void GetNumber()
        {
            string imagePath = @"C:\Users\Sebastian\Pictures\Screenshots\Zrzut ekranu 2025-07-27 183727.png";

            using var ocr = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default);
            ocr.SetVariable("tessedit_char_whitelist", "0123456789");
            using var img = Pix.LoadFromFile(imagePath);
            using var page = ocr.Process(img, PageSegMode.SingleChar);
            Console.WriteLine(page.GetText());
        }
    }
}
