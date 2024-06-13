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

        public WelcomePresenter Presenter { private get; set; }

        private void RoundedButton_Click(object sender, EventArgs e)
        {
            Presenter.LoadImage();
            if (Presenter.Image != null)
            {
                var commandQueue = new CommandQueue();
                var filtersManager = new FiltersManager(commandQueue);

                var image = (System.Drawing.Image)Presenter.Image.Clone();
                var history = new History(new ImageMemento(image, filtersManager.GetValues(), filtersManager.GetActiveFilters()));
                var originator = new ImageOriginator(image, filtersManager.GetValues(), filtersManager.GetActiveFilters());

                var editorView = new EditorForm();
                var state = Presenter.State;
                var presenter = new EditorPresenter(editorView, state, filtersManager, history, originator);

                presenter.Show();
                Presenter.Hide();
            }        
        }
    }
}
