using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Core.AppAbstractions.IAppAbstractionsOptions;

namespace Zy.App.Common.Core.AppAbstractions.AppAbstractionsOptions
{
    public class ZyMvcBuilder : IZyMvcBuilder
    {
        public ZyMvcBuilder(IMvcCoreBuilder MvcBuilder, IServiceCollection Services, IHealthChecksBuilder healthChecks)
        {
            this.MvcBuilder = MvcBuilder;
            this.Services = Services;
            this.HealthChecks = healthChecks;
            this._modules = new List<IZyMvcModuleBuilder>();
        }

        private IList<IZyMvcModuleBuilder> _modules { get; }

        public IMvcCoreBuilder MvcBuilder { get; }

        public IServiceCollection Services { get; }

        public IHealthChecksBuilder HealthChecks { get; }

        public IEnumerable<IZyMvcModuleBuilder> Modules => this._modules; // 添加注册控制器和autoMapper文件

        public IZyMvcBuilder AddModules(Action<IZyMvcModuleBuilder> action)
        {
            var mvcModuleBuilder = new ZyMvcModuleBuilder(this.Services, this.HealthChecks);

            action(mvcModuleBuilder);

            this._modules.Add(mvcModuleBuilder);

            return this;
        }
    }
}
