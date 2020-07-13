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
    public class FileOnFileSystemRepository : Repository<FileOnFileSystem>, IFileOnFileSystemRepository
    {
        public FileOnFileSystemRepository(EFCoreContext context)
            :  base(context) {}

        public async Task<IEnumerable<FileOnFileSystem>> GetAllFilesOnFileSystemAsync(bool trackChanges)
        {
            return await GetAll(trackChanges).ToListAsync();
        }
    }
}
