﻿
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Zy.App.Common.StoreCore;
using Zy.Ids.DAL.Entitys;
using Zy.User.DAL.Entitys;

namespace Zy.Ids.App.IdsModelExtensions
{
    public class ClientStore : IClientStore
    {
        private readonly IEntityStore<ClientEntity> clientStore;
        private readonly IEntityStore<ClientGrantTypeEntity> grantTypeStore;
        private readonly IEntityStore<ClientScopeEntity> scopeStore;
        private readonly IEntityStore<ClientSecretEntity> secretStore;
        private readonly IEntityStore<ClientRedirectUriEntity> redirectUriStore;
        private readonly IEntityStore<UserEntity> userStore;

        public ClientStore(IEntityStore<ClientEntity> clientStore,
            IEntityStore<ClientGrantTypeEntity> grantTypeStore,
            IEntityStore<ClientScopeEntity> scopeStore,
            IEntityStore<ClientRedirectUriEntity> redirectUriStore,
            IEntityStore<ClientSecretEntity> secretStore,
            IEntityStore<UserEntity> userStore
            )
        {
            this.clientStore = clientStore;
            this.grantTypeStore = grantTypeStore;
            this.scopeStore = scopeStore;
            this.secretStore = secretStore;
            this.redirectUriStore = redirectUriStore;
            this.userStore = userStore;
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

            Dictionary<string, Claim> filterDic = new()
            {
                { "client_id", new Claim("client_id", clientEntity.ClientId) },
                { "client_name", new Claim("client_name", clientEntity.ClientName) }
            };

            if (clientEntity.UserId > 0)
            {
                var user = await this.userStore.FindAsync(default, clientEntity.UserId);
                if (user != null)
                {
                    filterDic.Add("user_id", new Claim("user_id", user.Id.ToString()));
                    filterDic.Add("user_name", new Claim("user_id", user.Name));
                }
            }

            client.Claims = filterDic.Values.ToArray();

            return client;
        }
    }
}
