using System.Drawing;
using System.Windows.Forms;

namespace Sudoku_Solver
{
    public static class BoardReader
    {

        public static void SplitImageIntoCells()
        {
            Image screenshot = Image.FromFile(GetLastScreenshotPath());

            int cellWidth = screenshot.Width / 9;
            int cellHeight = screenshot.Height / 9;

            List<Image> cells = new List<Image>();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    int padding = 10;
                    int cropStartX = j * cellWidth + padding;
                    int cropStartY = i * cellHeight + padding;
                    int cropHeight = cellWidth - padding;
                    int cropWidth = cellHeight - padding;
                    Image croppedImage = CropImage(GetLastScreenshotPath(), cropStartX, cropStartY, cropWidth, cropHeight);
                    cells.Add(croppedImage);
                }
            }

            for (int i = 0; i < cells.Count; i++)
            {
                cells[i].Save($"Cells\\cell{i}.png");
            }

        }

        static Image CropImage(string path, int x, int y, int width, int height)
        {
            Image obraz = Image.FromFile(path);
            Rectangle rectangle = new Rectangle(x, y, width, height);
            Bitmap bitmap = new Bitmap(obraz);
            Image croppedImage = bitmap.Clone(rectangle, bitmap.PixelFormat);
            return croppedImage;
        }

        public static string GetLastScreenshotPath()
        {
            string screenShotFolderPath = GetScreenshotFolderPath();
            List<string> files = new();
            files = Directory.GetFiles(screenShotFolderPath).ToList();
            string lastFile = files.Last();
            return lastFile;
        }

        public static string GetScreenshotFolderPath()
        {
            return $"C:\\Users\\{GetWindowsUsername()}\\Pictures\\Screenshots";
        }

        static string GetWindowsUsername()
        {
            return Environment.UserName;
        }
    }
}
