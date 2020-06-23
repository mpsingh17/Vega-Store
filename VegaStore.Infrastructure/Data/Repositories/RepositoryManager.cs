using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VegaCore.Infrastructure.Data;
using VegaStore.Core.Entities;
using VegaStore.Core.Repositories;

namespace VegaStore.Infrastructure.Data.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly EFCoreContext _context;
        private MakeRepository _makeRepository;
        private ModelRepository _modelRepository;
        private FeatureRepository _featureRepository;
        private VehicleRepository _vehicleRepository;

        public RepositoryManager(EFCoreContext context)
        {
            _context = context;
        }

        public IMakeRepository Makes => _makeRepository ?? (_makeRepository = new MakeRepository(_context));
        
        public IModelRepository Models => _modelRepository ?? (_modelRepository = new ModelRepository(_context));
        
        public IFeatureRepository Features => _featureRepository ?? (_featureRepository = new FeatureRepository(_context));
        
        public IVehicleRepository Vehicles => _vehicleRepository ?? (_vehicleRepository = new VehicleRepository(_context));

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
