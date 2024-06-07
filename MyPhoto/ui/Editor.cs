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
using MyPhoto.core;
using MyPhoto.utils;
using static System.Net.Mime.MediaTypeNames;

namespace MyPhoto
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
            var values = CommandQueue.GetCommandState();
            originator.UpdateValues(values);

            if (saveImage)
            {
                var image = (System.Drawing.Image)PictureBox.Image.Clone();
                originator.UpdateImage(image);
            }

            history.AddMemento(originator.CreateMemento());

            Console.WriteLine(history.currentIndex);
            Console.WriteLine(history.mementos.Count);
            Console.WriteLine("\n");

            RedoButton.Enabled = IsRedoEnabled();
            UndoButton.Enabled = IsUndoEnabled();
        }

        private void Editor_Load(object sender, EventArgs e)
        {
            ResetAll();
            var image = (System.Drawing.Image)PictureBox.Image.Clone();
            history = new History(new ImageMemento(image, CommandQueue.GetCommandState()));
            originator = new ImageOriginator(image, CommandQueue.GetCommandState());
            UpdateUi();
            ReloadPictureBox();
        }

        private void UpdateUi()
        {
            BrightnessSlider.Value = CommandQueue.GetValue(FiltersLibrary.Filter.BRIGHTNESS);
            ContrastSlider.Value = CommandQueue.GetValue(FiltersLibrary.Filter.CONTRAST);
            SaturationSlider.Value = CommandQueue.GetValue(FiltersLibrary.Filter.SATURATION);
            RedChannelSlider.Value = CommandQueue.GetValue(FiltersLibrary.Filter.RED);
            GreenChannelSlider.Value = CommandQueue.GetValue(FiltersLibrary.Filter.GREEN);
            BlueChannelSlider.Value = CommandQueue.GetValue(FiltersLibrary.Filter.BLUE);
            SepiaCheckBox.Checked = CommandQueue.GetValue(FiltersLibrary.Filter.SEPIA) > 0;
            NegativeCheckBox.Checked = CommandQueue.GetValue(FiltersLibrary.Filter.NEGATIVE) > 0;
            TransparencyCheckBox.Checked = CommandQueue.GetValue(FiltersLibrary.Filter.TRANSPARENCY) > 0;
            GrayscaleCheckBox.Checked = CommandQueue.GetValue(FiltersLibrary.Filter.GRAYSCALE) > 0;
            DarkCheckBox.Checked = CommandQueue.GetValue(FiltersLibrary.Filter.DARK) > 0;
            BlueCheckBox.Checked = CommandQueue.GetValue(FiltersLibrary.Filter.BLUE_FILTER) > 0;
            PurpleCheckBox.Checked = CommandQueue.GetValue(FiltersLibrary.Filter.PURPLE) > 0;

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
            FiltersManager.ResetAll();
            ReloadPictureBox();
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            var res = ImageLoader.LoadImage(ref ImageEditorState.image, ref ImageEditorState.imagePath);
            if (res == DialogResult.OK)
            {
                CommandQueue.ResetAll();
                ReloadPictureBox(); // Reapply all of the filters

                var image = (System.Drawing.Image)ImageEditorState.image.Clone();

                history.ResetHistory();
                originator.UpdateImage(image);
                originator.UpdateValues(CommandQueue.GetCommandState());
                history.Initialize(originator.CreateMemento());

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
            PictureBox.Image = FiltersManager.Reload(ref ImageEditorState.image);
        }

        private void BrightnessSlider_Scroll(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            PictureBox.Image = FiltersManager.ApplyBrightness(ref ImageEditorState.image, BrightnessSlider.Value);
            SaveState();
        }

        private void ContrastSlider_Scroll(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            PictureBox.Image = FiltersManager.ApplyContrast(ref ImageEditorState.image, ContrastSlider.Value);
            SaveState();
        }

        private void SaturationSlider_Scroll(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            PictureBox.Image = FiltersManager.ApplySaturation(ref ImageEditorState.image, SaturationSlider.Value);
            SaveState();
        }

        private void Red_Scroll(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            PictureBox.Image = FiltersManager.ApplyRedChannel(ref ImageEditorState.image, RedChannelSlider.Value);
            SaveState();
        }

        private void Green_Scroll(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            PictureBox.Image = FiltersManager.ApplyGreenChannel(ref ImageEditorState.image, GreenChannelSlider.Value);
            SaveState();
        }

        private void Blue_Scroll(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            PictureBox.Image = FiltersManager.ApplyBlueChannel(ref ImageEditorState.image, BlueChannelSlider.Value);
            SaveState();
        }

        private void TransparencyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            PictureBox.Image = FiltersManager.ApplyTransparency(ref ImageEditorState.image, (TransparencyCheckBox.Checked) ? 1 : 0);
            SaveState();
        }

        private void SepiaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            PictureBox.Image = FiltersManager.ApplySepia(ref ImageEditorState.image, (SepiaCheckBox.Checked) ? 1 : 0);
            SaveState();
        }

        private void GrayscaleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            PictureBox.Image = FiltersManager.ApplyGrayscale(ref ImageEditorState.image, (GrayscaleCheckBox.Checked) ? 1 : 0);
            SaveState();
        }

        private void NegativeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            PictureBox.Image = FiltersManager.ApplyNegative(ref ImageEditorState.image, (NegativeCheckBox.Checked) ? 1 : 0);
            SaveState();
        }

        private void DarkCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            PictureBox.Image = FiltersManager.ApplyDark(ref ImageEditorState.image, (DarkCheckBox.Checked) ? 1 : 0);
            SaveState();
        }

        private void BlueCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            PictureBox.Image = FiltersManager.ApplyBlue(ref ImageEditorState.image, (BlueCheckBox.Checked) ? 1 : 0);
            SaveState();
        }

        private void PurpleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            PictureBox.Image = FiltersManager.ApplyPurple(ref ImageEditorState.image, (PurpleCheckBox.Checked) ? 1 : 0);
            SaveState();
        }

        private void PictureBox_Down(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            PictureBox.Image = (System.Drawing.Image)ImageEditorState.image.Clone();
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
            ImageEditorState.image = FiltersManager.ApplyGaussianBlur(image, 9.25f);
            image.Dispose();
            ReloadPictureBox();
            SaveState(true);
        }

        private void MedianBlurButton_Click(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            var image = (System.Drawing.Image)ImageEditorState.image.Clone();
            ImageEditorState.image?.Dispose();
            ImageEditorState.image = FiltersManager.ApplyMedianBlur(image, 9);
            image.Dispose();
            ReloadPictureBox();
            SaveState(true);
        }

        private void CartoonButton_Click(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            var image = (System.Drawing.Image)ImageEditorState.image.Clone();
            ImageEditorState.image?.Dispose();
            ImageEditorState.image = FiltersManager.ApplyCartoon(image, 9);
            image.Dispose();
            ReloadPictureBox();
            SaveState(true);
        }

        private void Undo()
        {
            ReleasePictureBoxResources();
            var state = history.GetPrevious();
            originator.RestoreFromMemento(state);

            var image = originator.GetImage();
            var values = originator.GetValues();

            if (image != null)
            {
                ImageEditorState.image = (System.Drawing.Image)image.Clone();
            }

            CommandQueue.UpdateCommandState(values);
            UpdateUi();

            Console.WriteLine(history.currentIndex);
            Console.WriteLine(history.mementos.Count);
            Console.WriteLine("\n");

            ReloadPictureBox();
        }

        private void Redo()
        {
            ReleasePictureBoxResources();
            var state = history.GetNext();
            originator.RestoreFromMemento(state);

            var image = originator.GetImage();
            var values = originator.GetValues();

            if (image != null)
            {
                ImageEditorState.image = (System.Drawing.Image)image.Clone();
            }
            CommandQueue.UpdateCommandState(values);
            UpdateUi();

            Console.WriteLine(history.currentIndex);
            Console.WriteLine(history.mementos.Count);
            Console.WriteLine("\n");

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
            return history.IsUndoEnabled();
        }

        private bool IsRedoEnabled()
        {
            return history.IsRedoEnabled();
        }

        
    }
}
