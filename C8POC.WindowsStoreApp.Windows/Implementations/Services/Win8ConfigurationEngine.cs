using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C8POC.WindowsStoreApp.Implementations.Services
{
    using C8POC.Interfaces.Domain.Engines;
    using C8POC.Interfaces.Infrastructure.Services;

    class Win8ConfigurationEngine : IConfigurationEngine
    {
        public IEngineMediator EngineMediator { get; set; }

        public void SetMediator(IEngineMediator engineMediator)
        {
            this.EngineMediator = engineMediator;
        }

        public IConfigurationService ConfigurationService { get; set; }

        public T GetConfigurationKeyOfType<T>(string configurationKey)
        {
            if (configurationKey == "FramesPerSecond" && typeof(T) == typeof(int))
                return (T)(object)60;

            if (configurationKey == "CyclesPerFrame" && typeof(T) == typeof(int))
                return (T)(object)10;

            return default(T);
        }
    }
}
