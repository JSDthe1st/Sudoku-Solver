
using Tesseract;

namespace Sudoku_Solver.Reader
{
    public class OCR
    {
        TesseractEngine ocr;

        public OCR()
        {
            ocr = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default);
            //ocr.SetVariable("tessedit_char_whitelist", "123456789 ");
        }

        public string ReadSingleCharacter(string imagePath)
        {
            using var img = Pix.LoadFromFile(imagePath);
            using var page = ocr.Process(img, PageSegMode.SingleBlock);
            //if (page.GetMeanConfidence() < 0.30)
            //    return "";
            return page.GetText().Trim();
        }

        public void Dispose()
        {
            ocr?.Dispose();
        }

        ~OCR()
        {
            Dispose();
        }
    }
}
