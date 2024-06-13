using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhoto.Ui
{
    public interface IWelcomeView
    {
        WelcomePresenter WelcomePresenter { set; }

        void Hide();
    }
}
