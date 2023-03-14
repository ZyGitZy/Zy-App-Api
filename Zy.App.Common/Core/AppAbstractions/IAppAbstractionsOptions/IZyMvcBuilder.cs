using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zy.App.Common.Core.AppAbstractions.IAppAbstractionsOptions
{
    public interface IZyMvcBuilder : IZyBuilder
    {
        IMvcCoreBuilder MvcBuilder { get; }

        IEnumerable<IZyMvcModuleBuilder> Modules { get; }

        IZyMvcBuilder AddModules(Action<IZyMvcModuleBuilder> action);
    }
}
