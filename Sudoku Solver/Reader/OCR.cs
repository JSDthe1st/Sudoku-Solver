using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace Sudoku_Solver.Reader
{
    public class OCR
    {
        public string ReadSingleCharacter(string imagePath)
        {
            using var ocr = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default);
            ocr.SetVariable("tessedit_char_whitelist", "0123456789 ");
            using var img = Pix.LoadFromFile(imagePath);
            using var page = ocr.Process(img, PageSegMode.SingleChar);
            return page.GetText().Trim();
        }
    }
}
