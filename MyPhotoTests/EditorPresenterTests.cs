using MyPhoto.Core;
using MyPhoto.Ui;
using Moq;
using MyPhoto.Utils;
using MyPhoto.Properties;
using System.Windows.Forms.PropertyGridInternal;
using Castle.Components.DictionaryAdapter.Xml;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Diagnostics.Metrics;

namespace MyPhoto.Tests
{
    public class EditorPresenterTests
    {
        private readonly IEditorView mockEditorView;

        private readonly FiltersManager mockFiltersManager;

        private readonly History mockHistory;

        private readonly ImageOriginator mockOriginator;

        private readonly ImageEditorState mockState;

        private readonly EditorPresenter mockEditorPresenter;

        public EditorPresenterTests() 
        {
            var mockImage = MyPhoto.Properties.Resources.WelcomeScreen_cropped;
            mockEditorView = Mock.Of<IEditorView>();
            
            mockEditorView.Image = (System.Drawing.Image)mockImage.Clone();
            mockState = new ImageEditorState();
            mockState.image = (System.Drawing.Image)mockImage.Clone();

            var mockCommandQueue = new CommandQueue();
            mockFiltersManager = new FiltersManager(mockCommandQueue);

            mockHistory = new History(new ImageMemento(mockImage));
            mockOriginator = new ImageOriginator(mockImage);

            mockEditorPresenter = new EditorPresenter(mockEditorView, mockState, mockFiltersManager, mockHistory, mockOriginator);          
        }

        private bool CompareTwoImages(in System.Drawing.Bitmap image1, in System.Drawing.Bitmap image2)
        {
            System.Drawing.Color pixel1;
            System.Drawing.Color pixel2;
            if (image1.Width == image2.Width && image1.Height == image2.Height)
            {
                for (int i = 0; i < image1.Width; i++)
                {
                    for (int j = 0; j < image1.Height; j++)
                    {
                        pixel1 = image1.GetPixel(i, j);
                        pixel2 = image2.GetPixel(i, j);
                        if (pixel1 != pixel2)
                        {
                            return false;
                        }
                    }
                }
            }
            else 
                return false;

            return true;
        }

        [Fact]
        public void Presenter_UndoButtonEnabled_ShouldDisableUndoButton()
        {
            // Should disable Undo Button when the program was just opened
            Assert.False(mockEditorView.UndoButtonEnabled);
        }

        [Fact]
        public void Presenter_RedoButtonEnabled_ShouldDisableRedoButton()
        {
            // Should disable Redo Button when the program was just opened
            Assert.False(mockEditorView.RedoButtonEnabled);
        }

        [Fact]
        public void Presenter_UndoButtonEnabled_ShouldEnableUndoButton()
        {
            mockEditorPresenter.ApplyBlue();

            Assert.True(mockEditorView.UndoButtonEnabled);
        }

        [Fact]
        public void Presenter_RedoButtonEnabled_ShouldEnableRedoButton()
        {
            mockEditorPresenter.ApplyBlue();
            mockEditorPresenter.Undo();

            Assert.True(mockEditorView.RedoButtonEnabled);
        }

        [Fact]
        public void Presenter_PictureBoxDown_ShouldShowUnmodifiedPicture()
        {
            mockEditorPresenter.ShowInitialImage();           
            var image = mockHistory.GetInitialMemento().GetSavedImage(); // Get the unmodified image

            Assert.True(CompareTwoImages((System.Drawing.Bitmap)image, (System.Drawing.Bitmap)mockEditorView.Image));
        }

        [Fact]
        public void Presenter_ResetAll_ShouldShowImageWithoutFilters()
        {
            mockEditorPresenter.ResetAll();
            var image = mockHistory.GetInitialMemento().GetSavedImage(); // Get the unmodified image

            Assert.True(CompareTwoImages((System.Drawing.Bitmap)image, (System.Drawing.Bitmap)mockEditorView.Image));
        }

        [Fact]
        public void Presenter_ChangeBrightness_ShouldShowCorrectBrightnessValue()
        {
            mockEditorView.Brightness = 50;
            mockEditorPresenter.ApplyBrightness();

            Assert.True(mockEditorView.Brightness == mockFiltersManager.GetValue(FiltersLibrary.Filter.BRIGHTNESS));
        }

        [Fact]
        public void Presenter_ChangeContrast_ShouldShowCorrectContrastValue()
        {
            mockEditorView.Contrast = 50;
            mockEditorPresenter.ApplyContrast();

            Assert.True(mockEditorView.Contrast == mockFiltersManager.GetValue(FiltersLibrary.Filter.CONTRAST));
        }

        [Fact]
        public void Presenter_ChangeSaturation_ShouldShowCorrectSaturationValue()
        {
            mockEditorView.Saturation = 50;
            mockEditorPresenter.ApplySaturation();

            Assert.True(mockEditorView.Saturation == mockFiltersManager.GetValue(FiltersLibrary.Filter.SATURATION));
        }

        [Fact]
        public void Presenter_ChangeHue_ShouldShowCorrectHueValue()
        {
            mockEditorView.Hue = 50;
            mockEditorPresenter.ApplyHue();

            Assert.True(mockEditorView.Hue == mockFiltersManager.GetValue(FiltersLibrary.Filter.HUE));
        }

        [Fact]
        public void Presenter_ChangeRedChannel_ShouldShowCorrectRedChannelValue()
        {
            mockEditorView.RedChannel = 50;
            mockEditorPresenter.ApplyRedChannel();

            Assert.True(mockEditorView.RedChannel == mockFiltersManager.GetValue(FiltersLibrary.Filter.RED_CHANNEL));
        }

        [Fact]
        public void Presenter_ChangeGreenChannel_ShouldShowCorrectGreenChannelValue()
        {
            mockEditorView.GreenChannel = 50;
            mockEditorPresenter.ApplyGreenChannel();

            Assert.True(mockEditorView.GreenChannel == mockFiltersManager.GetValue(FiltersLibrary.Filter.GREEN_CHANNEL));
        }

        [Fact]
        public void Presenter_ChangeBlueChannel_ShouldShowCorrectBlueChannelValue()
        {
            mockEditorView.BlueChannel = 50;
            mockEditorPresenter.ApplyBlueChannel();

            Assert.True(mockEditorView.BlueChannel == mockFiltersManager.GetValue(FiltersLibrary.Filter.BLUE_CHANNEL));
        }

        [Fact]
        public void Presenter_ChangeNegative_ShouldShowCorrectNegativeValue()
        {
            mockEditorView.Negative = 1;
            mockEditorPresenter.ApplyNegative();

            Assert.True(mockEditorView.Negative == mockFiltersManager.GetValue(FiltersLibrary.Filter.NEGATIVE));
        }

        [Fact]
        public void Presenter_ChangeSepia_ShouldShowCorrectSepiaValue()
        {
            mockEditorView.Sepia = 1;
            mockEditorPresenter.ApplySepia();

            Assert.True(mockEditorView.Sepia == mockFiltersManager.GetValue(FiltersLibrary.Filter.SEPIA));
        }
    }
};