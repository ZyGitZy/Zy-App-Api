using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Core.HealthCheckExtensions.HealthCheckExtensionOption;
using Zy.App.Common.Core.HealthCheckExtensions.IHealthCheckExtensionOption;

namespace Zy.App.Common.Core.HealthCheckExtensions
{
    public static class HealthCheckServiceExtensions
    {
        public static IZyHealthChecksBuilder AddZyHealthCheckService(this IServiceCollection services)
        {
            return services.AddZyHealthCheckService(option => { });
        }

        public static IZyHealthChecksBuilder AddZyHealthCheckService(this IServiceCollection services, Action<ZyHealthCheckOptions> action)
        {
            ZyHealthCheckOptions zyHealthChecksBuilder = new();
            action(zyHealthChecksBuilder);
            services.Configure(action);

            if (!zyHealthChecksBuilder.Open)
            {
                return new ZyHealthChecksBuilder();
            }

            var healthChecks = services.AddHealthChecks();

            var builder = new ZyHealthChecksBuilder(healthChecks);

            return builder;
        }
    }
}
