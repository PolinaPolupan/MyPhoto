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
        public static Dictionary<FiltersLibrary.Filter, int> FiltersValuesMapping = new Dictionary<FiltersLibrary.Filter, int>();

        public static float[][] ApplyAll()
        {
            float[][] matrix = MathUtils.GetIdentity();
            foreach (KeyValuePair<FiltersLibrary.Filter, int> entry in FiltersValuesMapping)
            {
                switch (entry.Key)
                {
                    case FiltersLibrary.Filter.BRIGHTNESS:
                        matrix = MathUtils.Multiply(FiltersLibrary.GetBrightnessMatrix(entry.Value), matrix); 
                        break;
                    case FiltersLibrary.Filter.CONTRAST:
                        matrix = MathUtils.Multiply(FiltersLibrary.GetContrastMatrix(entry.Value), matrix); 
                        break;  
                    case FiltersLibrary.Filter.RED:
                        matrix = MathUtils.Multiply(FiltersLibrary.GetRedChannelMatrix(entry.Value), matrix); 
                        break;
                    case FiltersLibrary.Filter.GREEN:
                        matrix = MathUtils.Multiply(FiltersLibrary.GetGreenChannelMatrix(entry.Value), matrix); 
                        break;
                    case FiltersLibrary.Filter.BLUE:
                        matrix = MathUtils.Multiply(FiltersLibrary.GetBlueChannelMatrix(entry.Value), matrix); 
                        break;
                    case FiltersLibrary.Filter.SEPIA:
                        matrix = MathUtils.Multiply(FiltersLibrary.GetSepiaMatrix(entry.Value), matrix); 
                        break;
                    case FiltersLibrary.Filter.GRAYSCALE:
                        matrix = MathUtils.Multiply(FiltersLibrary.GetGrayscaleMatrix(entry.Value), matrix); 
                        break;
                    case FiltersLibrary.Filter.NEGATIVE:
                        matrix = MathUtils.Multiply(FiltersLibrary.GetNegativeMatrix(entry.Value), matrix); 
                        break;
                    case FiltersLibrary.Filter.SATURATION:
                        matrix = MathUtils.Multiply(FiltersLibrary.GetSaturationMatrix(entry.Value), matrix); 
                        break;
                    case FiltersLibrary.Filter.TRANSPARENCY:
                        matrix = MathUtils.Multiply(FiltersLibrary.GetTransparencyMatrix(entry.Value), matrix);
                        break;
                }
            }
            return matrix;
        }

        public static void AddFilterCommand(FiltersLibrary.Filter filter, int value)
        {
            FiltersValuesMapping[filter] = value;
        }

        public static void ResetAll()
        {
            FiltersValuesMapping.Clear();
            AddFilterCommand(FiltersLibrary.Filter.BRIGHTNESS, 0);
            AddFilterCommand(FiltersLibrary.Filter.CONTRAST, 100);
            AddFilterCommand(FiltersLibrary.Filter.SATURATION, 100);
            AddFilterCommand(FiltersLibrary.Filter.RED, 100);
            AddFilterCommand(FiltersLibrary.Filter.BLUE, 100);
            AddFilterCommand(FiltersLibrary.Filter.GREEN, 100);
        }
    }
}
