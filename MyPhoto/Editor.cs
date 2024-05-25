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

        private void ReloadPictureBox()
        {
            ReleasePictureBoxResources();
            PictureBox.Image = FiltersManager.Reload(ref ImageEditorState.image);
        }

        private void Editor_Load(object sender, EventArgs e)
        {
            ReloadPictureBox();
        }

        private void BrightnessSlider_Scroll(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            PictureBox.Image = FiltersManager.ApplyBrightness(ref ImageEditorState.image, BrightnessSlider.Value);
        }

        private void ContrastSlider_Scroll(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            PictureBox.Image = FiltersManager.ApplyContrast(ref ImageEditorState.image, ContrastSlider.Value);
        }

        private void SaturationSlider_Scroll(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            PictureBox.Image = FiltersManager.ApplySaturation(ref ImageEditorState.image, SaturationSlider.Value);
        }

        private void Red_Scroll(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            PictureBox.Image = FiltersManager.ApplyRedChannel(ref ImageEditorState.image, RedChannelSlider.Value);
        }

        private void Green_Scroll(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            PictureBox.Image = FiltersManager.ApplyGreenChannel(ref ImageEditorState.image, GreenChannelSlider.Value);
        }

        private void Blue_Scroll(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            PictureBox.Image = FiltersManager.ApplyBlueChannel(ref ImageEditorState.image, BlueChannelSlider.Value);
        }

        private void TransparencyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            PictureBox.Image = FiltersManager.ApplyTransparency(ref ImageEditorState.image, (TransparencyCheckBox.Checked) ? 1 : 0);
        }

        private void SepiaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            PictureBox.Image = FiltersManager.ApplySepia(ref ImageEditorState.image, (SepiaCheckBox.Checked) ? 1 : 0);
        }

        private void GrayscaleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            PictureBox.Image = FiltersManager.ApplyGrayscale(ref ImageEditorState.image, (GrayscaleCheckBox.Checked) ? 1 : 0);

        }

        private void NegativeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            PictureBox.Image = FiltersManager.ApplyNegative(ref ImageEditorState.image, (NegativeCheckBox.Checked) ? 1 : 0);
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

        private void ResetAllButton_Click(object sender, EventArgs e)
        {
            ResetAll();
        }

        private void ResetAll()
        {
            BrightnessSlider.Value = 0;
            ContrastSlider.Value = 100;
            SaturationSlider.Value = 100;
            RedChannelSlider.Value = 100;
            GreenChannelSlider.Value = 100;
            BlueChannelSlider.Value = 100;
            SepiaCheckBox.Checked = false;
            GrayscaleCheckBox.Checked = false;
            NegativeCheckBox.Checked = false;
            TransparencyCheckBox.Checked = false;

            FiltersManager.ResetAll();
            ReleasePictureBoxResources();
            ReloadPictureBox();
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            var res = ImageLoader.LoadImage(ref ImageEditorState.image, ref ImageEditorState.imagePath);
            if (res == DialogResult.OK)
            {
                ReloadPictureBox(); // Reapply all of the filters
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            ImageLoader.SaveImage(PictureBox.Image);
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {

        }

        private void GaussianBlurButton_Click(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            var image = (System.Drawing.Image)ImageEditorState.image.Clone();
            ImageEditorState.image?.Dispose();
            ImageEditorState.image = FiltersManager.ApplyGaussianBlur(image, 9.25f);
            image.Dispose();
            ReloadPictureBox();
        }

        private void MedianBlurButton_Click(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            var image = (System.Drawing.Image)ImageEditorState.image.Clone();
            ImageEditorState.image?.Dispose();
            ImageEditorState.image = FiltersManager.ApplyMedianBlur(image, 9);
            image.Dispose();
            ReloadPictureBox();
        }

        private void CartoonButton_Click(object sender, EventArgs e)
        {
            ReleasePictureBoxResources();
            var image = (System.Drawing.Image)ImageEditorState.image.Clone();
            ImageEditorState.image?.Dispose();
            ImageEditorState.image = FiltersManager.ApplyCartoon(image, 9);
            image.Dispose();
            ReloadPictureBox();
        }
    }
}
