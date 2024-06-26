using System.Windows.Forms;

namespace MyPhoto.Ui
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();            

            var welcomeView = new WelcomeForm();
            var state = new ImageEditorState();
            var welcomePresenter = new WelcomePresenter(welcomeView, state);

            Application.Run(welcomeView);
        }
    }
}