using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhoto.Ui
{
    internal interface IWelcomeView
    {
        WelcomePresenter Presenter { set; }

        void Hide();
    }
}
