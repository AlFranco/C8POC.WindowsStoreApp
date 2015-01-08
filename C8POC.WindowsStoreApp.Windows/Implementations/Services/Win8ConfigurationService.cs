using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C8POC.WindowsStoreApp.Implementations.Services
{
    using C8POC.Interfaces.Infrastructure.Services;

    class Win8ConfigurationService : IConfigurationService
    {
        public IDictionary<string, string> GetEngineConfiguration()
        {
            return new Dictionary<string, string>();
        }

        public void SaveEngineConfiguration()
        {
        }
    }
}
