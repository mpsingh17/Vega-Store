﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VegaStore.Core.Repositories
{
    public interface IRepositoryManager
    {
        IMakeRepository Makes { get; }
        IModelRepository Models { get; }
        IFeatureRepository Features { get; }
        IVehicleRepository Vehicles { get; }
        IFileOnFileSystemRepository FilesOnFileSystem { get; }

        Task SaveAsync();
    }
}
