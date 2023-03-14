using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Core.HealthCheckExtensions.IHealthCheckExtensionOption;

namespace Zy.App.Common.Core.AppAbstractions.IAppAbstractionsOptions
{
    public interface IZyMvcModuleBuilder : IZyBuilder, IZyHealthChecksBuilder
    {
        IEnumerable<Assembly> AutoMappers { get; }

        IEnumerable<Assembly> Controllers { get; }

        IZyMvcModuleBuilder AddAutoMapper(Assembly assembly);

        IZyMvcModuleBuilder AddController(Assembly assembly);
    }
}
