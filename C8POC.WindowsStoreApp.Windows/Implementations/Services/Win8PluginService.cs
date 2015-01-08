using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C8POC.WindowsStoreApp.Implementations.Services
{
    using C8POC.Interfaces.Domain.Plugins;
    using C8POC.Interfaces.Domain.Services;
    using C8POC.Plugins.Graphics.WPFPlugin;
    using C8POC.Plugins.Keyboard.SystemWindowsKeyboard;
    using C8POC.Plugins.Sound.ConsoleBeep;

    public class Win8PluginService : IPluginService
    {
        public T GetPluginByNameSpace<T>(string nameSpace) where T : class, IPlugin
        {
            Type type = typeof(T);
            IPlugin plugin = null;

            if (type == typeof(IGraphicsPlugin))
            {
                plugin = new Win8GraphicsPlugin();
            }
            else if (type == typeof(ISoundPlugin))
            {
                plugin = new Win8SoundPlugin();
            }
            else if (type == typeof(IKeyboardPlugin))
            {
                // plugin = new Win8KeyBoardPlugin();
            }

            return (T)plugin;            
        }

        public IDictionary<string, string> GetPluginConfiguration(IPlugin plugin)
        {
            return new Dictionary<string, string>();
        }

        public IEnumerable<Lazy<T, IPluginMetadata>> GetPluginsOfType<T>() where T : class, IPlugin
        {
            return new List<Lazy<T, IPluginMetadata>>();
        }

        public void SavePluginConfiguration(IDictionary<string, string> pluginConfiguration, IPlugin plugin)
        {
        }
    }
}
