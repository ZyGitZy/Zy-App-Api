using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Core.DbContextExtension;
using Zy.App.Common.Interfaces;
using Zy.App.Common.StoreCore;

namespace Zy.Core.Dal
{
    public class ZyCoreEntityStore<Entity> : EntityStore<Entity>, IZyCoreEntityStore<Entity> where Entity : class, IEntity
    {
        public ZyCoreEntityStore(IZyAppContext zyAppContext, DbContextBase dbContext) : base(zyAppContext, dbContext)
        {
        }
    }
}
