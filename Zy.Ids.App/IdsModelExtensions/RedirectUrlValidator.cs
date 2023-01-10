using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zy.Ids.App.IdsModelExtensions
{
    public class RedirectUrlValidator : IRedirectUriValidator
    {
        public async Task<bool> IsPostLogoutRedirectUriValidAsync(string requestedUri, Client client)
        {
            return await Task.FromResult(true);
        }

        public Task<bool> IsRedirectUriValidAsync(string requestedUri, Client client)
        {
            return Task.FromResult(true);
        }
    }
}
