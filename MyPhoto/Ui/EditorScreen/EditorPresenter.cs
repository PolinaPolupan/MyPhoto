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

        private readonly ImageEditorState _state;

        public Image Image
        {
            get { return _state.image; }
            set { _state.image = value; }
        }

        public EditorPresenter(IEditorView view, ImageEditorState state, FiltersManager filtersManager, History history, ImageOriginator originator)
        {
            _view = view;
            _view.EditorPresenter = this;
            _filtersManager = filtersManager;
            _history = history;
            _originator = originator;
            _state = state;
        }

        public void Show()
        {
            _view.Show();
        }

        public void ReloadPictureBox()
        {
            ReleasePictureBoxResources();
            _view.Image = _filtersManager.Reload(ref _state.image);
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
            var res = ImageLoader.LoadImage(ref _state.image, ref _state.imagePath);
            if (res == DialogResult.OK)
            {
                _filtersManager.ResetAll();
                ReloadPictureBox(); // Reapply all of the filters

                Debug.Assert(_state.image != null); // What if the image == null? Resolve issue
                var image = (System.Drawing.Image)_state.image.Clone();

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
            Debug.Assert(_state.image != null);
            _view.Image?.Dispose();
        }

        public void ApplyGaussianBlur()
        {
            ReleasePictureBoxResources();
            var image = (System.Drawing.Image)_state.image.Clone();
            _state.image?.Dispose();

            _state.image = _filtersManager.ApplyGaussianBlur(image, 9.25f);

            image.Dispose();
            ReloadPictureBox();
            SaveState(true);
        }

        public void ApplyMedianBlur()
        {
            ReleasePictureBoxResources();
            var image = (System.Drawing.Image)_state.image.Clone();
            _state.image?.Dispose();

            _state.image = _filtersManager.ApplyMedianBlur(image, 9);

            image.Dispose();
            ReloadPictureBox();
            SaveState(true);
        }

        public void ApplyCartoon()
        {
            ReleasePictureBoxResources();
            var image = (System.Drawing.Image)_state.image.Clone();
            _state.image?.Dispose();

            _state.image = _filtersManager.ApplyCartoon(image, 9);

            image.Dispose();
            ReloadPictureBox();
            SaveState(true);
        }

        public void ApplyBrightness()
        {
            ReleasePictureBoxResources();
            _view.Image = _filtersManager.ApplyBrightness(ref _state.image, _view.Brightness);
            SaveState();
        }

        public void ApplyContrast()
        {
            ReleasePictureBoxResources();
            _view.Image = _filtersManager.ApplyContrast(ref _state.image, _view.Contrast);
            SaveState();
        }

        public void ApplySaturation()
        {
            ReleasePictureBoxResources();
            _view.Image = _filtersManager.ApplySaturation(ref _state.image, _view.Saturation);
            SaveState();
        }

        public void ApplyRedChannel()
        {
            ReleasePictureBoxResources();
            _view.Image = _filtersManager.ApplyRedChannel(ref _state.image, _view.RedChannel);
            SaveState();
        }

        public void ApplyGreenChannel()
        {
            ReleasePictureBoxResources();
            _view.Image = _filtersManager.ApplyGreenChannel(ref _state.image, _view.GreenChannel);
            SaveState();
        }

        public void ApplyBlueChannel()
        {
            ReleasePictureBoxResources();
            _view.Image = _filtersManager.ApplyBlueChannel(ref _state.image, _view.BlueChannel);
            SaveState();
        }

        public void ApplyHue()
        {
            ReleasePictureBoxResources();
            _view.Image = _filtersManager.ApplyHue(ref _state.image, _view.Hue);
            SaveState();
        }

        public void ApplyTransparency()
        {
            ReleasePictureBoxResources();
            _view.Image = _filtersManager.ApplyTransparency(ref _state.image, _view.Transparency);
            SaveState();
        }

        public void ApplySepia()
        {
            ReleasePictureBoxResources();
            _view.Image = _filtersManager.ApplySepia(ref _state.image, _view.Sepia);
            SaveState();
        }

        public void ApplyGrayscale()
        {
            ReleasePictureBoxResources();
            _view.Image = _filtersManager.ApplyGrayscale(ref _state.image, _view.Grayscale);
            SaveState();
        }

        public void ApplyNegative()
        {
            ReleasePictureBoxResources();
            _view.Image = _filtersManager.ApplyNegative(ref _state.image, _view.Negative);
            SaveState();
        }

        public void ApplyDark()
        {
            ReleasePictureBoxResources();
            _view.Image = _filtersManager.ApplyDark(ref _state.image, _view.Dark);
            SaveState();
        }

        public void ApplyBlue()
        {
            ReleasePictureBoxResources();
            _view.Image = _filtersManager.ApplyBlue(ref _state.image, _view.Blue);
            SaveState();
        }

        public void ApplyPurple()
        {
            ReleasePictureBoxResources();
            _view.Image = _filtersManager.ApplyPurple(ref _state.image, _view.Purple);
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
                _state.image = (System.Drawing.Image)image.Clone();

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
                _state.image = (System.Drawing.Image)image.Clone();

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
