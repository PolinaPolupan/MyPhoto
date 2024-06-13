using MyPhoto.Core;
using MyPhoto.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MyPhoto.Ui
{
    internal class EditorPresenter
    {
        private readonly IEditorView _view;

        private readonly FiltersManager _filtersManager;

        private readonly History _history;

        private readonly ImageOriginator _originator;

        private readonly ImageEditorState _state;
             
        public EditorPresenter(
            in IEditorView view, 
            in ImageEditorState state, 
            in FiltersManager filtersManager, 
            in History history, 
            in ImageOriginator originator
            )
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

        private void ReleasePictureBoxResources()
        {
            Debug.Assert(_state.image != null); // Assert there is an image in the editor
            Debug.Assert(_view.Image != null); // Assert there is an image in the Picture Box
            _view.Image?.Dispose(); // Release resources
        }

        private void UpdateUi()
        {
            _view.Brightness = GetValue(FiltersLibrary.Filter.BRIGHTNESS);
            _view.Contrast = GetValue(FiltersLibrary.Filter.CONTRAST);
            _view.Saturation = GetValue(FiltersLibrary.Filter.SATURATION);
            _view.Hue = GetValue(FiltersLibrary.Filter.HUE);
            _view.RedChannel = GetValue(FiltersLibrary.Filter.RED);
            _view.GreenChannel = GetValue(FiltersLibrary.Filter.GREEN);
            _view.BlueChannel = GetValue(FiltersLibrary.Filter.BLUE);
            _view.Sepia = GetValue(FiltersLibrary.Filter.SEPIA);
            _view.Negative = GetValue(FiltersLibrary.Filter.NEGATIVE);
            _view.Transparency = GetValue(FiltersLibrary.Filter.TRANSPARENCY);
            _view.Grayscale = GetValue(FiltersLibrary.Filter.GRAYSCALE);
            _view.Dark = GetValue(FiltersLibrary.Filter.DARK);
            _view.Blue = GetValue(FiltersLibrary.Filter.BLUE_FILTER);
            _view.Purple = GetValue(FiltersLibrary.Filter.PURPLE);

            _view.UndoButtonEnabled = IsUndoEnabled();
            _view.RedoButtonEnabled = IsRedoEnabled();
        }

        /// <summary>
        /// Reloads Picture Box - reapplies all of the filters and sets the reloaded image
        /// </summary>
        public void ReloadPictureBox()
        {
            ReleasePictureBoxResources();

            _view.Image = _filtersManager.Reload(ref _state.image);
        }

        /// <summary>
        /// Implements Picture Box down action functionality.
        /// When the user presses Picture Box the unmodified image is shown.
        /// </summary>
        public void ShowInitialImage()
        {
            ReleasePictureBoxResources();

            var image = _history.GetInitialMemento().GetSavedImage(); // Get the unmodified image

            Debug.Assert(image != null); // The initial memento's image is not equal to null, because it's set in the constructor of the history
            _view.Image = (System.Drawing.Image)image.Clone(); // Set the image to the Picture Box
        }

        /// <summary>
        /// Opens the new image into the Picture Box
        /// </summary>
        public void OpenImage()
        {
            var image = ImageLoader.LoadImage(); // Load the image

            if (image != null) // If it's successfull
            {
                _filtersManager.ResetAll(); // Reset all of the filters

                // Release resources
                if (_state.image != null) 
                    _state.image.Dispose();

                _state.image = image;  // Assign the new image to the state

                _history.ResetHistory(); // Reset the history

                var historyImage = (Image)image.Clone(); // Clone the state image to save the copy of it to the history
                // Update the originator with the default values and the new image
                _originator.Update(_filtersManager.GetValues(), _filtersManager.GetActiveFilters(), historyImage);
                // Initilalize the history with the initial memento
                _history.Initialize(_originator.CreateMemento());

                ReloadPictureBox(); // Reapply all of the filters
                // Reset the ui to the initial state
                UpdateUi();
            }            
        }       

        public void ResetAll()
        {
            _filtersManager.ResetAll(); // Reset all of the filters
            ReloadPictureBox(); // Set the image with no filters to the Picture Box
            SaveState(); // Save the action to the history
        }

        /// <summary>
        /// A hepler function which returns the value of the filter from the filters manager
        /// </summary>
        /// <returns>
        /// The value of the filter
        /// </returns>
        private int GetValue(FiltersLibrary.Filter filter)
        {
            return _filtersManager.GetValue(filter);
        }

        public void ApplyGaussianBlur()
        {
            ReleasePictureBoxResources();

            var image = (System.Drawing.Image)_state.image.Clone(); // Create temp image
            _state.image?.Dispose(); // Release the image in the state
            _state.image = _filtersManager.ApplyGaussianBlur(image, 9.25f);
            image.Dispose(); // Release the temp image

            ReloadPictureBox();

            SaveState(true); // Save the state with the image
        }

        public void ApplyMedianBlur()
        {
            ReleasePictureBoxResources();

            var image = (System.Drawing.Image)_state.image.Clone(); // Create temp image
            _state.image?.Dispose(); // Release the image in the state
            _state.image = _filtersManager.ApplyMedianBlur(image, 9);
            image.Dispose(); // Release the temp image

            ReloadPictureBox();

            SaveState(true); // Save the state with the image
        }

        public void ApplyPencil()
        {
            ReleasePictureBoxResources();

            var image = (System.Drawing.Image)_state.image.Clone(); // Create temp image
            _state.image?.Dispose(); // Release the image in the state
            _state.image = _filtersManager.ApplyPencil(image, 9);
            image.Dispose(); // Release the temp image

            ReloadPictureBox();

            SaveState(true); // Save the state with the image
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

        /// <summary>
        /// Saves the current state of the editor to the history.
        /// </summary>
        /// <param name="saveImage">
        /// Sets if the current image in the Picture Box should be saved or not.
        /// It should be set to "false" when a filter which uses Color Matrix is applied.
        /// </param>
        public void SaveState(bool saveImage = false)
        {
            var values = _filtersManager.GetValues();
            var activeFilters = _filtersManager.GetActiveFilters();

            if (saveImage)
            {
                var image = (System.Drawing.Image)_view.Image.Clone(); // Save the copy of the image to the history
                _originator.Update(values, activeFilters, image);
            }
            else
            {
                _originator.Update(values, activeFilters);
            }

            _history.AddMemento(_originator.CreateMemento());

            UpdateUi();
        }

        public void Redo()
        {
            ReleasePictureBoxResources();

            var state = _history.GetNext(); // Get the next state in the history
            _originator.RestoreFromMemento(state);

            // Get the values
            var image = _originator.GetImage();
            var values = _originator.GetValues();
            var activeFilters = _originator.GetActiveFilters();

            // If there is an image in the history - set it to the editor state
            if (image != null)
                _state.image = (System.Drawing.Image)image.Clone();

            // Set the values to the manager
            _filtersManager.SetValues(values);
            _filtersManager.SetActiveFilters(activeFilters);

            UpdateUi();
            ReloadPictureBox();
        }

        public void Undo()
        {
            ReleasePictureBoxResources();

            var state = _history.GetPrevious(); // Get the previous state in the history
            _originator.RestoreFromMemento(state);

            // Get the values
            var image = _originator.GetImage();
            var values = _originator.GetValues();
            var activeFilters = _originator.GetActiveFilters();

            // If there is an image in the history - set it to the editor state
            if (image != null)
                _state.image = (System.Drawing.Image)image.Clone();

            // Set the values to the manager
            _filtersManager.SetValues(values);
            _filtersManager.SetActiveFilters(activeFilters);

            UpdateUi();
            ReloadPictureBox();
        }

        /// <summary>
        /// Checks if the Undo button should be enabled
        /// </summary>
        /// <returns>
        /// If the Undo button is enabled or not
        /// </returns>
        public bool IsUndoEnabled()
        {
            return _history.IsUndoEnabled();
        }

        /// <summary>
        /// Checks if the Redo button should be enabled
        /// </summary>
        /// <returns>
        /// If the Redo button is enabled or not
        /// </returns>
        public bool IsRedoEnabled()
        {
            return _history.IsRedoEnabled();
        }
    }
}
