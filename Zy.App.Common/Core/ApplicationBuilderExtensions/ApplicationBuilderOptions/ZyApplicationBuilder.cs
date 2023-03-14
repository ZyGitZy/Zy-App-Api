using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zy.App.Common.Core.ApplicationBuilderExtensions.ApplicationBuilderOptions
{
    public class ZyApplicationBuilder : IZyApplicationBuilder
    {
        public ZyApplicationBuilder(IApplicationBuilder applicationBuilder, ZyApplicationOptions zyApplicationOptions)
        {
            this.ApplicationBuilder = applicationBuilder;
            ZyApplicationOptions = zyApplicationOptions;
        }

        public IApplicationBuilder ApplicationBuilder { get; }

        public ZyApplicationOptions ZyApplicationOptions { get; }

    }
}
