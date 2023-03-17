using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zy.App.Common.Core.HealthCheckExtensions.IHealthCheckExtensionOption
{
    public interface IZyHealthChecksBuilder
    {
        IHealthChecksBuilder HealthChecks { get; }
    }
}
