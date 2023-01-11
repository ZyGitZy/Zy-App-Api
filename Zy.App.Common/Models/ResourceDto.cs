using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Interfaces;

namespace Zy.App.Common.Models
{
    public class ResourceDto: IAppDto
    {
        public long? Id { get; set; }

        public int? RowVersion { get; set; }
    }
}
