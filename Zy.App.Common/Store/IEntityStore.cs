using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Zy.App.Common.StoreCore
{
    public interface IEntityStore<TEntity>
    {
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>>? query = null);

        ValueTask<TEntity?> FindAsync(CancellationToken cancellationToke = default, params object[] keyValues);

        Task<int> DeleteSaveAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task<int> UpdateSaveAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task<TEntity> CreateSaveAsync(TEntity entity, CancellationToken cancellationToken = default);

        void Delete(TEntity entity);

        void Update(TEntity entity);

        TEntity Create(TEntity entity);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
