using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Core.HealthCheckExtensions.IHealthCheckExtensionOption;

namespace Zy.App.Common.Core.HealthCheckExtensions.HealthCheckExtensionOption
{
    public class ZyHealthChecksBuilder : IZyHealthChecksBuilder
    {
        public ZyHealthChecksBuilder()
        {
        
        }

        public ZyHealthChecksBuilder(IHealthChecksBuilder healthChecksBuilder)
        {
            this.HealthChecks = healthChecksBuilder;
        }

        public IHealthChecksBuilder? HealthChecks { get; }
    }
}
