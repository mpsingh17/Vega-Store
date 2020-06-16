using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VegaStore.Core.Entities;

namespace VegaStore.Core.Repositories
{
    public interface IMakeRepository : IRepository<Make>
    {
        Task<IEnumerable<Make>> GetAllMakesAsync(string userId, bool trackChanges);
        Task<Make> GetSingleMakeAsync(string userId, int makeId, bool trackChanges);
    }
}
