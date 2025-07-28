using System.Drawing;

namespace Sudoku_Solver
{
    public static class ScreenshotHandler
    {
        public static List<Image> SplitImageIntoCells(string imagePath, int numberOfCells, int cutPadding = 0)
        {
            Image screenshot = Image.FromFile(imagePath);

            double cellWidth = (double) screenshot.Width / numberOfCells;
            double cellHeight = (double) screenshot.Height / numberOfCells;

            List<Image> cells = new List<Image>();

            for (int i = 0; i < numberOfCells; i++)
            {
                for (int j = 0; j < numberOfCells; j++)
                {
                    int cropStartX = (int)(j * cellWidth + cutPadding);
                    int cropStartY = (int)(i * cellHeight + cutPadding);
                    int cropHeight = (int)(cellWidth - cutPadding);
                    int cropWidth = (int)(cellHeight - cutPadding);

                    Image croppedImage = CropImage(screenshot, cropStartX, cropStartY, cropWidth, cropHeight);
                    cells.Add(croppedImage);
                }
            }

            return cells;
        }

        public static Image Preprocess(Image image)
        {
            Bitmap bitmap = new Bitmap(image);
            
            for (int row = 0; row < bitmap.Height; row++)
            {
                for (int col = 0; col < bitmap.Width; col++)
                {
                    Color color = bitmap.GetPixel(col, row);

                    if (color.R == 52 &&
                        color.G == 72 &&
                        color.B == 97)
                    {
                        bitmap.SetPixel(col, row, Color.Black);
                    }
                    else
                    {
                        bitmap.SetPixel(col, row, Color.White);
                    }

                }
            }

            return bitmap;
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

        static Image CropImage(Image image, int x, int y, int width, int height)
        {
            Rectangle rectangle = new Rectangle(x, y, width, height);
            Bitmap bitmap = new Bitmap(image);
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
