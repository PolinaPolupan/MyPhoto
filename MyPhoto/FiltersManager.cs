using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyPhoto
{
    internal static class FiltersManager
    {
        private static Bitmap ApplyColorMatrix(ref Image image)
        {
            int width = image.Width;
            int height = image.Height;
            var bitmap = new Bitmap(width, height);
            using var g = Graphics.FromImage(bitmap);
            using var attributes = new ImageAttributes();

            // Get the resulting matrix
            ColorMatrix colorMatrix = new ColorMatrix(CommandQueue.ApplyAll());
            
            attributes.SetColorMatrix(colorMatrix);
            g.DrawImage(image, new Rectangle(0, 0, width, height),
                  0, 0, width, height, GraphicsUnit.Pixel, attributes);
            g.Dispose(); // Release resources
            return bitmap;
        }

        public static void ResetAll()
        {
            CommandQueue.ResetAll();
        }

        public static Bitmap Reload(ref Image image)
        {
            return ApplyColorMatrix(ref image);
        }

        public static Bitmap ApplySepia(ref Image image, int value)
        {
            CommandQueue.AddFilterCommand(FiltersLibrary.Filter.SEPIA, value);
            return ApplyColorMatrix(ref image);
        }

        public static Bitmap ApplyGrayscale(ref Image image, int value)
        {
            CommandQueue.AddFilterCommand(FiltersLibrary.Filter.GRAYSCALE, value);
            return ApplyColorMatrix(ref image);
        }

        public static Bitmap ApplyNegative(ref Image image, int value)
        {
            CommandQueue.AddFilterCommand(FiltersLibrary.Filter.NEGATIVE, value);
            return ApplyColorMatrix(ref image);
        }

        public static Bitmap ApplyTransparency(ref Image image, int value)
        {
            CommandQueue.AddFilterCommand(FiltersLibrary.Filter.TRANSPARENCY, value);
            return ApplyColorMatrix(ref image);
        }

        public static Bitmap ApplyBrightness(ref Image image, int brightness)
        {      
            CommandQueue.AddFilterCommand(FiltersLibrary.Filter.BRIGHTNESS, brightness);
            return ApplyColorMatrix(ref image);
        }

        public static Bitmap ApplyContrast(ref Image image, int contrast)
        {
            CommandQueue.AddFilterCommand(FiltersLibrary.Filter.CONTRAST, contrast);
            return ApplyColorMatrix(ref image);
        }

        public static Bitmap ApplySaturation(ref Image image, int sat)
        {
            CommandQueue.AddFilterCommand(FiltersLibrary.Filter.SATURATION, sat);
            return ApplyColorMatrix(ref image);
        }

        public static Bitmap ApplyRedChannel(ref Image image, int red)
        {
            CommandQueue.AddFilterCommand(FiltersLibrary.Filter.RED, red);
            return ApplyColorMatrix(ref image);
        }

        public static Bitmap ApplyGreenChannel(ref Image image, int green)
        {
            CommandQueue.AddFilterCommand(FiltersLibrary.Filter.GREEN, green);
            return ApplyColorMatrix(ref image);
        }

        public static Bitmap ApplyBlueChannel(ref Image image, int blue)
        {
            CommandQueue.AddFilterCommand(FiltersLibrary.Filter.BLUE, blue);
            return ApplyColorMatrix(ref image);
        }

        public static Bitmap ApplyGaussianBlur(Image image, float weight)
        {
            return FiltersLibrary.Convolve((System.Drawing.Bitmap)image, FiltersLibrary.GaussianBlur(7, weight));
        }

        public static Bitmap ApplyMedianBlur(Image image, int matrixSize)
        {
            return FiltersLibrary.MedianFilter((System.Drawing.Bitmap)image, matrixSize);
        }

        public static Bitmap ApplyCartoon(Image image, byte threshold = 0)
        {
            return FiltersLibrary.GradientBasedEdgeDetectionFilter((System.Drawing.Bitmap)image, threshold);
        }
    }
}
