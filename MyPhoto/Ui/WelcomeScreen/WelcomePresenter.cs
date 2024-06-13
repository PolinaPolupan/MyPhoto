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
        private IWelcomeView _view;
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
            view.Presenter = this;
            State = state;
        }

        public void LoadImage()
        {
            ImageLoader.LoadImage(ref State.image, ref State.imagePath);
        }

        public void Hide()
        {
            _view.Hide();
        }
    }
}
