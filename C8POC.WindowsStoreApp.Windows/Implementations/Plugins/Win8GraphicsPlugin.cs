// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WpfPlugin.cs" company="AlFranco">
//   Albert Rodriguez Franco 2013
// </copyright>
// <summary>
//   Graphics plugin with GDI primitives for Windows Forms
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace C8POC.Plugins.Graphics.WPFPlugin
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices.WindowsRuntime;

    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media.Imaging;

    using C8POC.Core.Infrastructure;
    using C8POC.Interfaces.Domain.Plugins;
    using Windows.UI.Xaml.Media;
    using Windows.UI;

    using C8POC.WindowsStoreApp;
    using Windows.UI.Xaml.Shapes;

    using C8POC.WindowsStoreApp.Implementations;
    using System.Threading.Tasks;
    using Windows.ApplicationModel.Core;
    using Windows.UI.Core;
    using Windows.UI.Xaml.Media.Imaging;
    using Windows.UI.Xaml;
    using System.Diagnostics;


    /// <summary>
    /// Graphics plugin using WPF
    /// </summary>
    public class Win8GraphicsPlugin : IGraphicsCanvasWin8Plugin
    {
        #region Properties

        /// <summary>
        /// The brush.
        /// </summary>
        private readonly Brush brush = new SolidColorBrush(Colors.White);

        /// <summary>
        /// From that will be displayed when the plugin is activated
        /// </summary>
        private Image graphicsCanvas;

        /// <summary>
        /// Gets or sets a value indicating whether the form has been closed by code
        /// </summary>
        private bool FormClosedByCode { get; set; }

        /// <summary>
        /// Gets the graphics buffer
        /// </summary>
        private BitArray graphicsBuffer;

        private WriteableBitmap bitmap;

        private int quality = 8;

        private TextBlock fpsTextBlock;

        #endregion

        #region IGraphicsPlugin Members

        /// <summary>
        /// Prompts for a configuration
        /// </summary>
        public void Configure()
        {
        }

        /// <summary>
        /// Gets the about plugin window
        /// </summary>
        public void AboutPlugin()
        {
        }

        /// <summary>
        /// The configure.
        /// </summary>
        /// <param name="currentConfiguration">
        /// The current configuration.
        /// </param>
        /// <returns>
        /// The <see cref="IDictionary"/>.
        /// </returns>
        public IDictionary<string, string> Configure(IDictionary<string, string> currentConfiguration)
        {
            return null;
        }

        /// <summary>
        /// Disables de plugin
        /// </summary>
        public void DisablePlugin()
        {
        }

        /// <summary>
        /// The enable plugin.
        /// </summary>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        public void EnablePlugin(IDictionary<string, string> parameters)
        {
        }

        /// <summary>
        /// The get default plugin configuration.
        /// </summary>
        /// <returns>
        /// The <see cref="IDictionary"/>.
        /// </returns>
        public IDictionary<string, string> GetDefaultPluginConfiguration()
        {
            return null;
        }

        /// <summary>
        /// Drawing implementation for the plugin
        /// </summary>
        /// <param name="graphics">The graphics array</param>
        public void Draw(BitArray graphics)
        {
            MemoryStream ms = this.Draw1(graphics);
            
            // Before was invoke
            CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High,
            () =>
            {
               Stopwatch value = Stopwatch.StartNew();
                DrawStream(ms);
               value.Stop();
               if (value.ElapsedMilliseconds > 0)
               {
                   fpsTextBlock.Text = (1000 / value.ElapsedMilliseconds).ToString();
               }
                // Your UI update code goes here!
            });

        }

        public void InjectCanvasForDrawing(FrameworkElement canvas)
        {
            bitmap = new WriteableBitmap(C8Constants.ResolutionWidth, C8Constants.ResolutionHeight);
            this.graphicsCanvas = (Image)canvas;
            // this.graphicsCanvas.Source = bitmap;
            this.graphicsCanvas.SizeChanged += graphicsCanvas_SizeChanged;
        }

        private void graphicsCanvas_SizeChanged(object sender, Windows.UI.Xaml.SizeChangedEventArgs e)
        {
            if (this.graphicsBuffer != null)
            { this.Draw(graphicsBuffer); }
        }

        private void DrawStream(Stream value)
        {
            using (Stream stream = bitmap.PixelBuffer.AsStream())
            {
                using (value)
                {
                    value.Position = 0;
                    byte[] buff = new byte[value.Length];
                    value.Read(buff, 0, buff.Length);
                    stream.Write(buff, 0, buff.Length);
                }
            }
            bitmap.Invalidate();

            this.graphicsCanvas.Source = bitmap.Resize(
                (int)(this.quality * C8Constants.ResolutionWidth),
                (int)(this.quality *  C8Constants.ResolutionHeight),
                Windows.UI.Xaml.Media.Imaging.WriteableBitmapExtensions.Interpolation.NearestNeighbor);
        }

        private MemoryStream Draw1(BitArray graphics)
        {
            MemoryStream stream = new MemoryStream();

            for (int i = 0; i < graphics.Length; i++)
            {
                bool isWhite = graphics[i];
                Color color = Colors.White;
                if (!isWhite)
                {
                    color = Colors.Black;
                }

                stream.WriteByte(color.R);
                stream.WriteByte(color.G);
                stream.WriteByte(color.B);
                stream.WriteByte(color.A);

            }

            return stream;
        }

        /// <summary>
        /// Event fired when the graphics form closes
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void GraphicsFormClosed(object sender, EventArgs e)
        {
            if (!this.FormClosedByCode && this.GraphicsExit != null)
            {
                this.FormClosedByCode = false;
                this.GraphicsExit();
            }
        }

        /// <summary>
        /// The graphics exit.
        /// </summary>
        public event GraphicsExitEventHandler GraphicsExit;

        #endregion

        #region Other methods
        /// <summary>
        /// Gets the state of a pixel, take into account that
        /// screen starts at upper left corner (0,0) and ends at lower right corner (63,31)
        /// </summary>
        /// <param name="graphics">
        /// The graphics.
        /// </param>
        /// <param name="x">
        /// Horizontal position
        /// </param>
        /// <param name="y">
        /// Vertical positionB
        /// </param>
        /// <returns>
        /// If the pixel set or not
        /// </returns>
        private static bool GetPixelState(BitArray graphics, int x, int y)
        {
            return graphics[x + (C8Constants.ResolutionWidth * y)]; //C8Constants.ResolutionWidth is the resolution width of the screen
        }

        #endregion


        public void SetResolutionQuality(int quality)
        {
            this.quality = quality;
        }


        public void InjectFramesPerSecondIntoElement(FrameworkElement element)
        {
            this.fpsTextBlock = (TextBlock)element;
        }
    }
}
