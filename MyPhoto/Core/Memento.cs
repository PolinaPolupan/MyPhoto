﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using MyPhoto.Core;

namespace MyPhoto.Core
{
    public class ImageMemento
    {
        private readonly Image? _image = null;
        private readonly Dictionary<FiltersLibrary.Filter, int> _values = [];
        private readonly List<FiltersLibrary.Filter> _activeFilters = [];

        public ImageMemento(
            in Image image, Dictionary<FiltersLibrary.Filter, int> values, 
            in List<FiltersLibrary.Filter> activeFilters)
        {
            _image = image;
            _values = values;
            _activeFilters = activeFilters;
        }

        public ImageMemento(
            in Dictionary<FiltersLibrary.Filter, int> values, 
            in List<FiltersLibrary.Filter> activeFilters)
        {
            _values = values;
            _activeFilters = activeFilters;
        }

        public ImageMemento(in Image image)
        {
            _image = image;
        }

        public Image? GetSavedImage()
        {
            return _image;
        }

        public Dictionary<FiltersLibrary.Filter, int> GetSavedValues()
        {
            return new Dictionary<FiltersLibrary.Filter, int>(_values);
        }

        public List<FiltersLibrary.Filter> GetSavedActiveFilters()
        {
            return new List<FiltersLibrary.Filter>(_activeFilters);
        }

        public void ReleaseResources()
        {
            _image?.Dispose();
        }
    }

    public class ImageOriginator
    {
        private Image? _image = null;
        private Dictionary<FiltersLibrary.Filter, int> _values = [];
        private List<FiltersLibrary.Filter> _activeFilters = [];

        public ImageOriginator(
            in Image image, 
            in Dictionary<FiltersLibrary.Filter, int> values, 
            in List<FiltersLibrary.Filter> activeFilters)
        {
            _image = image;
            _values = values;
            _activeFilters = activeFilters;

        }

        public ImageOriginator(in Image image)
        {
            _image = image;
        }

        public void Update(
            in Dictionary<FiltersLibrary.Filter, int> values, 
            in List<FiltersLibrary.Filter> activeFilters, 
            in Image image)
        {
            _image = image;
            _values = values;
            _activeFilters = activeFilters;
        }

        public void Update(in Dictionary<FiltersLibrary.Filter, int> values, in List<FiltersLibrary.Filter> activeFilters)
        {
            _values = values;
            _activeFilters = activeFilters;
        }

        public void UpdateActiveFilters(in List<FiltersLibrary.Filter> activeFilters)
        {
            _activeFilters = activeFilters;
        }

        public Image? GetImage()
        {
            return _image;
        }

        public Dictionary<FiltersLibrary.Filter, int> GetValues()
        { 
            return _values;
        }

        public List<FiltersLibrary.Filter> GetActiveFilters()
        {
            return _activeFilters;
        }

        public ImageMemento CreateMemento()
        {
            if (_image != null)
            {
                return new ImageMemento(_image, _values, _activeFilters);
            }
            return new ImageMemento(_values, _activeFilters);
        }

        public void RestoreFromMemento(in ImageMemento memento)
        {
            _image = memento.GetSavedImage();
            _values = memento.GetSavedValues();
            _activeFilters = memento.GetSavedActiveFilters();
        }
    }

    public class History
    {
        private List<ImageMemento> _mementos = [];
        private int _currentIndex = 0;

        public History(in ImageMemento memento)
        {
            Initialize(memento);
        }

        public void Initialize(in ImageMemento memento)
        {
            // Don't need to increment the current index to add the intial state
            _currentIndex = 0;
            _mementos.Add(memento);
        }

        public void AddMemento(in ImageMemento memento)
        {
            _currentIndex++;

            if (_mementos.Count > _currentIndex)
            {
                // MEMORY LEAK: Resolve issue
                for (int i = _currentIndex; i < _mementos.Count; i++)
                {
                    // This line accidentally deletes the objects, which are active at the current moment
                    // But it is nessecary to release the redundant resources
                    // Resolve this issue
                    // mementos[i].ReleaseResources();  <- Gives errors
                }
                _mementos.RemoveRange(_currentIndex, _mementos.Count - _currentIndex);
            }          

            _mementos.Add(memento);
        }

        public ImageMemento GetInitialMemento()
        {
            return _mementos[0];
        }

        public ImageMemento GetPrevious()
        {
            _currentIndex--;
            if (_currentIndex < 0)
            {
                _currentIndex = 0;
            }
            return _mementos[_currentIndex];
        }

        public ImageMemento GetNext()
        {
            _currentIndex++;
            if (_currentIndex > _mementos.Count - 1)
                _currentIndex = _mementos.Count - 1;
            return _mementos[_currentIndex];
        }

        public bool IsUndoEnabled()
        {
            return _currentIndex > 0;
        }

        public bool IsRedoEnabled()
        {
            return _currentIndex < _mementos.Count - 1;
        }

        public void ResetHistory()
        {
            // Release resources
            for (int i = 0; i < _mementos.Count; i++)
            {
                _mementos[i].ReleaseResources();
            }
            _mementos.Clear();
            _currentIndex = 0;
        }
    }
}
