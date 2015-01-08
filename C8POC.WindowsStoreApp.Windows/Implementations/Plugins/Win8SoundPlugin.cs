// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleBeep.cs" company="AlFranco">
//   Albert Rodriguez Franco 2013
// </copyright>
// <summary>
//   Early implementation of a Sound Class
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace C8POC.Plugins.Sound.ConsoleBeep
{
    using System;
    using System.Collections.Generic;

    using C8POC.WindowsStoreApp;
    using Windows.ApplicationModel.Core;
    using Windows.UI.Core;

    /// <summary>
    ///     Early implementation of a Sound Class
    /// </summary>
    public class Win8SoundPlugin : IWin8SoundPlugin
    {
        #region Public Methods and Operators

        /// <summary>
        ///     About plugin implementation
        /// </summary>
        public void AboutPlugin()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Configuration for plugin
        /// </summary>
        /// <param name="currentConfiguration">
        /// The current Configuration.
        /// </param>
        /// <returns>
        /// Resulting configuration
        /// </returns>
        public IDictionary<string, string> Configure(IDictionary<string, string> currentConfiguration)
        {
            return null;
        }

        /// <summary>
        ///     Disable plugin Action
        /// </summary>
        public void DisablePlugin()
        {
        }

        /// <summary>
        /// Enables the plugin with specified parameters
        /// </summary>
        /// <param name="parameters">
        /// Parameters for plugin
        /// </param>
        public void EnablePlugin(IDictionary<string, string> parameters)
        {
        }

        /// <summary>
        ///     Action to generate sound
        /// </summary>
        public async void GenerateSound()
        {
            // Before was invoke
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High,
            () =>
            {
                this.mediaElement.Play();// Your UI update code goes here!
            });
            
        }

        /// <summary>
        ///     Gets the default configuration of the plugin
        /// </summary>
        /// <returns>Default configuration for plugin</returns>
        public IDictionary<string, string> GetDefaultPluginConfiguration()
        {
            return null;
        }

        #endregion

        public void InjectMediaAsset(Windows.UI.Xaml.Controls.MediaElement mediaElement)
        {
            this.mediaElement = mediaElement;
        }

        public Windows.UI.Xaml.Controls.MediaElement mediaElement { get; set; }
    }
}