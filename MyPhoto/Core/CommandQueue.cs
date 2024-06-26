﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPhoto.Utils;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MyPhoto.Core
{
    public class CommandQueue
    {
        /// <summary>
        /// _activeFilters list saves the filters, which are applied to the image at the current moment
        /// </summary>
        private List<FiltersLibrary.Filter> _activeFilters = [];
        /// <summary>
        /// _filtersValues dictionary saves the filters with the appropriate values (examples: intensity, slider value)
        /// </summary>
        private Dictionary<FiltersLibrary.Filter, int> _filtersValues = [];
        /// <summary>
        /// _filtersActions is a container, that maps filters to the functions of the Filters Library
        /// </summary>
        private Dictionary<FiltersLibrary.Filter, Func<int, float[][]>> _filtersActions = new Dictionary<FiltersLibrary.Filter, Func<int, float[][]>>()
        {
            { FiltersLibrary.Filter.BRIGHTNESS, FiltersLibrary.GetBrightnessMatrix },
            { FiltersLibrary.Filter.CONTRAST, FiltersLibrary.GetContrastMatrix },
            { FiltersLibrary.Filter.SATURATION, FiltersLibrary.GetSaturationMatrix },
            { FiltersLibrary.Filter.RED_CHANNEL, FiltersLibrary.GetRedChannelMatrix },
            { FiltersLibrary.Filter.GREEN_CHANNEL, FiltersLibrary.GetGreenChannelMatrix },
            { FiltersLibrary.Filter.BLUE_CHANNEL, FiltersLibrary.GetBlueChannelMatrix },
            { FiltersLibrary.Filter.SEPIA, FiltersLibrary.GetSepiaMatrix },
            { FiltersLibrary.Filter.GRAYSCALE, FiltersLibrary.GetGrayscaleMatrix },
            { FiltersLibrary.Filter.NEGATIVE, FiltersLibrary.GetNegativeMatrix },
            { FiltersLibrary.Filter.TRANSPARENCY, FiltersLibrary.GetTransparencyMatrix },
            { FiltersLibrary.Filter.DARK, FiltersLibrary.GetDarkMatrix },
            { FiltersLibrary.Filter.BLUE, FiltersLibrary.GetBlueMatrix },
            { FiltersLibrary.Filter.PURPLE, FiltersLibrary.GetPurpleMatrix },
            { FiltersLibrary.Filter.HUE, FiltersLibrary.GetHueMatrix },
        };
        /// <summary>
        /// _defaultFiltersValues saves the default values of filters
        /// </summary>
        private Dictionary<FiltersLibrary.Filter, int> _defaultFiltersValues = new Dictionary<FiltersLibrary.Filter, int>()
        {
            { FiltersLibrary.Filter.BRIGHTNESS, 0 },
            { FiltersLibrary.Filter.CONTRAST, 100 },
            { FiltersLibrary.Filter.SATURATION, 100 },
            { FiltersLibrary.Filter.RED_CHANNEL, 100 },
            { FiltersLibrary.Filter.GREEN_CHANNEL, 100 },
            { FiltersLibrary.Filter.BLUE_CHANNEL, 100 },
            { FiltersLibrary.Filter.SEPIA, 0 },
            { FiltersLibrary.Filter.GRAYSCALE, 0 },
            { FiltersLibrary.Filter.NEGATIVE, 0 },
            { FiltersLibrary.Filter.TRANSPARENCY, 0 },
            { FiltersLibrary.Filter.DARK, 0 },
            { FiltersLibrary.Filter.BLUE, 0 },
            { FiltersLibrary.Filter.PURPLE, 0 },
            { FiltersLibrary.Filter.HUE, 0 },
        };

        private void UpdateActiveFilters()
        {
            // Iterating over the list in reverse order to avoid
            // "Collection was modified; enumeration operation may not execute"
            // exception
            for (int i = _activeFilters.Count - 1; i >= 0; i--)
            {
                if (GetValue(_activeFilters[i]) == _defaultFiltersValues[_activeFilters[i]])
                {
                    _activeFilters.Remove(_activeFilters[i]);
                }
            }
        }

        private void AddActiveFilter(FiltersLibrary.Filter filter)
        {
            // Don't add the filter twice
            if (!_activeFilters.Contains(filter))
            {
                _activeFilters.Add(filter);
            }            
            UpdateActiveFilters();
        }

        public float[][] ApplyAll()
        {
            float[][] matrix = MathUtils.Identity5x5;
            foreach (FiltersLibrary.Filter filter in _activeFilters)
            {
                var res = _filtersActions[filter].Invoke(_filtersValues[filter]);
                matrix = MathUtils.Multiply(res, matrix);
            }
            return matrix;
        }

        public void AddFilterCommand(FiltersLibrary.Filter filter, int value)
        {
            _filtersValues[filter] = value;
            AddActiveFilter(filter);
        }

        public Dictionary<FiltersLibrary.Filter, int> GetValues()
        {
            return new Dictionary<FiltersLibrary.Filter, int>(_filtersValues);
        }

        public List<FiltersLibrary.Filter> GetActiveFilters()
        {
            return new List<FiltersLibrary.Filter>(_activeFilters);
        }

        public void SetValues(in Dictionary<FiltersLibrary.Filter, int> values)
        {
            _filtersValues = values;
        }

        public void SetActiveFilters(in List<FiltersLibrary.Filter> activeFilters)
        {
            _activeFilters = activeFilters;
        }

        public int GetValue(FiltersLibrary.Filter filter)
        {
            return _filtersValues.TryGetValue(filter, out var temp) ? temp : _defaultFiltersValues[filter];
        }

        public void ResetAll()
        {
            _filtersValues.Clear();
            _activeFilters.Clear();
        }
    }
}
