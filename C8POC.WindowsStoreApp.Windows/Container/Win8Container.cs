// --------------------------------------------------------------------------------------------------------------------
// <copyright file="C8WindowsContainer.cs" company="AlFranco">
//   Albert Rodriguez Franco 2013
// </copyright>
// <summary>
//   Defines the C8WindowsContainer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace C8POC.WinFormsUI.Container
{
    using Autofac;

    using C8POC.Core.Domain.Engines;
    using C8POC.Core.Domain.Entities;
    using C8POC.Core.Domain.Services;
    using C8POC.Interfaces.Domain.Engines;
    using C8POC.Interfaces.Domain.Entities;
    using C8POC.Interfaces.Domain.Services;
    using C8POC.Interfaces.Infrastructure.Services;
    using C8POC.WindowsStoreApp.Implementations.Services;

    /// <summary>
    /// Container to resolve instances from the Windows Version of C8POC
    /// </summary>
    public class Win8Container : ContainerBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Win8Container"/> class.
        /// </summary>
        public Win8Container()
        {
            this.RegisterType<C8MachineState>().As<IMachineState>();
            this.RegisterType<Win8ConfigurationService>().As<IConfigurationService>();
            this.RegisterType<Win8PluginService>().As<IPluginService>();
            this.RegisterType<OpcodeProcessor>().As<IOpcodeProcessor>();
            this.RegisterType<Win8RomService>().As<IRomService>();
            this.RegisterType<OpcodeMapService>().As<IOpcodeMapService>();

            // Mediator
            this.RegisterType<EngineMediator>().As<IEngineMediator>();
            this.RegisterType<InputOutputEngine>().As<IInputOutputEngine>();
            this.RegisterType<ExecutionEngine>().As<IExecutionEngine>();
            this.RegisterType<Win8ConfigurationEngine>().As<IConfigurationEngine>();
        }
    }
}
