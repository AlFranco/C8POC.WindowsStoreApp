using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C8POC.WindowsStoreApp.Implementations.Services
{
    using System.IO;
    using System.Runtime.InteropServices.WindowsRuntime;

    using Windows.Storage;
    using Windows.Storage.Streams;

    using C8POC.Core.Infrastructure;
    using C8POC.Interfaces.Domain.Entities;
    using C8POC.Interfaces.Infrastructure.Services;

    internal class Win8RomService : IRomService
    {
        public async Task LoadRom(StorageFile romFile, IMachineState machineState)
        {
            using (var stream = await romFile.OpenStreamForReadAsync())
            {
                if (stream.Length == 0)
                {
                    throw new Exception("File empty or damaged");
                }

                for (int index = 0; index < (int)stream.Length; index++)
                {
                    machineState.Memory[C8Constants.StartRomAddress + index] = (byte)stream.ReadByte();
                }
            }
        }
    }
}

