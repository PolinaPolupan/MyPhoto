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
    public partial class EditorForm : Form, IEditorView
    {
        public EditorPresenter EditorPresenter { private get; set; }

        public System.Drawing.Image Image
        {
            get { return PictureBox.Image; }
            set { PictureBox.Image = value; }
        }

        public int Brightness 
        {
            get { return BrightnessSlider.Value; }
            set { BrightnessSlider.Value = value; } 
        }
        
        public int Contrast 
        {
            get { return ContrastSlider.Value; }
            set { ContrastSlider.Value = value; }
        }
        
        public int Saturation 
        {
            get { return SaturationSlider.Value; }
            set { SaturationSlider.Value = value; }
        }
        
        public int Hue 
        {
            get { return HueSlider.Value; }
            set { HueSlider.Value = value; }
        }
        
        public int RedChannel 
        {
            get { return RedChannelSlider.Value; }
            set { RedChannelSlider.Value = value; }
        }
        
        public int GreenChannel 
        {
            get { return GreenChannelSlider.Value; }
            set { GreenChannelSlider.Value = value; }
        }
        
        public int BlueChannel 
        {
            get { return BlueChannelSlider.Value; }
            set { BlueChannelSlider.Value = value; }
        }

        public int Transparency 
        {
            get { return (TransparencyCheckBox.Checked) ? 1 : 0; }
            set { TransparencyCheckBox.Checked = value > 0; }
        }

        public int Sepia 
        {
            get { return (SepiaCheckBox.Checked) ? 1 : 0; }
            set { SepiaCheckBox.Checked = value > 0; }
        }

        public int Grayscale
        {
            get { return (GrayscaleCheckBox.Checked) ? 1 : 0; }
            set { GrayscaleCheckBox.Checked = value > 0; }
        }

        public int Negative
        {
            get { return (NegativeCheckBox.Checked) ? 1 : 0; }
            set { NegativeCheckBox.Checked = value > 0; }
        }
        public int Dark
        {
            get { return (DarkCheckBox.Checked) ? 1 : 0; }
            set { DarkCheckBox.Checked = value > 0; }
        }

        public int Blue
        {
            get { return (BlueCheckBox.Checked) ? 1 : 0; }
            set { BlueCheckBox.Checked = value > 0; }
        }

        public int Purple
        {
            get { return (PurpleCheckBox.Checked) ? 1 : 0; }
            set { PurpleCheckBox.Checked = value > 0; }
        }

        public bool UndoButtonEnabled 
        {
            get { return UndoButton.Enabled; }
            set { UndoButton.Enabled = value; }
        }

        public bool RedoButtonEnabled 
        {
            get { return RedoButton.Enabled; }
            set { RedoButton.Enabled = value; }
        }

        public EditorForm()
        {
            InitializeComponent();
        }

        private void Editor_Load(object sender, EventArgs e)
        {
            EditorPresenter.ReloadPictureBox();
        }

        private void ResetAllButton_Click(object sender, EventArgs e)
        {
            EditorPresenter.ResetAll();
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            EditorPresenter.OpenImage();
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
            EditorPresenter.ApplyBrightness();
        }

        private void ContrastSlider_Scroll(object sender, EventArgs e)
        {
            EditorPresenter.ApplyContrast();
        }

        private void SaturationSlider_Scroll(object sender, EventArgs e)
        {
            EditorPresenter.ApplySaturation();
        }

        private void Red_Scroll(object sender, EventArgs e)
        {
            EditorPresenter.ApplyRedChannel();
        }

        private void Green_Scroll(object sender, EventArgs e)
        {
            EditorPresenter.ApplyGreenChannel();
        }

        private void Blue_Scroll(object sender, EventArgs e)
        {
            EditorPresenter.ApplyBlueChannel();
        }

        private void HueSlider_Scroll(object sender, EventArgs e)
        {
            EditorPresenter.ApplyHue();
        }

        private void TransparencyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            EditorPresenter.ApplyTransparency();
        }

        private void SepiaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            EditorPresenter.ApplySepia();
        }

        private void GrayscaleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            EditorPresenter.ApplyGrayscale();
        }

        private void NegativeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            EditorPresenter.ApplyNegative();
        }

        private void DarkCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            EditorPresenter.ApplyDark();
        }

        private void BlueCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            EditorPresenter.ApplyBlue();
        }

        private void PurpleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            EditorPresenter.ApplyPurple();
        }

        private void PictureBox_Down(object sender, EventArgs e)
        {
            EditorPresenter.ShowInitialImage();
        }

        private void PictureBox_Up(object sender, MouseEventArgs e)
        {
            EditorPresenter.ReloadPictureBox();
        }

        private void GaussianBlurButton_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // Change cursor to hourglass type
            EditorPresenter.ApplyGaussianBlur();
            Cursor = Cursors.Arrow; // Change cursor to normal type
        }

        private void MedianBlurButton_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // Change cursor to hourglass type
            EditorPresenter.ApplyMedianBlur();
            Cursor = Cursors.Arrow; // Change cursor to normal type
        }

        private void CartoonButton_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // Change cursor to hourglass type
            EditorPresenter.ApplyPencil();
            Cursor = Cursors.Arrow; // Change cursor to normal type
        }

        private void UndoButton_Click(object sender, EventArgs e)
        {
            EditorPresenter.Undo();
        }

        private void RedoButton_Click(object sender, EventArgs e)
        {
            EditorPresenter.Redo();
        }
    }
}
