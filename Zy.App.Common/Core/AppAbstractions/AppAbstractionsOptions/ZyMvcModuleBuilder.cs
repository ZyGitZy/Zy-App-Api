using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Core.AppAbstractions.IAppAbstractionsOptions;

namespace Zy.App.Common.Core.AppAbstractions.AppAbstractionsOptions
{
    public class ZyMvcModuleBuilder : IZyMvcModuleBuilder
    {

        private readonly IList<Assembly> _autoMappers;

        private readonly IList<Assembly> _controllers;

        public ZyMvcModuleBuilder(IServiceCollection services, IHealthChecksBuilder healthChecks)
        {
            this.Services = services;
            this._autoMappers = new List<Assembly>();
            this._controllers = new List<Assembly>();
            this.HealthChecks = healthChecks;
        }

        public IEnumerable<Assembly> AutoMappers => this._autoMappers;

        public IEnumerable<Assembly> Controllers => this._controllers;

        public IServiceCollection Services { get; }

        public IHealthChecksBuilder HealthChecks { get; }

        public IZyMvcModuleBuilder AddAutoMapper(Assembly assembly)
        {
            this._autoMappers.Add(assembly);

            return this;
        }

        public IZyMvcModuleBuilder AddController(Assembly assembly)
        {
            this._controllers.Add(assembly);

            return this;
        }
    }
}
