using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyPhoto.Core;
using MyPhoto.Utils;
using static System.Net.Mime.MediaTypeNames;

namespace MyPhoto.Ui
{
    public partial class Editor : Form
    {
        public Editor()
        {
            InitializeComponent();
        }

        private void ReleasePictureBoxResources()
        {
            Debug.Assert(ImageEditorState.image != null);
            PictureBox.Image?.Dispose();
        }

        private void SaveState(bool saveImage = false)
        {
            var values = _filtersManager.GetValues();
            var activeFilters = _filtersManager.GetActiveFilters();

            if (saveImage)
            {
                var image = (System.Drawing.Image)PictureBox.Image.Clone();
                _originator.Update(values, activeFilters, image);
            }
            else
            {
                _originator.Update(values, activeFilters);
            }

            _history.AddMemento(_originator.CreateMemento());

            Console.WriteLine(_history.currentIndex);
            Console.WriteLine(_history.mementos.Count);
            Console.WriteLine("\n");

            RedoButton.Enabled = IsRedoEnabled();
            UndoButton.Enabled = IsUndoEnabled();
        }

        private void Editor_Load(object sender, EventArgs e)
        {
            _commandQueue = new CommandQueue();
            _filtersManager = new FiltersManager(_commandQueue);
            ReloadPictureBox();

            var image = (System.Drawing.Image)PictureBox.Image.Clone();
            _history = new History(new ImageMemento(image, _filtersManager.GetValues(), _filtersManager.GetActiveFilters()));
            _originator = new ImageOriginator(image, _filtersManager.GetValues(), _filtersManager.GetActiveFilters());
        }

        private void UpdateUi()
        {
            BrightnessSlider.Value = _filtersManager.GetValue(FiltersLibrary.Filter.BRIGHTNESS);
            ContrastSlider.Value = _filtersManager.GetValue(FiltersLibrary.Filter.CONTRAST);
            SaturationSlider.Value = _filtersManager.GetValue(FiltersLibrary.Filter.SATURATION);
            HueSlider.Value = _filtersManager.GetValue(FiltersLibrary.Filter.HUE);
            RedChannelSlider.Value = _filtersManager.GetValue(FiltersLibrary.Filter.RED);
            GreenChannelSlider.Value = _filtersManager.GetValue(FiltersLibrary.Filter.GREEN);
            BlueChannelSlider.Value = _filtersManager.GetValue(FiltersLibrary.Filter.BLUE);
            SepiaCheckBox.Checked = _filtersManager.GetValue(FiltersLibrary.Filter.SEPIA) > 0;
            NegativeCheckBox.Checked = _filtersManager.GetValue(FiltersLibrary.Filter.NEGATIVE) > 0;
            TransparencyCheckBox.Checked = _filtersManager.GetValue(FiltersLibrary.Filter.TRANSPARENCY) > 0;
            GrayscaleCheckBox.Checked = _filtersManager.GetValue(FiltersLibrary.Filter.GRAYSCALE) > 0;
            DarkCheckBox.Checked = _filtersManager.GetValue(FiltersLibrary.Filter.DARK) > 0;
            BlueCheckBox.Checked =_filtersManager.GetValue(FiltersLibrary.Filter.BLUE_FILTER) > 0;
            PurpleCheckBox.Checked = _filtersManager.GetValue(FiltersLibrary.Filter.PURPLE) > 0;

            UndoButton.Enabled = IsUndoEnabled();
            RedoButton.Enabled = IsRedoEnabled();
        }

        private void ResetAllButton_Click(object sender, EventArgs e)
        {
            ResetAll();

            SaveState();

            UpdateUi();
        }

        private void ResetAll()
        {
            _filtersManager.ResetAll();

            ReloadPictureBox();
        }

        private void OpenButton_Click(object sender, EventArgs e)
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

