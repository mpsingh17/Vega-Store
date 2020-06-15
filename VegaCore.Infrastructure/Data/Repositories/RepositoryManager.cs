using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VegaCore.Infrastructure.Data;
using VegaStore.Core.Repositories;

namespace VegaStore.Infrastructure.Data.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly EFCoreContext _context;
        private MakeRepository _makeRepository;

        public RepositoryManager(EFCoreContext context)
        {
            _context = context;
        }

        public IMakeRepository Makes => _makeRepository ?? (_makeRepository = new MakeRepository(_context));

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
