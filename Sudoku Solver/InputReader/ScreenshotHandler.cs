using System.Drawing;
using System.Net;
using System.Windows.Forms;

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
                    int cropHeight = (int)(cellHeight - 2 * cutPadding);
                    int cropWidth = (int)(cellWidth - 2 * cutPadding);

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
                    Color pixel = bitmap.GetPixel(col, row);

                    // reduce to black and white
                    if (pixel.R == 52 &&
                        pixel.G == 72 &&
                        pixel.B == 97)
                    {
                        bitmap.SetPixel(col, row, Color.Black);
                    }
                    else
                    {
                        bitmap.SetPixel(col, row, Color.White);
                    }
                }
            }

            bitmap = (Bitmap)BoldenBlackPixels(bitmap, 2);

            return bitmap;
        }

        public static Image BoldenBlackPixels(Image image, int thickness)
        {
            Bitmap bitmap = new Bitmap(image);

            for (int i = 0; i < thickness; i++)
            {
                Console.WriteLine(i);
                for (int row = 0; row < bitmap.Height; row++)
                {
                    for (int col = 0; col < bitmap.Width; col++)
                    {
                        try
                        {
                            Color pixelBelow = bitmap.GetPixel(col, row + 1);
                            (int, int, int) RGBBelow = (pixelBelow.R, pixelBelow.G, pixelBelow.B);

                            Color pixelLeft = bitmap.GetPixel(col + 1, row);
                            (int, int, int) RGBLeft = (pixelLeft.R, pixelLeft.G, pixelLeft.B);

                            if (RGBBelow == (0, 0, 0) || RGBLeft == (0, 0, 0))
                                bitmap.SetPixel(col, row, Color.Black);
                        }
                        catch (ArgumentOutOfRangeException)
                        { }
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

        static Image CropImage(Image image, int cropStartX, int cropStartY, int cropWidth, int cropHeight)
        {
            Rectangle rectangle = new Rectangle(cropStartX, cropStartY, cropWidth, cropHeight);
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
