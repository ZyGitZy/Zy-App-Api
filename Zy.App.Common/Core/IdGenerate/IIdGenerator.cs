using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zy.App.Common.Core.IdGenerate
{
    public interface IIdGenerator
    {
        long NewId();

        long NewId<T>();
    }
}