                UpdateUi();
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            ImageLoader.SaveImage(PictureBox.Image);
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Z))
            {
                Undo();
                return true;
            }
            if (keyData == (Keys.Control | Keys.Z | Keys.Shift))
            {
                Redo();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ReloadPictureBox()
        {
            ReleasePictureBoxResources();
            PictureBox.Image = _filtersManager.Reload(ref ImageEditorState.image);
        }

        private void BrightnessSlider_Scroll(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            PictureBox.Image = _filtersManager.ApplyBrightness(ref ImageEditorState.image, BrightnessSlider.Value);
            SaveState();
        }

        private void ContrastSlider_Scroll(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            PictureBox.Image = _filtersManager.ApplyContrast(ref ImageEditorState.image, ContrastSlider.Value);
            SaveState();
        }

        private void SaturationSlider_Scroll(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            PictureBox.Image = _filtersManager.ApplySaturation(ref ImageEditorState.image, SaturationSlider.Value);
            SaveState();
        }

        private void Red_Scroll(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            PictureBox.Image = _filtersManager.ApplyRedChannel(ref ImageEditorState.image, RedChannelSlider.Value);
            SaveState();
        }

        private void Green_Scroll(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            PictureBox.Image = _filtersManager.ApplyGreenChannel(ref ImageEditorState.image, GreenChannelSlider.Value);
            SaveState();
        }

        private void Blue_Scroll(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            PictureBox.Image = _filtersManager.ApplyBlueChannel(ref ImageEditorState.image, BlueChannelSlider.Value);
            SaveState();
        }
        private void HueSlider_Scroll(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            PictureBox.Image = _filtersManager.ApplyHue(ref ImageEditorState.image, HueSlider.Value);
            SaveState();
        }

        private void TransparencyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            PictureBox.Image = _filtersManager.ApplyTransparency(ref ImageEditorState.image, (TransparencyCheckBox.Checked) ? 1 : 0);
            SaveState();
        }

        private void SepiaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            PictureBox.Image = _filtersManager.ApplySepia(ref ImageEditorState.image, (SepiaCheckBox.Checked) ? 1 : 0);
            SaveState();
        }

        private void GrayscaleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            PictureBox.Image = _filtersManager.ApplyGrayscale(ref ImageEditorState.image, (GrayscaleCheckBox.Checked) ? 1 : 0);
            SaveState();
        }

        private void NegativeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            PictureBox.Image = _filtersManager.ApplyNegative(ref ImageEditorState.image, (NegativeCheckBox.Checked) ? 1 : 0);
            SaveState();
        }

        private void DarkCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            PictureBox.Image = _filtersManager.ApplyDark(ref ImageEditorState.image, (DarkCheckBox.Checked) ? 1 : 0);
            SaveState();
        }

        private void BlueCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            PictureBox.Image = _filtersManager.ApplyBlue(ref ImageEditorState.image, (BlueCheckBox.Checked) ? 1 : 0);
            SaveState();
        }

        private void PurpleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            PictureBox.Image = _filtersManager.ApplyPurple(ref ImageEditorState.image, (PurpleCheckBox.Checked) ? 1 : 0);
            SaveState();
        }

        private void PictureBox_Down(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            var image = _history.GetInitialMemento().GetSavedImage();
            Debug.Assert(image != null);
            PictureBox.Image = (System.Drawing.Image)image.Clone();
        }

        private void PictureBox_Up(object sender, MouseEventArgs e)
        {
            ReloadPictureBox();
        }

        private void GaussianBlurButton_Click(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            var image = (System.Drawing.Image)ImageEditorState.image.Clone();
            ImageEditorState.image?.Dispose();

            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            ImageEditorState.image = _filtersManager.ApplyGaussianBlur(image, 9.25f);
            Cursor = Cursors.Arrow; // change cursor to normal type

            image.Dispose();
            ReloadPictureBox();
            SaveState(true);
        }

        private void MedianBlurButton_Click(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            var image = (System.Drawing.Image)ImageEditorState.image.Clone();
            ImageEditorState.image?.Dispose();

            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            ImageEditorState.image = _filtersManager.ApplyMedianBlur(image, 9);
            Cursor = Cursors.Arrow; // change cursor to normal type

            image.Dispose();
            ReloadPictureBox();
            SaveState(true);
        }

        private void CartoonButton_Click(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            var image = (System.Drawing.Image)ImageEditorState.image.Clone();
            ImageEditorState.image?.Dispose();

            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            ImageEditorState.image = _filtersManager.ApplyCartoon(image, 9);
            Cursor = Cursors.Arrow; // change cursor to normal type

            image.Dispose();
            ReloadPictureBox();
            SaveState(true);
        }

        private void Undo()
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
            UpdateUi();

            ReloadPictureBox();
        }

        private void Redo()
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
            UpdateUi();

            ReloadPictureBox();
        }

        private void UndoButton_Click(object sender, EventArgs e)
        {
            Undo();
        }

        private void RedoButton_Click(object sender, EventArgs e)
        {
            Redo();
        }

        private bool IsUndoEnabled()
        {
            return _history.IsUndoEnabled();
        }

        private bool IsRedoEnabled()
        {
            return _history.IsRedoEnabled();
        }
    }
}
