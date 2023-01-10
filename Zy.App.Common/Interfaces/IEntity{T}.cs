using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zy.App.Common.Interfaces
{
    public interface IEntity<Tkey> : IEntity
    {
        Tkey Id { get; set; }
    }
}
