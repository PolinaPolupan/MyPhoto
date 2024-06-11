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

        private Bitmap ApplyColorMatrix(ref Image image)
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

        public Bitmap Reload(ref Image image)
        {
            return ApplyColorMatrix(ref image);
        }

        public Bitmap ApplySepia(ref Image image, int value)
        {
            _commandQueue.AddFilterCommand(FiltersLibrary.Filter.SEPIA, value);
            return ApplyColorMatrix(ref image);
        }

        public Bitmap ApplyGrayscale(ref Image image, int value)
        {
            _commandQueue.AddFilterCommand(FiltersLibrary.Filter.GRAYSCALE, value);
            return ApplyColorMatrix(ref image);
        }

        public Bitmap ApplyNegative(ref Image image, int value)
        {
            _commandQueue.AddFilterCommand(FiltersLibrary.Filter.NEGATIVE, value);
            return ApplyColorMatrix(ref image);
        }

        public Bitmap ApplyTransparency(ref Image image, int value)
        {
            _commandQueue.AddFilterCommand(FiltersLibrary.Filter.TRANSPARENCY, value);
            return ApplyColorMatrix(ref image);
        }

        public Bitmap ApplyBrightness(ref Image image, int brightness)
        {      
            _commandQueue.AddFilterCommand(FiltersLibrary.Filter.BRIGHTNESS, brightness);
            return ApplyColorMatrix(ref image);
        }

        public Bitmap ApplyContrast(ref Image image, int contrast)
        {
            _commandQueue.AddFilterCommand(FiltersLibrary.Filter.CONTRAST, contrast);
            return ApplyColorMatrix(ref image);
        }

        public Bitmap ApplySaturation(ref Image image, int sat)
        {
            _commandQueue.AddFilterCommand(FiltersLibrary.Filter.SATURATION, sat);
            return ApplyColorMatrix(ref image);
        }

        public Bitmap ApplyHue(ref Image image, int value)
        {
            _commandQueue.AddFilterCommand(FiltersLibrary.Filter.HUE, value);
            return ApplyColorMatrix(ref image);
        }

        public Bitmap ApplyRedChannel(ref Image image, int red)
        {
            _commandQueue.AddFilterCommand(FiltersLibrary.Filter.RED, red);
            return ApplyColorMatrix(ref image);
        }

        public Bitmap ApplyGreenChannel(ref Image image, int green)
        {
            _commandQueue.AddFilterCommand(FiltersLibrary.Filter.GREEN, green);
            return ApplyColorMatrix(ref image);
        }

        public Bitmap ApplyBlueChannel(ref Image image, int blue)
        {
            _commandQueue.AddFilterCommand(FiltersLibrary.Filter.BLUE, blue);
            return ApplyColorMatrix(ref image);
        }

        public Bitmap ApplyDark(ref Image image, int value)
        {
            _commandQueue.AddFilterCommand(FiltersLibrary.Filter.DARK, value);
            return ApplyColorMatrix(ref image);
        }

        public Bitmap ApplyBlue(ref Image image, int value)
        {
            _commandQueue.AddFilterCommand(FiltersLibrary.Filter.BLUE_FILTER, value);
            return ApplyColorMatrix(ref image);
        }

        public Bitmap ApplyPurple(ref Image image, int value)
        {
            _commandQueue.AddFilterCommand(FiltersLibrary.Filter.PURPLE, value);
            return ApplyColorMatrix(ref image);
        }

        public Bitmap ApplyGaussianBlur(Image image, float weight)
        {
            return FiltersLibrary.Convolve((System.Drawing.Bitmap)image, FiltersLibrary.GaussianBlur(7, weight));
        }

        public Bitmap ApplyMedianBlur(Image image, int matrixSize)
        {
            return FiltersLibrary.MedianFilter((System.Drawing.Bitmap)image, matrixSize);
        }

        public Bitmap ApplyCartoon(Image image, byte threshold = 0)
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

        public void SetValues(Dictionary<FiltersLibrary.Filter, int> values)
        {
            _commandQueue.SetValues(values);
        }

        public void SetActiveFilters(List<FiltersLibrary.Filter> activeFilters)
        {
            _commandQueue.SetActiveFilters(activeFilters);
        }

        public int GetValue(FiltersLibrary.Filter filter)
        {
            return _commandQueue.GetValue(filter);
        }
    }
}
