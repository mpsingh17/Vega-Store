using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VegaCore.Infrastructure.Data;
using VegaStore.Core.Entities;
using VegaStore.Core.Repositories;

namespace VegaStore.Infrastructure.Data.Repositories
{
    public class FeatureRepository : Repository<Feature>, IFeatureRepository
    {
        public FeatureRepository(EFCoreContext context)
            : base(context) {}

        public async Task<IEnumerable<Feature>> GetAllFeaturesAsync(bool trackChanges)
        {
            return await GetAll(trackChanges)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Feature>> GetAllFeaturesAsync(IEnumerable<int> ids, bool trackChanges)
        {
            return await FindByCondition(f => ids.Contains(f.Id), trackChanges)
                .ToListAsync();
        }

        public async Task<Feature> GetSingleFeatureAsync(int id, bool trackChanges)
        {
            return await FindByCondition(f => f.Id.Equals(id), trackChanges)
                .SingleOrDefaultAsync();
        }
    }
}
