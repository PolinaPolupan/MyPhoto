using MyPhoto.Core;
using MyPhoto.Utils;

namespace MyPhoto.Ui
{
    internal partial class WelcomeForm : Form, IWelcomeView
    {
        public WelcomeForm()
        {
            InitializeComponent();
            this.SendToBack();
        }

        public WelcomePresenter WelcomePresenter { private get; set; }

        private void RoundedButton_Click(object sender, EventArgs e)
        {
            WelcomePresenter.LoadImage();
            // TODO: Move this logic to the factory
            if (WelcomePresenter.Image != null)
            {
                var commandQueue = new CommandQueue();
                var filtersManager = new FiltersManager(commandQueue);

                var image = (System.Drawing.Image)WelcomePresenter.Image.Clone();
                var history = new History(new ImageMemento(image));
                var originator = new ImageOriginator(image);

                var editorView = new EditorForm();
                var state = WelcomePresenter.State;
                var presenter = new EditorPresenter(editorView, state, filtersManager, history, originator);

                presenter.Show();
                WelcomePresenter.Hide();
            }        
        }
    }
}
