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

        public WelcomePresenter(IWelcomeView view)
        {
            _view = view;
            view.Presenter = this;
        }

        public void LoadImage()
        {
            ImageLoader.LoadImage(ref ImageEditorState.image, ref ImageEditorState.imagePath);
        }

        public void Hide()
        {
            _view.Hide();
        }
    }
}
