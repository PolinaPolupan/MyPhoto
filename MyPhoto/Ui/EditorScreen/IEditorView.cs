using MyPhoto.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhoto.Ui
{
    internal interface IEditorView
    {
        System.Drawing.Image Image { get; set; }

        EditorPresenter EditorPresenter { set; }

        void Show();
    }
}
