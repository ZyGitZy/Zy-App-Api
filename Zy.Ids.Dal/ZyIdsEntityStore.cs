using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Interfaces;
using Zy.App.Common.StoreCore;

namespace Zy.Ids.Dal
{
    public class ZyIdsEntityStore<TEntity> : EntityStore<TEntity>, IZyIdsEntityStore<TEntity> where TEntity : class, IEntity
    {
        public ZyIdsEntityStore(IZyAppContext zyAppContext, ZyIdsDbContext dbContext) : base(zyAppContext, dbContext)
        {
        }
    }
}
