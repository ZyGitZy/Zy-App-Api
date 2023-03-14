using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zy.App.Common.Core.AppAbstractions.IAppAbstractionsOptions
{
    public interface IZyBuilder
    {
        IServiceCollection Services { get; }
    }
}
