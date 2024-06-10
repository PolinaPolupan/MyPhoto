using MyPhoto.Core;
using MyPhoto.Utils;

namespace MyPhoto.Ui
{
    public partial class WelcomeForm : Form, IWelcomeView
    {
        public WelcomeForm()
        {
            InitializeComponent();
            this.SendToBack();
        }

        public WelcomePresenter Presenter { private get; set; }

        private void RoundedButton_Click(object sender, EventArgs e)
        {
            Presenter.LoadImage();
            if (ImageEditorState.image != null)
            {
                EditorForm editor = new EditorForm();
                editor.Show();
                Hide();
            }        
        }
    }
}
