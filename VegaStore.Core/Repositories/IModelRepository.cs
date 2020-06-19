using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VegaStore.Core.Entities;

namespace VegaStore.Core.Repositories
{
    public interface IModelRepository : IRepository<Model>
    {
        IEnumerable<Model> Models { get; }
        Task<Model> GetSingleModelAsync(int id, bool trackChanges);
    }
}
