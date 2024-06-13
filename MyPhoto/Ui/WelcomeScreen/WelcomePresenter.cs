using MyPhoto.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhoto.Ui
{
    internal class WelcomePresenter
    {
        private readonly IWelcomeView _view;

        public ImageEditorState State
        {
            private set;
            get;
        }

        public Image Image 
        {
            get { return State.image; }
            set { State.image = value; }
        }

        public WelcomePresenter(IWelcomeView view, ImageEditorState state)
        {
            _view = view;
            view.WelcomePresenter = this;
            State = state;
        }

        public void LoadImage()
        {
            State.image = ImageLoader.LoadImage();
        }

        public void Hide()
        {
            _view.Hide();
        }
    }
}
