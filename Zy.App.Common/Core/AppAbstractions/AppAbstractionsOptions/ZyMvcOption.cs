using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Core.HealthCheckExtensions.HealthCheckExtensionOption;

namespace Zy.App.Common.Core.AppAbstractions.AppAbstractionsOptions
{
    public class ZyMvcOption
    {
        public ZyHealthCheckOptions HealthCheckOption { get; set; }

        public ZyMvcOption() 
        {
            this.HealthCheckOption = new ZyHealthCheckOptions();
        }
    }
}
