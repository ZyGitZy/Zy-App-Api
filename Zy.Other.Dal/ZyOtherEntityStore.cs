using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Interfaces;
using Zy.App.Common.StoreCore;

namespace Zy.Other.Dal
{
    public class ZyOtherEntityStore<TEntity> : EntityStore<TEntity>, IZyOtherEntityStore<TEntity> where TEntity : class, IEntity
    {
        public ZyOtherEntityStore(IZyAppContext zyAppContext, ZyOtherDbContext dbContext) : base(zyAppContext, dbContext)
        {
        }
    }
}
