using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Zy.App.Common.Core.DbContextExtension;
using Zy.App.Common.Core.IdGenerate;
using Zy.App.Common.Interfaces;

namespace Zy.App.Common.StoreCore
{
    public class EntityStore<TEntity> : IEntityStore<TEntity> where TEntity : class, IEntity
    {
        private readonly DbContext dbContext;

        private readonly DbSet<TEntity> dataSet;

        public EntityStore(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dataSet = this.dbContext.Set<TEntity>();
        }

        public EntityStore(IZyAppContext zyAppContext, DbContextBase dbContext)
        {
            this.dbContext = dbContext;
            dbContext.SetByzanContext(zyAppContext);
            this.dataSet = this.dbContext.Set<TEntity>();
        }

        public TEntity Create(TEntity entity)
        {
            if (entity is IEntity<long> idEntity && idEntity.Id == 0)
            {
                idEntity.Id = IdGenerator.NewId<TEntity>();
            }

            this.dataSet.Add(entity);

            return entity;
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? query = null)
        {
            if (query != null)
            {
                return await this.dataSet.AnyAsync(query);
            }

            return await this.dataSet.AnyAsync();
        }

        public bool Any(Expression<Func<TEntity, bool>>? query = null)
        {
            if (query != null)
            {
                return this.dataSet.Any(query);
            }

            return this.dataSet.Any();
        }

        public async Task<TEntity> CreateSaveAsync(TEntity entity, CancellationToken cancellationToken)
        {
            this.Create(entity);
            await this.dbContext.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public void Delete(TEntity entity)
        {
            if (entity is not IEntityLogicDelete entityLogicDelete)
            {
                this.dataSet.Remove(entity);
            }
            else
            {
                entityLogicDelete.IsDeleted = true;
                this.dataSet.Update(entity);
            }
        }

        public async Task<int> DeleteSaveAsync(TEntity entity, CancellationToken cancellationToken)
        {
            this.Delete(entity);
            return await this.dbContext.SaveChangesAsync(cancellationToken);
        }

        public ValueTask<TEntity?> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return this.dataSet.FindAsync(keyValues, cancellationToken);
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>>? query = null)
        {
            if (query != null)
            {
                return this.dataSet.Where(query);
            }
            return dataSet;
        }

        public void Update(TEntity entity)
        {

        }

        public async Task<int> UpdateSaveAsync(TEntity entity, CancellationToken cancellationToken)
        {
            return await this.dbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return this.dbContext.SaveChangesAsync(cancellationToken);
        }

    }
}
