using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zy.App.Common.Interfaces
{
    public interface IEntityAdditionColumns
    {
        long CreateByUserId { get; set; }

        DateTime CreateDateTime { get; set; }

        long LastUpdateByUserId { get; set; }

        DateTime LastUpdateDateTime { get; set; }
    }
}
