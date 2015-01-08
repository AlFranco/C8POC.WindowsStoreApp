using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace C8POC.WindowsStoreApp
{
    using Autofac;
using C8POC.Interfaces.Domain.Engines;
using C8POC.WindowsStoreApp.Implementations;
using C8POC.WinFormsUI.Container;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private IEngineMediator engineMediator;
        private Dictionary<int, byte> keyMap = new Dictionary<int, byte>();

        public MainPage()
        {
            this.InitializeComponent();
            this.SetKeyMapper();

            // Intercept KeyUp/Down events
            Window.Current.Content.AddHandler(UIElement.KeyDownEvent, new KeyEventHandler(MainPage_KeyDown), true);
            Window.Current.Content.AddHandler(UIElement.KeyUpEvent, new KeyEventHandler(MainPage_KeyUp), true);

            this.CreateEngine();
        }

        void MainPage_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (keyMap.Any(x => x.Key == (int)e.Key))
            {
                this.engineMediator.InputOutputEngine.KeyDown(keyMap[(int)e.Key]);
            }
        }

        private void MainPage_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (keyMap.Any(x => x.Key == (int)e.Key))
            {
                this.engineMediator.InputOutputEngine.KeyUp(keyMap[(int)e.Key]);
            }
        }

        private void SetKeyMapper()
        {
            this.keyMap.Add(49, 0x0);
            this.keyMap.Add(50, 0x1);
            this.keyMap.Add(51, 0x2);
            this.keyMap.Add(52, 0x3);
            this.keyMap.Add(81, 0x4);
            this.keyMap.Add(87, 0x5);
            this.keyMap.Add(69, 0x6);
            this.keyMap.Add(82, 0x7);
            this.keyMap.Add(65, 0x8);
            this.keyMap.Add(83, 0x9);
            this.keyMap.Add(68, 0xA);
            this.keyMap.Add(70, 0xB);
            this.keyMap.Add(90, 0xC);
            this.keyMap.Add(88, 0xD);
            this.keyMap.Add(67, 0xE);
            this.keyMap.Add(86, 0xF);
        }

        private void CreateEngine()
        {
            var builder = new Win8Container();
            var container = builder.Build();

            this.engineMediator = container.Resolve<IEngineMediator>();
            this.engineMediator.StartPluginsExecution();

            this.InjectCanvasIntoGraphicsPlugin();
            this.InjectMediaElementIntoSoundPlugin();
        }

        private void InjectCanvasIntoGraphicsPlugin()
        {
            var pluginforcanvas = this.engineMediator.InputOutputEngine.SelectedGraphicsPlugin as IGraphicsCanvasWin8Plugin;
            pluginforcanvas.InjectCanvasForDrawing(this.source);
            pluginforcanvas.InjectFramesPerSecondIntoElement(this.TextBoxFPS);
        }

        private void InjectMediaElementIntoSoundPlugin()
        {
            var pluginforcanvas = this.engineMediator.InputOutputEngine.SelectedSoundPlugin as IWin8SoundPlugin;
            pluginforcanvas.InjectMediaAsset(this.bleep);
        }

        private async void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            await this.OpenFile();
        }

        private async Task OpenFile()
        {
            //Open a file
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            openPicker.FileTypeFilter.Add(".c8");

            StorageFile file = await openPicker.PickSingleFileAsync();

            if (file != null)
            {
                this.TextBoxInformation.Text = file.DisplayName;
                await this.engineMediator.LoadRomToEngine(file);
                this.engineMediator.StartEmulation();
            }
        }

        private void DropDownName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.engineMediator == null)
            {
                return;
            }

            var graphicsPlugin = this.engineMediator.InputOutputEngine.SelectedGraphicsPlugin as IGraphicsCanvasWin8Plugin;

            var selectedQualityvalue = 0;

            switch (DropDownQuality.SelectedIndex)
            {
                case 0:
                    selectedQualityvalue = 4;
                    break;
                case 1:
                    selectedQualityvalue = 8;
                    break;
                case 2:
                    selectedQualityvalue = 16;
                    break;
                case 3:
                    selectedQualityvalue = 32;
                    break;
                default:
                    selectedQualityvalue = 4;
                    break;
            }

            graphicsPlugin.SetResolutionQuality(selectedQualityvalue);
        }
    }
}
