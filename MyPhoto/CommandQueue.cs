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
        private static Dictionary<FiltersLibrary.Filter, int> _filtersValuesMapping = new Dictionary<FiltersLibrary.Filter, int>();
        private static Dictionary<FiltersLibrary.Filter, Func<int, float[][]>> _filtersActionsMapping = new Dictionary<FiltersLibrary.Filter, Func<int, float[][]>>() 
        {
            { FiltersLibrary.Filter.BRIGHTNESS, FiltersLibrary.GetBrightnessMatrix },
            { FiltersLibrary.Filter.CONTRAST, FiltersLibrary.GetContrastMatrix },
            { FiltersLibrary.Filter.SATURATION, FiltersLibrary.GetSaturationMatrix },
            { FiltersLibrary.Filter.RED, FiltersLibrary.GetRedChannelMatrix },
            { FiltersLibrary.Filter.GREEN, FiltersLibrary.GetGreenChannelMatrix },
            { FiltersLibrary.Filter.BLUE, FiltersLibrary.GetBlueChannelMatrix },
            { FiltersLibrary.Filter.SEPIA, FiltersLibrary.GetSepiaMatrix },
            { FiltersLibrary.Filter.GRAYSCALE, FiltersLibrary.GetGrayscaleMatrix },
            { FiltersLibrary.Filter.NEGATIVE, FiltersLibrary.GetNegativeMatrix },
            { FiltersLibrary.Filter.TRANSPARENCY, FiltersLibrary.GetTransparencyMatrix },
            { FiltersLibrary.Filter.DARK, FiltersLibrary.GetDarkMatrix },
            { FiltersLibrary.Filter.BLUE_FILTER, FiltersLibrary.GetBlueMatrix },
            { FiltersLibrary.Filter.PURPLE, FiltersLibrary.GetPurpleMatrix },
        };

        private static Dictionary<FiltersLibrary.Filter, int> _defaultFiltersValues = new Dictionary<FiltersLibrary.Filter, int>() 
        { 
            { FiltersLibrary.Filter.BRIGHTNESS, 0 },
            { FiltersLibrary.Filter.CONTRAST, 100 },
            { FiltersLibrary.Filter.SATURATION, 100 },
            { FiltersLibrary.Filter.RED, 100 },
            { FiltersLibrary.Filter.GREEN, 100 },
            { FiltersLibrary.Filter.BLUE, 100 },
            { FiltersLibrary.Filter.SEPIA, 0 },
            { FiltersLibrary.Filter.GRAYSCALE, 0 },
            { FiltersLibrary.Filter.NEGATIVE, 0 },
            { FiltersLibrary.Filter.TRANSPARENCY, 0 },
            { FiltersLibrary.Filter.DARK, 0 },
            { FiltersLibrary.Filter.BLUE_FILTER, 0 },
            { FiltersLibrary.Filter.PURPLE, 0 },
        };

        public static float[][] ApplyAll()
        {
            float[][] matrix = MathUtils.Identity5x5;
            foreach (KeyValuePair<FiltersLibrary.Filter, int> entry in _filtersValuesMapping)
            {
                var res = _filtersActionsMapping[entry.Key].Invoke(entry.Value);
                matrix = MathUtils.Multiply(res, matrix);
            }
            return matrix;
        }

        public static void AddFilterCommand(FiltersLibrary.Filter filter, int value)
        {
            _filtersValuesMapping[filter] = value;
        }

        private static void AddFunctionCommand(FiltersLibrary.Filter filter, Func<int, float[][]> func)
        {
            _filtersActionsMapping[filter] = func;
        }

        public static Dictionary<FiltersLibrary.Filter, int> GetCommandState()
        {
            return new Dictionary<FiltersLibrary.Filter, int>(_filtersValuesMapping);
        }

        public static void UpdateCommandState(Dictionary<FiltersLibrary.Filter, int> values)
        {
            _filtersValuesMapping = values;
        }

        public static int GetValue(FiltersLibrary.Filter filter)
        {
            return _filtersValuesMapping.TryGetValue(filter, out var temp) ? temp : _defaultFiltersValues[filter];
        }

        public static void ResetAll()
        {
            _filtersValuesMapping.Clear();
            foreach (FiltersLibrary.Filter entry in FiltersLibrary.Filter.GetValues(typeof(FiltersLibrary.Filter)))
            {
                AddFilterCommand(entry, _defaultFiltersValues[entry]);
            }
        }
    }
}
