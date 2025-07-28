using System.Drawing;
using System.Windows.Forms;

namespace Sudoku_Solver
{
    public static class ScreenshotHandler
    {
        public static List<Image> SplitImageIntoCells(string imagePath, int numberOfCells)
        {
            Image screenshot = Image.FromFile(imagePath);

            int cellWidth = screenshot.Width / numberOfCells;
            int cellHeight = screenshot.Height / numberOfCells;

            List<Image> cells = new List<Image>();

            for (int i = 0; i < numberOfCells; i++)
            {
                for (int j = 0; j < numberOfCells; j++)
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

            return cells;
        }

        public static void Save(Image image, string path)
        {
            image.Save(path);
        }

        public static void Save(List<Image> images, string path)
        {
            int maxNumberOfDigits = images.Count.ToString().Length;

            for (int i = 0; i < images.Count; i++)
            {
                int currentNumberOfDigits = i.ToString().Length;
                string cellNumber = new string('0', maxNumberOfDigits - currentNumberOfDigits) + i.ToString();

                string fullPath = Path.Combine(path, $"cell{cellNumber}.png");
                images[i].Save(fullPath);
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
