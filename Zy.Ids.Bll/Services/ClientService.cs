using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.AppExtensions;
using Zy.Ids.Bll.Interfaces;
using Zy.Ids.Bll.Models;
using Zy.Ids.Dal;
using Zy.Ids.DAL.Entitys;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using IdentityServer4.Models;
using Zy.App.Common.Models;

namespace Zy.Ids.Bll.Services
{
    public class ClientService : IClientService
    {
        private readonly IMapper mapper;

        private readonly IZyIdsEntityStore<ClientEntity> clientStore;

        private readonly IZyIdsEntityStore<ClientSecretEntity> secretStore;

        private readonly IZyIdsEntityStore<ClientScopeEntity> scopStore;

        private readonly string tempName = "Client";

        public ClientService(IMapper mapper, IZyIdsEntityStore<ClientEntity> clientStore, IZyIdsEntityStore<ClientSecretEntity> secretStore, IZyIdsEntityStore<ClientScopeEntity> scopStore)
        {
            this.mapper = mapper;
            this.clientStore = clientStore;
            this.secretStore = secretStore;
            this.scopStore = scopStore;
        }

        public async Task<ServiceResult> DeleteAsync(long id, ClientBo clientBo)
        {
            var entity = await this.clientStore.FindAsync(default, id);

            if (entity == null)
            {
                return this.NotFound(tempName, id.ToString());
            }

            this.clientStore.Delete(entity);

            var scopEntity = await this.scopStore.Query(q => q.ClientId == entity.Id).ToListAsync();

            scopEntity.ForEach(this.scopStore.Delete);

            var secretEntity = await this.secretStore.Query(q => q.ClientId == entity.Id).FirstOrDefaultAsync();

            if (secretEntity != null)
            {
                this.secretStore.Delete(secretEntity);
            }

            await this.clientStore.SaveChangesAsync();

            return this.Ok();
        }

        public async Task<ServiceResult<ClientBo>> GetAsync(long id)
        {
            var entity = await this.clientStore.FindAsync(default, id);

            if (entity == null)
            {
                return this.NotFound(tempName, id.ToString()).As<ClientBo>();
            }

            var bo = this.mapper.Map<ClientEntity, ClientBo>(entity);

            return this.Ok(bo);
        }

        public async Task<ServiceResult<long>> PostAsync(ClientBo clientBo)
        {
            if (await IsExistsNo(clientBo.ClientName))
            {
                return this.NoDuplicate(tempName, clientBo.ClientName);
            }

            var entity = this.mapper.Map<ClientBo, ClientEntity>(clientBo);

            var result = this.clientStore.Create(entity);

            var secret = Guid.NewGuid().ToString();

            this.secretStore.Create(new()
            {
                ClientId = result.Id,
                Value = secret.Sha256(),
                RawValue = secret
            });

            if (clientBo.ClientScopes.Any())
            {
                foreach (var scope in clientBo.ClientScopes)
                {
                    this.scopStore.Create(new()
                    {
                        ClientId = result.Id,
                        Scope = scope
                    });
                }
            }

            await this.secretStore.SaveChangesAsync();

            return this.Ok(entity.Id);

        }

        public async Task<ServiceResult> PutAsync(long id, ClientBo clientBo)
        {
            var entity = await this.clientStore.FindAsync(default, id);

            if (entity == null)
            {
                return this.NotFound(tempName, id.ToString());
            }

            if (clientBo.ClientScopes.Any())
            {
                var originScopEntity = await this.scopStore.Query(q => q.ClientId == id).ToListAsync();
                var originScops = originScopEntity.Select(s => s.Scope).ToList();
                foreach (var item in clientBo.ClientScopes)
                {
                    var toDeleteScopes = originScops.Except(clientBo.ClientScopes.ToList()).ToList();
                    var deleteEntity = originScopEntity.Where(w => toDeleteScopes.Contains(w.Scope)).ToList();
                    deleteEntity.ForEach(this.scopStore.Delete);

                    var toAdd = clientBo.ClientScopes.Except(originScops);
                    foreach (var scop in toAdd)
                    {
                        this.scopStore.Create(new()
                        {
                            ClientId = entity.Id,
                            Scope = scop
                        });
                    }
                }
            }

            await this.clientStore.SaveChangesAsync();

            return this.Ok();
        }

        private async Task<bool> IsExistsNo(string no)
        {
            if (string.IsNullOrWhiteSpace(no))
            {
                return false;
            }

            return await this.clientStore.Query(q => q.ClientName == no).AnyAsync();
        }
    }
}
