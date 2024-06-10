using MyPhoto.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhoto.Ui
{
    public class WelcomePresenter
    {
        private readonly IWelcomeView _view;

        public WelcomePresenter(IWelcomeView view)
        {
            _view = view;
            view.Presenter = this;
        }

        public void LoadImage()
        {
            ImageLoader.LoadImage(ref ImageEditorState.image, ref ImageEditorState.imagePath);
        }
    }
}
