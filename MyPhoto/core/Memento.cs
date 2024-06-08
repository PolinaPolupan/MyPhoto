using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using MyPhoto.Core;

namespace MyPhoto.Core
{
    internal class ImageMemento
    {
        private Image? _image = null;
        private Dictionary<FiltersLibrary.Filter, int> _values;
        private List<FiltersLibrary.Filter> _activeFilters;

        public ImageMemento(Image image, Dictionary<FiltersLibrary.Filter, int> values, List<FiltersLibrary.Filter> activeFilters)
        {
            _image = image;
            _values = values;
            _activeFilters = activeFilters;
        }

        public ImageMemento(Dictionary<FiltersLibrary.Filter, int> values, List<FiltersLibrary.Filter> activeFilters)
        {
            _values = values;
            _activeFilters = activeFilters;
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

    internal class ImageOriginator
    {
        private Image? _image = null;
        private Dictionary<FiltersLibrary.Filter, int> _values;
        private List<FiltersLibrary.Filter> _activeFilters;

        public ImageOriginator(Image? image, Dictionary<FiltersLibrary.Filter, int> values, List<FiltersLibrary.Filter> activeFilters)
        {
            _image = image;
            _values = values;
            _activeFilters = activeFilters;
        }

        public void Update(Dictionary<FiltersLibrary.Filter, int> values, List<FiltersLibrary.Filter> activeFilters, Image image)
        {
            _image = image;
            _values = values;
            _activeFilters = activeFilters;
        }

        public void Update(Dictionary<FiltersLibrary.Filter, int> values, List<FiltersLibrary.Filter> activeFilters)
        {
            _values = values;
            _activeFilters = activeFilters;
        }

        public void UpdateActiveFilters(List<FiltersLibrary.Filter> activeFilters)
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

        public void RestoreFromMemento(ImageMemento memento)
        {
            _image = memento.GetSavedImage();
            _values = memento.GetSavedValues();
            _activeFilters = memento.GetSavedActiveFilters();
        }
    }

    internal class History
    {
        public List<ImageMemento> mementos = new List<ImageMemento>();
        public int currentIndex = 0;

        public History(ImageMemento memento)
        {
            Initialize(memento);
        }

        public void Initialize(ImageMemento memento)
        {
            currentIndex = 0;
            mementos.Add(memento);
        }

        public void AddMemento(ImageMemento memento)
        {
            currentIndex++;

            if (mementos.Count > currentIndex)
            {
                // MEMORY LEAK: Resolve issue
                for (int i = currentIndex; i < mementos.Count; i++)
                {
                    //mementos[i].ReleaseResources();  
                }
                mementos.RemoveRange(currentIndex, mementos.Count - currentIndex);
            }          

            mementos.Add(memento);
        }

        public ImageMemento GetInitialMemento()
        {
            return mementos[0];
        }

        public ImageMemento GetPrevious()
        {
            currentIndex--;
            if (currentIndex < 0)
            {
                currentIndex = 0;
            }
            return mementos[currentIndex];
        }

        public ImageMemento GetNext()
        {
            currentIndex++;
            if (currentIndex > mementos.Count - 1)
                currentIndex = mementos.Count - 1;
            return mementos[currentIndex];
        }

        public bool IsUndoEnabled()
        {
            return currentIndex > 0;
        }

        public bool IsRedoEnabled()
        {
            return currentIndex < mementos.Count - 1;
        }

        public void ResetHistory()
        {
            for (int i = 0; i < mementos.Count; i++)
            {
                mementos[i].ReleaseResources();
            }
            mementos.Clear();
            currentIndex = 0;
        }
    }
}
