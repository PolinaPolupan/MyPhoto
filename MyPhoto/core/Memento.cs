using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using MyPhoto.core;

namespace MyPhoto
{
    internal class ImageMemento
    {
        private Image? image = null;
        private Dictionary<FiltersLibrary.Filter, int> values;

        public ImageMemento(Image image, Dictionary<FiltersLibrary.Filter, int> values)
        {
            this.image = image;
            this.values = values;
        }

        public ImageMemento(Dictionary<FiltersLibrary.Filter, int> values)
        {
            this.values = values;
        }

        public Image? GetSavedImage()
        {
            return image;
        }

        public Dictionary<FiltersLibrary.Filter, int> GetSavedValues()
        {
            return new Dictionary<FiltersLibrary.Filter, int>(values);
        }

        public void ReleaseResources()
        {
            image?.Dispose();
        }
    }

    internal class ImageOriginator
    {
        private Image? image = null;
        private Dictionary<FiltersLibrary.Filter, int> values;

        public ImageOriginator(Image? image, Dictionary<FiltersLibrary.Filter, int> values)
        {
            this.image = image;
            this.values = values;
        }

        public void UpdateImage(Image image)
        {
            this.image = image;
        }

        public void UpdateValues(Dictionary<FiltersLibrary.Filter, int> values)
        {
            this.values = values;
        }

        public Image? GetImage()
        {
            return image;
        }

        public Dictionary<FiltersLibrary.Filter, int> GetValues()
        { 
            return values;
        }

        public ImageMemento CreateMemento()
        {
            if (image != null)
            {
                return new ImageMemento(this.image, this.values);
            }
            return new ImageMemento(this.values);
        }

        public void RestoreFromMemento(ImageMemento memento)
        {
            this.image = memento.GetSavedImage();
            this.values = memento.GetSavedValues();
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
