using MyPhoto.Core;
using MyPhoto.Utils;

namespace MyPhoto.Ui
{
    public partial class WelcomeScreen : Form
    {
        public WelcomeScreen()
        {
            InitializeComponent();
            this.SendToBack();
        }

        private void RoundedButton_Click(object sender, EventArgs e)
        {
            LoadImage();
            if (ImageEditorState.image != null)
            {
                Editor editor = new Editor();
                editor.Show();
                Hide();
            }        
        }

        void LoadImage()
        {
            ImageLoader.LoadImage(ref ImageEditorState.image, ref ImageEditorState.imagePath);
        }
    }
}
