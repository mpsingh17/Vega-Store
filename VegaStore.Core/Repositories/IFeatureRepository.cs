using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VegaStore.Core.Entities;

namespace VegaStore.Core.Repositories
{
    public interface IFeatureRepository : IRepository<Feature>
    {
        Task<IEnumerable<Feature>> GetAllFeaturesAsync(bool trackChanges);
        Task<IEnumerable<Feature>> GetAllFeaturesAsync(IEnumerable<int> ids, bool trackChanges);
        Task<Feature> GetSingleFeatureAsync(int id, bool trackChanges);
    }
}
