using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VegaStore.Core.Repositories
{
    public interface IRepositoryManager
    {
        IMakeRepository Makes { get; }
        Task SaveChangesAsync();
    }
}
