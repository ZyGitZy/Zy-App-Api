using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Core.AppAbstractions.IAppAbstractionsOptions;
using Zy.App.Common.Core.DbContextExtension.ZyDbContextOptions;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Zy.App.Common.Core.HealthCheckExtensions;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Zy.App.Common.Core.DbContextExtension
{
    public static class DbContextBuilderExtension
    {
        public static IZyMvcModuleBuilder AddMysqlDbContext<TContext>(
          this IZyMvcModuleBuilder builder,
          Action<ZyDbContextOption> option,
          Action<DbContextOptionsBuilder>? builderOptionsAction = null)
             where TContext : DbContext
        {
            IServiceCollection services = builder.Services;

            var mySqlOptions = new ZyDbContextOption();
            option(mySqlOptions);

            void DbContextOptionsBuilderAction(DbContextOptionsBuilder options)
            {
                // Add EFCore Extentions
                CoreOptionsExtension coreOptionsExtension = options.Options.FindExtension<CoreOptionsExtension>() ?? new CoreOptionsExtension();

                ZyCoreOptionsExtension byzanCoreOptionsExtension = new(coreOptionsExtension);
                ((IDbContextOptionsBuilderInfrastructure)options).AddOrUpdateExtension(byzanCoreOptionsExtension);

                options.UseMySql(mySqlOptions!.GetConnectionString(), ServerVersion.AutoDetect(mySqlOptions.GetConnectionString()), opt => { opt.MigrationsAssembly(mySqlOptions.MigrationsAssembly); })
                    .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryPossibleUnintendedUseOfEqualsWarning));
                builderOptionsAction?.Invoke(options);
            }

            if (mySqlOptions.UseConnectionPool == true)
            {
                int poolSize = 128;
                if (mySqlOptions.ConnectionPoolSize > 0)
                {
                    poolSize = mySqlOptions.ConnectionPoolSize.Value;
                }

                services.AddDbContextPool<TContext>(DbContextOptionsBuilderAction, poolSize);
            }
            else
            {
                services.AddDbContext<TContext>(DbContextOptionsBuilderAction);
            }

            return builder;
        }
    }
}
