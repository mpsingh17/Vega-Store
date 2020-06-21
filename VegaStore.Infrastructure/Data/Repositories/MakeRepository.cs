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
    public class MakeRepository : Repository<Make>, IMakeRepository
    {
        public MakeRepository(EFCoreContext context)
            : base(context) { }

        public async Task<IEnumerable<Make>> GetAllMakesAsync(string userId, bool trackChanges)
        {
            return await FindByCondition(m => m.UserId.Equals(userId), trackChanges)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Make>> GetAllMakesAsync(bool trackChanges)
        {
            return await GetAll(trackChanges)
                .ToListAsync();
        }

        public async Task<Make> GetSingleMakeAsync(string userId, int makeId, bool trackChanges)
        {
            return await FindByCondition(
                m => m.UserId.Equals(userId) &&
                m.Id.Equals(makeId), trackChanges
            )
            .SingleOrDefaultAsync();
        }
        
        public async Task<Make> GetSingleMakeAsync(int makeId, bool trackChanges)
        {
            return await FindByCondition(
                m => m.Id.Equals(makeId),
                trackChanges
            )
            .SingleOrDefaultAsync();
        }
    }
}
