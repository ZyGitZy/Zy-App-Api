using HealthChecks.MySql;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Zy.App.Common.Core.HealthCheckExtensions.HealthCheckExtensionOption;
using Zy.App.Common.Core.HealthCheckExtensions.IHealthCheckExtensionOption;

namespace Zy.App.Common.Core.HealthCheckExtensions
{
    public static class HealthCheckServiceExtensions
    {

        public static IZyHealthChecksBuilder AddHealthCheckMySQL(this IZyHealthChecksBuilder byzanMvcModuleBuilder, string connectionString)
        {
            return byzanMvcModuleBuilder.AddHealthCheckMySQL(connectionString, new ZyHealthCheckOptions());
        }

        public static IZyHealthChecksBuilder AddHealthCheckMySQL(this IZyHealthChecksBuilder byzanMvcModuleBuilder, string connectionString, ZyHealthCheckOptions options)
        {
            var healthChecksBuilder = byzanMvcModuleBuilder.HealthChecks;

            if (options.Tag == HealthCheckItemTags.Ignore || string.IsNullOrWhiteSpace(connectionString))
            {
                return byzanMvcModuleBuilder;
            }

            healthChecksBuilder.AddMySql(
                connectionString: connectionString,
                name: options.Name ?? "mysql",
                failureStatus: options.FailureStatus,
                tags: options.GetTags(),
                timeout: options.Timeout
                );

            return byzanMvcModuleBuilder;
        }


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
