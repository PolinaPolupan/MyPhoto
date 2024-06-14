using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace MyPhoto.Utils
{
    public static class ImageLoader
    {
        public static Image? LoadImage()
        {
            Image? image = null;
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Image Files|*.jpg;*.jpeg;*.png;..." })
            {
                DialogResult dialogResult = openFileDialog.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    image = Image.FromFile(openFileDialog.FileName);
                }
            }
            return image;
        }

        public static DialogResult SaveImage(in Image image)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = "Bitmap Image (.bmp)|*.bmp|Gif Image (.gif)|*.gif|JPEG Image (.jpeg)|*.jpeg|Png Image (.png)|*.png|Tiff Image (.tiff)|*.tiff|Wmf Image (.wmf)|*.wmf" })
            {
                DialogResult dialogResult = saveFileDialog.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    image.Save(saveFileDialog.FileName);                  
                }
                return dialogResult;
            }           
        }
    }
}
