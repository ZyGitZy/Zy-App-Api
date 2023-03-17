using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zy.App.Common.Core.DbContextExtension
{
    public class ZyCoreOptionsExtension : CoreOptionsExtension
    {
        public ZyCoreOptionsExtension()
        {

        }

        public ZyCoreOptionsExtension(CoreOptionsExtension coreOptions) : base(coreOptions)
        {

        }

        public override void ApplyServices(IServiceCollection services)
        {
            services.AddSingleton<IConventionSetPlugin, ZyCoreConventionSetBuilder>();
            base.ApplyServices(services);
        }
    }
}
