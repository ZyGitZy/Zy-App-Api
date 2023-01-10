
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.StoreCore;
using Zy.Ids.DAL.Entitys;

namespace Zy.Ids.App.IdsModelExtensions
{
    public class ClientStore : IClientStore
    {
        private readonly IEntityStore<ClientEntity> clientStore;
        private readonly IEntityStore<ClientGrantTypeEntity> grantTypeStore;
        private readonly IEntityStore<ClientScopeEntity> scopeStore;
        private readonly IEntityStore<ClientSecretEntity> secretStore;
        private readonly IEntityStore<ClientRedirectUriEntity> redirectUriStore;

        public ClientStore(IEntityStore<ClientEntity> clientStore,
            IEntityStore<ClientGrantTypeEntity> grantTypeStore,
            IEntityStore<ClientScopeEntity> scopeStore,
            IEntityStore<ClientRedirectUriEntity> redirectUriStore,
            IEntityStore<ClientSecretEntity> secretStore
            )
        {
            this.clientStore = clientStore;
            this.grantTypeStore = grantTypeStore;
            this.scopeStore = scopeStore;
            this.secretStore = secretStore;
            this.redirectUriStore = redirectUriStore;
        }

        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            var clientEntity = clientStore.Query()
                .FirstOrDefault(f => f.ClientId == clientId);

            if (clientEntity == null)
            {
                return default!;
            }

            Client client = new()
            {
                ClientId = clientEntity.ClientId,
                ClientName = clientEntity.ClientName,
                RequireClientSecret = clientEntity.RequireClientSecret,
                AllowOfflineAccess = clientEntity.AllowOfflineAccess,
                AllowAccessTokensViaBrowser = clientEntity.AllowAccessTokensViaBrowser,
                RequireConsent = clientEntity.RequireConsent
            };

            if (clientEntity.AccessTokenLifetime > 0)
            {
                client.AccessTokenLifetime = clientEntity.AccessTokenLifetime;
            }

            client.ClientClaimsPrefix = clientEntity.ClientClaimsPrefix;
            client.AllowedGrantTypes = await grantTypeStore.Query()
                .Where(w => w.ClientId == clientEntity.Id)
                .Select(s => s.GrantType)
                .ToArrayAsync();

            client.ClientSecrets = await secretStore.Query()
                .Where(w => w.ClientId == clientEntity.Id)
                .Select(s => new Secret { Value = s.Value })
                .ToArrayAsync();

            client.AllowedScopes = await scopeStore.Query()
                .Where(w => w.ClientId == clientEntity.Id)
                .Select(s => s.Scope).ToArrayAsync();

            client.RedirectUris = await redirectUriStore.Query()
                .Where(w => w.ClientId == clientEntity.Id)
                .Select(s => s.RedirectUri)
                .ToArrayAsync();

            if (!client.RedirectUris.Any())
            {
                client.RedirectUris = new string[] { string.Empty };
            }

            Dictionary<string, ClientClaim> filterDic = new()
            {
                { "client_id", new ClientClaim("client_id", clientEntity.ClientId) },
                { "client_name", new ClientClaim("client_name", clientEntity.ClientName) }
            };

            client.Claims = filterDic.Values.ToArray();

            return client;
        }
    }
}
