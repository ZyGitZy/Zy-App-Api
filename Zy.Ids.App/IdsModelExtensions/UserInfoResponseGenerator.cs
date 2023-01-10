using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Validation;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singnalr.DAL.IdentityExentions
{
    public class UserInfoResponseGenerator : IdentityServer4.ResponseHandling.UserInfoResponseGenerator
    {
        public UserInfoResponseGenerator(IProfileService profile, IResourceStore resourceStore, ILogger<IdentityServer4.ResponseHandling.UserInfoResponseGenerator> logger) : base(profile, resourceStore, logger)
        {
        }

        public override async Task<Dictionary<string, object>> ProcessAsync(UserInfoRequestValidationResult validationResult)
        {
            var result = await base.ProcessAsync(validationResult);
            return result;
        }
    }
}
