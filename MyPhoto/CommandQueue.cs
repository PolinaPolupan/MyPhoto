using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MyPhoto
{
    internal static class CommandQueue
    {
        private static Dictionary<FiltersLibrary.Filter, int> filtersValuesMapping = new Dictionary<FiltersLibrary.Filter, int>();
        private static Dictionary<FiltersLibrary.Filter, Func<int, float[][]>> filtersActionsMapping = new Dictionary<FiltersLibrary.Filter, Func<int, float[][]>>();

        public static float[][] ApplyAll()
        {
            float[][] matrix = MathUtils.Identity5x5;
            foreach (KeyValuePair<FiltersLibrary.Filter, int> entry in filtersValuesMapping)
            {
                var res = filtersActionsMapping[entry.Key].Invoke(entry.Value);
                matrix = MathUtils.Multiply(res, matrix);
            }
            return matrix;
        }

        public static void AddFilterCommand(FiltersLibrary.Filter filter, int value)
        {
            filtersValuesMapping[filter] = value;
        }

        private static void AddFunctionCommand(FiltersLibrary.Filter filter, Func<int, float[][]> func)
        {
            filtersActionsMapping[filter] = func;
        }

        public static Dictionary<FiltersLibrary.Filter, int> GetCommandState()
        {
            return new Dictionary<FiltersLibrary.Filter, int>(filtersValuesMapping);
        }

        public static void UpdateCommandState(Dictionary<FiltersLibrary.Filter, int> values)
        {
            filtersValuesMapping = values;
        }

        public static int GetBrightness()
        {
            return filtersValuesMapping[FiltersLibrary.Filter.BRIGHTNESS];
        }

        public static int GetContrast()
        {
            return filtersValuesMapping[FiltersLibrary.Filter.CONTRAST];
        }

        public static int GetSaturation()
        {
            return filtersValuesMapping[FiltersLibrary.Filter.SATURATION];
        }

        public static int GetRed()
        {
            return filtersValuesMapping[FiltersLibrary.Filter.RED];
        }

        public static int GetGreen()
        {
            return filtersValuesMapping[FiltersLibrary.Filter.GREEN];
        }

        public static int GetBlue()
        {
            return filtersValuesMapping[FiltersLibrary.Filter.BLUE];
        }

        public static int GetSepia()
        {
            return filtersValuesMapping[FiltersLibrary.Filter.SEPIA];
        }

        public static int GetGrayscale()
        {
            return filtersValuesMapping[FiltersLibrary.Filter.GRAYSCALE];
        }

        public static int GetNegative()
        {
            return filtersValuesMapping[FiltersLibrary.Filter.NEGATIVE];
        }

        public static int GetTransparency()
        {
            return filtersValuesMapping[FiltersLibrary.Filter.TRANSPARENCY];
        }

        public static void ResetAll()
        {
            filtersValuesMapping.Clear();
            AddFilterCommand(FiltersLibrary.Filter.BRIGHTNESS, 0);
            AddFilterCommand(FiltersLibrary.Filter.CONTRAST, 100);
            AddFilterCommand(FiltersLibrary.Filter.SATURATION, 100);
            AddFilterCommand(FiltersLibrary.Filter.RED, 100);
            AddFilterCommand(FiltersLibrary.Filter.GREEN, 100);
            AddFilterCommand(FiltersLibrary.Filter.BLUE, 100);            
            AddFilterCommand(FiltersLibrary.Filter.SEPIA, 0);
            AddFilterCommand(FiltersLibrary.Filter.GRAYSCALE, 0);
            AddFilterCommand(FiltersLibrary.Filter.NEGATIVE, 0);
            AddFilterCommand(FiltersLibrary.Filter.TRANSPARENCY, 0);

            filtersActionsMapping.Clear();
            AddFunctionCommand(FiltersLibrary.Filter.BRIGHTNESS, FiltersLibrary.GetBrightnessMatrix);
            AddFunctionCommand(FiltersLibrary.Filter.CONTRAST, FiltersLibrary.GetContrastMatrix);
            AddFunctionCommand(FiltersLibrary.Filter.SATURATION, FiltersLibrary.GetSaturationMatrix);
            AddFunctionCommand(FiltersLibrary.Filter.RED, FiltersLibrary.GetRedChannelMatrix);
            AddFunctionCommand(FiltersLibrary.Filter.GREEN, FiltersLibrary.GetGreenChannelMatrix);
            AddFunctionCommand(FiltersLibrary.Filter.BLUE, FiltersLibrary.GetBlueChannelMatrix);
            AddFunctionCommand(FiltersLibrary.Filter.SEPIA, FiltersLibrary.GetSepiaMatrix);
            AddFunctionCommand(FiltersLibrary.Filter.GRAYSCALE, FiltersLibrary.GetGrayscaleMatrix);
            AddFunctionCommand(FiltersLibrary.Filter.NEGATIVE, FiltersLibrary.GetNegativeMatrix);
            AddFunctionCommand(FiltersLibrary.Filter.TRANSPARENCY, FiltersLibrary.GetTransparencyMatrix);
        }
    }
}
