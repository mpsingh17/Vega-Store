using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VegaCore.Infrastructure.Data;
using VegaStore.Core.Repositories;

namespace VegaStore.Infrastructure.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly EFCoreContext Context;
        private readonly DbSet<TEntity> _entities;

        public Repository(EFCoreContext context)
        {
            Context = context;
            _entities = Context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll(bool trackChanges)
        {
            return trackChanges ? _entities : _entities.AsNoTracking();
        }

        public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression, bool trackChanges)
        {
            return (trackChanges) ? _entities.Where(expression) : _entities.AsNoTracking().Where(expression);
        }

        public async Task AddAsync(TEntity entity) => await _entities.AddAsync(entity);


        public async Task AddRangeAsync(IEnumerable<TEntity> entities) => await _entities.AddRangeAsync(entities);

        public void Remove(TEntity entity) => _entities.Remove(entity);

        public void RemoveRange(IEnumerable<TEntity> entities) => _entities.RemoveRange(entities);
    }
}
