using MyPhoto.Core;
using MyPhoto.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhoto.Ui
{
    internal class EditorPresenter
    {
        private readonly IEditorView _view;

        private readonly FiltersManager _filtersManager;

        private readonly History _history;

        private readonly ImageOriginator _originator;

        public EditorPresenter(IEditorView view, FiltersManager filtersManager, History history, ImageOriginator originator)
        {
            _view = view;
            _view.EditorPresenter = this;
            _filtersManager = filtersManager;
            _history = history;
            _originator = originator;
        }

        public void Show()
        {
            _view.Show();
        }

        public void ReloadPictureBox()
        {
            ReleasePictureBoxResources();
            _view.Image = _filtersManager.Reload(ref ImageEditorState.image);
        }

        public void PictureBox_Down()
        {
            ReleasePictureBoxResources();
            var image = _history.GetInitialMemento().GetSavedImage();
            Debug.Assert(image != null);
            _view.Image = (System.Drawing.Image)image.Clone();
        }

        public void OpenImage()
        {
            var res = ImageLoader.LoadImage(ref ImageEditorState.image, ref ImageEditorState.imagePath);
            if (res == DialogResult.OK)
            {
                _filtersManager.ResetAll();
                ReloadPictureBox(); // Reapply all of the filters

                Debug.Assert(ImageEditorState.image != null); // What if the image == null? Resolve issue
                var image = (System.Drawing.Image)ImageEditorState.image.Clone();

                _history.ResetHistory();

                _originator.Update(_filtersManager.GetValues(), _filtersManager.GetActiveFilters(), image);

                _history.Initialize(_originator.CreateMemento());
            }
        }

        public void ResetAll()
        {
            _filtersManager.ResetAll();
            ReloadPictureBox();
        }

        public int GetValue(FiltersLibrary.Filter filter)
        {
            return _filtersManager.GetValue(filter);
        }

        private void ReleasePictureBoxResources()
        {
            Debug.Assert(ImageEditorState.image != null);
            _view.Image?.Dispose();
        }

        public void ApplyGaussianBlur()
        {
            ReleasePictureBoxResources();
            var image = (System.Drawing.Image)ImageEditorState.image.Clone();
            ImageEditorState.image?.Dispose();

            ImageEditorState.image = _filtersManager.ApplyGaussianBlur(image, 9.25f);

            image.Dispose();
            ReloadPictureBox();
            SaveState(true);
        }

        public void ApplyMedianBlur()
        {
            ReleasePictureBoxResources();
            var image = (System.Drawing.Image)ImageEditorState.image.Clone();
            ImageEditorState.image?.Dispose();

            ImageEditorState.image = _filtersManager.ApplyMedianBlur(image, 9);

            image.Dispose();
            ReloadPictureBox();
            SaveState(true);
        }

        public void ApplyCartoon()
        {
            ReleasePictureBoxResources();
            var image = (System.Drawing.Image)ImageEditorState.image.Clone();
            ImageEditorState.image?.Dispose();

            ImageEditorState.image = _filtersManager.ApplyCartoon(image, 9);

            image.Dispose();
            ReloadPictureBox();
            SaveState(true);
        }

        public void ApplyBrightness(int value)
        {
            ReleasePictureBoxResources();
            _view.Image = _filtersManager.ApplyBrightness(ref ImageEditorState.image, value);
            SaveState();
        }

        public void ApplyContrast(int value)
        {
            ReleasePictureBoxResources();
            _view.Image = _filtersManager.ApplyContrast(ref ImageEditorState.image, value);
            SaveState();
        }

        public void ApplySaturation(int value)
        {
            ReleasePictureBoxResources();
            _view.Image = _filtersManager.ApplySaturation(ref ImageEditorState.image, value);
            SaveState();
        }

        public void ApplyRedChannel(int value)
        {
            ReleasePictureBoxResources();
            _view.Image = _filtersManager.ApplyRedChannel(ref ImageEditorState.image, value);
            SaveState();
        }

        public void ApplyGreenChannel(int value)
        {
            ReleasePictureBoxResources();
            _view.Image = _filtersManager.ApplyGreenChannel(ref ImageEditorState.image, value);
            SaveState();
        }

        public void ApplyBlueChannel(int value)
        {
            ReleasePictureBoxResources();
            _view.Image = _filtersManager.ApplyBlueChannel(ref ImageEditorState.image, value);
            SaveState();
        }

        public void ApplyHue(int value)
        {
            ReleasePictureBoxResources();
            _view.Image = _filtersManager.ApplyHue(ref ImageEditorState.image, value);
            SaveState();
        }

        public void ApplyTransparency(int value)
        {
            ReleasePictureBoxResources();
            _view.Image = _filtersManager.ApplyTransparency(ref ImageEditorState.image, value);
            SaveState();
        }

        public void ApplySepia(int value)
        {
            ReleasePictureBoxResources();
            _view.Image = _filtersManager.ApplySepia(ref ImageEditorState.image, value);
            SaveState();
        }

        public void ApplyGrayscale(int value)
        {
            ReleasePictureBoxResources();
            _view.Image = _filtersManager.ApplyGrayscale(ref ImageEditorState.image, value);
            SaveState();
        }

        public void ApplyNegative(int value)
        {
            ReleasePictureBoxResources();
            _view.Image = _filtersManager.ApplyNegative(ref ImageEditorState.image, value);
            SaveState();
        }

        public void ApplyDark(int value)
        {
            ReleasePictureBoxResources();
            _view.Image = _filtersManager.ApplyDark(ref ImageEditorState.image, value);
            SaveState();
        }

        public void ApplyBlue(int value)
        {
            _view.Image = _filtersManager.ApplyBlue(ref ImageEditorState.image, value);
            SaveState();
        }

        public void ApplyPurple(int value)
        {
            ReleasePictureBoxResources();
            _view.Image = _filtersManager.ApplyPurple(ref ImageEditorState.image, value);
            SaveState();
        }

        public void SaveState(bool saveImage = false)
        {
            var values = _filtersManager.GetValues();
            var activeFilters = _filtersManager.GetActiveFilters();

            if (saveImage)
            {
                var image = (System.Drawing.Image)_view.Image.Clone();
                _originator.Update(values, activeFilters, image);
            }
            else
            {
                _originator.Update(values, activeFilters);
            }

            _history.AddMemento(_originator.CreateMemento());
        }

        public void Redo()
        {
            ReleasePictureBoxResources();
            var state = _history.GetNext();
            _originator.RestoreFromMemento(state);

            var image = _originator.GetImage();
            var values = _originator.GetValues();
            var activeFilters = _originator.GetActiveFilters();

            if (image != null)
                ImageEditorState.image = (System.Drawing.Image)image.Clone();

            _filtersManager.SetValues(values);
            _filtersManager.SetActiveFilters(activeFilters);
        }

        public void Undo()
        {
            ReleasePictureBoxResources();
            var state = _history.GetPrevious();
            _originator.RestoreFromMemento(state);

            var image = _originator.GetImage();
            var values = _originator.GetValues();
            var activeFilters = _originator.GetActiveFilters();

            if (image != null)
                ImageEditorState.image = (System.Drawing.Image)image.Clone();

            _filtersManager.SetValues(values);
            _filtersManager.SetActiveFilters(activeFilters);
        }

        public bool IsUndoEnabled()
        {
            return _history.IsUndoEnabled();
        }

        public bool IsRedoEnabled()
        {
            return _history.IsRedoEnabled();
        }
    }
}
