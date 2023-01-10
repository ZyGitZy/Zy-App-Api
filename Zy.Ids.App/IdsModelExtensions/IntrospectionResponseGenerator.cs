using IdentityServer4.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zy.Ids.App.IdsModelExtensions
{
    public class IntrospectionResponseGenerator : IdentityServer4.ResponseHandling.IntrospectionResponseGenerator
    {
        public IntrospectionResponseGenerator(IEventService events, ILogger<IdentityServer4.ResponseHandling.IntrospectionResponseGenerator> logger) : base(events, logger)
        {
        }

        public override async System.Threading.Tasks.Task<Dictionary<string, object>> ProcessAsync(IdentityServer4.Validation.IntrospectionRequestValidationResult validationResult)
        {
            var result = await base.ProcessAsync(validationResult);
            return result;
        }
    }
}
