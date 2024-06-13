using MyPhoto.Core;
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

namespace MyPhoto.Core
{
    internal class FiltersManager(CommandQueue commandQueue)
    {
        private readonly CommandQueue _commandQueue = commandQueue;

        private Bitmap ApplyColorMatrix(in Image image)
        {
            int width = image.Width;
            int height = image.Height;
            var bitmap = new Bitmap(width, height);
            using var g = Graphics.FromImage(bitmap);
            using var attributes = new ImageAttributes();

            // Get the resulting matrix
            ColorMatrix colorMatrix = new ColorMatrix(_commandQueue.ApplyAll());
            
            attributes.SetColorMatrix(colorMatrix);
            g.DrawImage(image, new Rectangle(0, 0, width, height),
                  0, 0, width, height, GraphicsUnit.Pixel, attributes);
            g.Dispose(); // Release resources
            return bitmap;
        }

        public void ResetAll()
        {
            _commandQueue.ResetAll();
        }

        public Bitmap Reload(in Image image)
        {
            return ApplyColorMatrix(image);
        }

        public Bitmap ApplySepia(in Image image, int value)
        {
            _commandQueue.AddFilterCommand(FiltersLibrary.Filter.SEPIA, value);
            return ApplyColorMatrix(image);
        }

        public Bitmap ApplyGrayscale(in Image image, int value)
        {
            _commandQueue.AddFilterCommand(FiltersLibrary.Filter.GRAYSCALE, value);
            return ApplyColorMatrix(image);
        }

        public Bitmap ApplyNegative(in Image image, int value)
        {
            _commandQueue.AddFilterCommand(FiltersLibrary.Filter.NEGATIVE, value);
            return ApplyColorMatrix(image);
        }

        public Bitmap ApplyTransparency(in Image image, int value)
        {
            _commandQueue.AddFilterCommand(FiltersLibrary.Filter.TRANSPARENCY, value);
            return ApplyColorMatrix(image);
        }

        public Bitmap ApplyBrightness(in Image image, int brightness)
        {      
            _commandQueue.AddFilterCommand(FiltersLibrary.Filter.BRIGHTNESS, brightness);
            return ApplyColorMatrix(image);
        }

        public Bitmap ApplyContrast(in Image image, int contrast)
        {
            _commandQueue.AddFilterCommand(FiltersLibrary.Filter.CONTRAST, contrast);
            return ApplyColorMatrix(image);
        }

        public Bitmap ApplySaturation(in Image image, int sat)
        {
            _commandQueue.AddFilterCommand(FiltersLibrary.Filter.SATURATION, sat);
            return ApplyColorMatrix(image);
        }

        public Bitmap ApplyHue(in Image image, int value)
        {
            _commandQueue.AddFilterCommand(FiltersLibrary.Filter.HUE, value);
            return ApplyColorMatrix(image);
        }

        public Bitmap ApplyRedChannel(in Image image, int red)
        {
            _commandQueue.AddFilterCommand(FiltersLibrary.Filter.RED_CHANNEL, red);
            return ApplyColorMatrix(image);
        }

        public Bitmap ApplyGreenChannel(in Image image, int green)
        {
            _commandQueue.AddFilterCommand(FiltersLibrary.Filter.GREEN_CHANNEL, green);
            return ApplyColorMatrix(image);
        }

        public Bitmap ApplyBlueChannel(in Image image, int blue)
        {
            _commandQueue.AddFilterCommand(FiltersLibrary.Filter.BLUE_CHANNEL, blue);
            return ApplyColorMatrix(image);
        }

        public Bitmap ApplyDark(in Image image, int value)
        {
            _commandQueue.AddFilterCommand(FiltersLibrary.Filter.DARK, value);
            return ApplyColorMatrix(image);
        }

        public Bitmap ApplyBlue(in Image image, int value)
        {
            _commandQueue.AddFilterCommand(FiltersLibrary.Filter.BLUE, value);
            return ApplyColorMatrix(image);
        }

        public Bitmap ApplyPurple(in Image image, int value)
        {
            _commandQueue.AddFilterCommand(FiltersLibrary.Filter.PURPLE, value);
            return ApplyColorMatrix(image);
        }

        public Bitmap ApplyGaussianBlur(in Image image, float weight)
        {
            return FiltersLibrary.Convolve((System.Drawing.Bitmap)image, FiltersLibrary.GaussianBlur(7, weight));
        }

        public Bitmap ApplyMedianBlur(in Image image, int matrixSize)
        {
            return FiltersLibrary.MedianFilter((System.Drawing.Bitmap)image, matrixSize);
        }

        public Bitmap ApplyPencil(in Image image, byte threshold = 0)
        {
            return FiltersLibrary.GradientBasedEdgeDetectionFilter((System.Drawing.Bitmap)image, threshold);
        }

        public List<FiltersLibrary.Filter> GetActiveFilters()
        {
            return _commandQueue.GetActiveFilters();
        }

        public Dictionary<FiltersLibrary.Filter, int> GetValues()
        {
            return _commandQueue.GetValues();
        }

        public void SetValues(in Dictionary<FiltersLibrary.Filter, int> values)
        {
            _commandQueue.SetValues(values);
        }

        public void SetActiveFilters(in List<FiltersLibrary.Filter> activeFilters)
        {
            _commandQueue.SetActiveFilters(activeFilters);
        }

        public int GetValue(FiltersLibrary.Filter filter)
        {
            return _commandQueue.GetValue(filter);
        }
    }
}
