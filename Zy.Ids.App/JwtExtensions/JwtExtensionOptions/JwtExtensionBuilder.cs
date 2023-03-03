using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zy.Ids.App.JwtModelExtensions.JwtModelExtensionOptions
{
    public class JwtExtensionBuilder
    {
        public JwtExtensionBuilder(IServiceCollection service, JwtExtensionOption option)
        {
            this.Service = service;
            this.Option = option;
        }

        public IServiceCollection Service;

        public JwtExtensionOption Option;
    }
}
