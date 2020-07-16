using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VegaStore.Core.Entities;

namespace VegaStore.Core.Repositories
{
    public interface IFileOnFileSystemRepository : IRepository<FileOnFileSystem>
    {
        Task<IEnumerable<FileOnFileSystem>> GetAllFilesOnFileSystemAsync(bool trackChanges);
        Task<FileOnFileSystem> GetSingleFileOnFileSystemAsync(int id, bool trackChanges);
    }
}
