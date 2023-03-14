using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.AppExtensions;
using Zy.App.Common.Core.AppAbstractions.AppAbstractionsOptions;
using Zy.App.Common.Core.AppAbstractions.IAppAbstractionsOptions;
using Zy.App.Common.Core.HealthCheckExtensions;

namespace Zy.App.Common.Core.App.Abstractions
{
    public static class ZyAppExtension
    {
        public static IZyMvcBuilder AddZyMvcBuilder(this IServiceCollection services)
        {
            return services.AddZyMvcBuilder(a => { });
        }

        public static IZyMvcBuilder AddZyMvcBuilder(this IServiceCollection services, Action<ZyMvcOption> action)
        {
            var mvcOption = new ZyMvcOption();

            action(mvcOption);

            var mvcCore = services.AddMvcCore().AddApiExplorer().AddCors();

            var healthCheck = services.AddZyHealthCheckService(opt => opt.Apply(mvcOption.HealthCheckOption));

            var builder = new ZyMvcBuilder(mvcCore, services, healthCheck.healthChecks);

            return builder;
        }

        public static IZyMvcBuilder BuildModules(this IZyMvcBuilder mvcBuilder)
        {
            var autoMapperAssmeblys = mvcBuilder.Modules.SelectMany(s => s.AutoMappers).Distinct(new AssemblyEqualityComparer());

            var controllerAssmeblys = mvcBuilder.Modules.SelectMany(s => s.Controllers).Distinct(new AssemblyEqualityComparer()); ;

            mvcBuilder.Services.AddAutoMapperModule(autoMapperAssmeblys);

            foreach (var item in controllerAssmeblys)
            {
                mvcBuilder.MvcBuilder.AddApplicationPart(item);
            }

            return mvcBuilder;

        }
        private class AssemblyEqualityComparer : IEqualityComparer<Assembly>
        {
            public bool Equals([AllowNull] Assembly x, [AllowNull] Assembly y)
            {
                return x == y;
            }

            public int GetHashCode([DisallowNull] Assembly obj)
            {
                return obj.GetHashCode();
            }
        }
    }

}
