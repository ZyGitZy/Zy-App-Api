using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Interfaces;
using Zy.App.Common.StoreCore;

namespace Zy.Ids.Dal
{
    public interface IZyIdsEntityStore<TEntity> : IEntityStore<TEntity> where TEntity : class, IEntity
    {
    }
}
