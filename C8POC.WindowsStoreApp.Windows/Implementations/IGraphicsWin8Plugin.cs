using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C8POC.WindowsStoreApp.Implementations
{
    using System.Collections;

    using Windows.UI.Xaml.Controls;

    using C8POC.Interfaces.Domain.Plugins;
    using Windows.UI.Xaml;

    public interface IGraphicsCanvasWin8Plugin : IGraphicsPlugin
    {
        void InjectCanvasForDrawing(FrameworkElement canvas);

        void SetResolutionQuality(int quality);

        void InjectFramesPerSecondIntoElement(FrameworkElement element);
    }
}