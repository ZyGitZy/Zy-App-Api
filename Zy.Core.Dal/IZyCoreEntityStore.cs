using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Interfaces;
using Zy.App.Common.StoreCore;

namespace Zy.Core.Dal
{
    public interface IZyCoreEntityStore<Entity> : IEntityStore<Entity> where Entity : class, IEntity
    {
    }
}
