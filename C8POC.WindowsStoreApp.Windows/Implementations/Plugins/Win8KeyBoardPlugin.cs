// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemWindowsKeyboardPlugin.cs" company="AlFranco">
//   Albert Rodriguez Franco 2013
// </copyright>
// <summary>
//   Implementation of a keyboard plugin using Windows Hooks
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace C8POC.Plugins.Keyboard.SystemWindowsKeyboard
{
    using System;
    using System.Collections.Generic;

    using C8POC.Interfaces.Domain.Plugins;

    /// <summary>
    ///     Implementation of a keyboard plugin using Windows Hooks
    /// </summary>
    public class Win8KeyBoardPlugin : IKeyboardPlugin
    {
        public event KeyDownEventHandler KeyDown;

        public event KeyStopEmulationEventHandler KeyStopEmulation;

        public event KeyUpEventHandler KeyUp;

        public void AboutPlugin()
        {
            // throw new NotImplementedException();
        }

        public IDictionary<string, string> Configure(IDictionary<string, string> currentConfiguration)
        {
            // throw new NotImplementedException();
            return new Dictionary<string, string>();
        }

        public void DisablePlugin()
        {
            // throw new NotImplementedException();
        }

        public void EnablePlugin(IDictionary<string, string> parameters)
        {
            // throw new NotImplementedException();
        }

        public IDictionary<string, string> GetDefaultPluginConfiguration()
        {
            // throw new NotImplementedException();
            return new Dictionary<string, string>();
        }
    }
}