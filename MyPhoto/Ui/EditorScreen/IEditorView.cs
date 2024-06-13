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
        int Brightness { get; set; }

        int Contrast { get; set; }

        int Saturation { get; set; }

        int Hue { get; set; }

        int RedChannel { get; set; }

        int GreenChannel { get; set; }

        int BlueChannel { get; set; }

        int Transparency { get; set; }

        int Sepia {  get; set; }

        int Grayscale { get; set; }

        int Negative {  get; set; }

        int Dark { get; set; }

        int Blue { get; set; }

        int Purple { get; set; }

        bool UndoButtonEnabled { get; set; }

        bool RedoButtonEnabled { get; set; }

        System.Drawing.Image Image { get; set; }

        EditorPresenter EditorPresenter { set; }

        void Show();
    }
}
