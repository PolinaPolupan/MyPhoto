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
    internal partial class EditorForm : Form, IEditorView
    {
        public EditorPresenter EditorPresenter { private get; set; }

        public System.Drawing.Image Image
        {
            get { return PictureBox.Image; }
            set { PictureBox.Image = value; }
        }

        public EditorForm()
        {
            InitializeComponent();
        }

        private void Editor_Load(object sender, EventArgs e)
        {
            EditorPresenter.ReloadPictureBox();
        }

        private void UpdateUi()
        {
            BrightnessSlider.Value = EditorPresenter.GetValue(FiltersLibrary.Filter.BRIGHTNESS);
            ContrastSlider.Value = EditorPresenter.GetValue(FiltersLibrary.Filter.CONTRAST);
            SaturationSlider.Value = EditorPresenter.GetValue(FiltersLibrary.Filter.SATURATION);
            HueSlider.Value = EditorPresenter.GetValue(FiltersLibrary.Filter.HUE);
            RedChannelSlider.Value = EditorPresenter.GetValue(FiltersLibrary.Filter.RED);
            GreenChannelSlider.Value = EditorPresenter.GetValue(FiltersLibrary.Filter.GREEN);
            BlueChannelSlider.Value = EditorPresenter.GetValue(FiltersLibrary.Filter.BLUE);
            SepiaCheckBox.Checked = EditorPresenter.GetValue(FiltersLibrary.Filter.SEPIA) > 0;
            NegativeCheckBox.Checked = EditorPresenter.GetValue(FiltersLibrary.Filter.NEGATIVE) > 0;
            TransparencyCheckBox.Checked = EditorPresenter.GetValue(FiltersLibrary.Filter.TRANSPARENCY) > 0;
            GrayscaleCheckBox.Checked = EditorPresenter.GetValue(FiltersLibrary.Filter.GRAYSCALE) > 0;
            DarkCheckBox.Checked = EditorPresenter.GetValue(FiltersLibrary.Filter.DARK) > 0;
            BlueCheckBox.Checked = EditorPresenter.GetValue(FiltersLibrary.Filter.BLUE_FILTER) > 0;
            PurpleCheckBox.Checked = EditorPresenter.GetValue(FiltersLibrary.Filter.PURPLE) > 0;

            UndoButton.Enabled = EditorPresenter.IsUndoEnabled();
            RedoButton.Enabled = EditorPresenter.IsRedoEnabled();
        }

        private void ResetAllButton_Click(object sender, EventArgs e)
        {
            EditorPresenter.ResetAll();

            EditorPresenter.SaveState();

            RedoButton.Enabled = EditorPresenter.IsRedoEnabled();
            UndoButton.Enabled = EditorPresenter.IsUndoEnabled();

            UpdateUi();
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            EditorPresenter.OpenImage();
            UpdateUi();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            ImageLoader.SaveImage(Image);
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Z))
            {
                EditorPresenter.Undo();
                return true;
            }
            if (keyData == (Keys.Control | Keys.Z | Keys.Shift))
            {
                EditorPresenter.Redo();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void BrightnessSlider_Scroll(object sender, EventArgs e)
        {
            EditorPresenter.ApplyBrightness(BrightnessSlider.Value);
        }

        private void ContrastSlider_Scroll(object sender, EventArgs e)
        {
            EditorPresenter.ApplyContrast(ContrastSlider.Value);
        }

        private void SaturationSlider_Scroll(object sender, EventArgs e)
        {
            EditorPresenter.ApplySaturation(SaturationSlider.Value);
        }

        private void Red_Scroll(object sender, EventArgs e)
        {
            EditorPresenter.ApplyRedChannel(RedChannelSlider.Value);
        }

        private void Green_Scroll(object sender, EventArgs e)
        {
            EditorPresenter.ApplyGreenChannel(GreenChannelSlider.Value);
        }

        private void Blue_Scroll(object sender, EventArgs e)
        {
            EditorPresenter.ApplyBlueChannel(BlueChannelSlider.Value);
        }

        private void HueSlider_Scroll(object sender, EventArgs e)
        {
            EditorPresenter.ApplyHue(HueSlider.Value);
        }

        private void TransparencyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            EditorPresenter.ApplyTransparency((TransparencyCheckBox.Checked) ? 1 : 0);
        }

        private void SepiaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            EditorPresenter.ApplySepia((SepiaCheckBox.Checked) ? 1 : 0);
        }

        private void GrayscaleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            EditorPresenter.ApplyGrayscale((GrayscaleCheckBox.Checked) ? 1 : 0);
        }

        private void NegativeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            EditorPresenter.ApplyNegative((NegativeCheckBox.Checked) ? 1 : 0);
        }

        private void DarkCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            EditorPresenter.ApplyDark((DarkCheckBox.Checked) ? 1 : 0);
        }

        private void BlueCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            EditorPresenter.ApplyBlue((BlueCheckBox.Checked) ? 1 : 0);
        }

        private void PurpleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            EditorPresenter.ApplyPurple((PurpleCheckBox.Checked) ? 1 : 0);
        }

        private void PictureBox_Down(object sender, EventArgs e)
        {
            EditorPresenter.PictureBox_Down();
        }

        private void PictureBox_Up(object sender, MouseEventArgs e)
        {
            EditorPresenter.ReloadPictureBox();
        }

        private void GaussianBlurButton_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            EditorPresenter.ApplyGaussianBlur();
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void MedianBlurButton_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            EditorPresenter.ApplyMedianBlur();
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void CartoonButton_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            EditorPresenter.ApplyCartoon();
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void UndoButton_Click(object sender, EventArgs e)
        {
            EditorPresenter.Undo();

            UpdateUi();

            EditorPresenter.ReloadPictureBox();
        }

        private void RedoButton_Click(object sender, EventArgs e)
        {
            EditorPresenter.Redo();

            UpdateUi();

            EditorPresenter.ReloadPictureBox();
        }
    }
}
