using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Interfaces;

namespace Zy.App.Common.Models
{
    public class ResourceBo : IResourceBo<long?>
    {
        public virtual long? Id { get; set; }

        public virtual int? RowVersion { get; set; }
    }
}
