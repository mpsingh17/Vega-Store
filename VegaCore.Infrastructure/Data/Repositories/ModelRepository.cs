using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VegaCore.Infrastructure.Data;
using VegaStore.Core.Entities;
using VegaStore.Core.Repositories;

namespace VegaStore.Infrastructure.Data.Repositories
{
    public class ModelRepository : Repository<Model>, IModelRepository
    {
        public ModelRepository(EFCoreContext context) : base(context) {}

        public IEnumerable<Model> Models => GetAll(trackChanges: false).ToListAsync().Result;

        public async Task<Model> GetSingleModelAsync(int id, bool trackChanges)
        {
            return await FindByCondition(m => m.Id.Equals(id), trackChanges)
                .SingleOrDefaultAsync();
        }
    }
}
