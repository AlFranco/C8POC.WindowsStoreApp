using C8POC.Interfaces.Domain.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace C8POC.WindowsStoreApp
{
    interface IWin8SoundPlugin : ISoundPlugin
    {
        void InjectMediaAsset(MediaElement mediaElement);
    }
}
