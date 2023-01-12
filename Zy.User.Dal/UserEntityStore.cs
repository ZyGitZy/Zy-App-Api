using Zy.App.Common.Core.DbContextExtension;
using Zy.App.Common.Interfaces;
using Zy.App.Common.StoreCore;

namespace Zy.User.Dal
{
    public class UserEntityStore<TEntity> : EntityStore<TEntity>, IEntityStore<TEntity> where TEntity : class, IEntity
    {
        public UserEntityStore(DbContextBase dbContext) : base(dbContext)
        {
        }
    }
}
