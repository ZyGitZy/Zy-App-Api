using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.StoreCore;
using Zy.Ids.Dal;
using Zy.Ids.DAL.Entitys;

namespace Zy.Ids.App.IdsModelExtensions
{
    public class RefreshTokenStore : IPersistedGrantStore
    {

        private ZyIdsDbContext serviceDbContext;

        private IEntityStore<PersistedGrantEntity> perStore;

        public RefreshTokenStore(ZyIdsDbContext serviceDbContext, IEntityStore<PersistedGrantEntity> entityStore)
        {
            this.serviceDbContext = serviceDbContext;
            this.perStore = entityStore;
        }

        public async Task<IEnumerable<PersistedGrant>> GetAllAsync(PersistedGrantFilter filter)
        {
            var entity = await this.perStore.Query().ToListAsync();
            return entity.Select(this.GetPersistedGrant);
        }

        public async Task<PersistedGrant> GetAsync(string key)
        {
            var entity = await this.perStore.Query(q => q.Key == key).FirstOrDefaultAsync();
            return this.GetPersistedGrant(entity);
        }

        public async Task RemoveAllAsync(PersistedGrantFilter filter)
        {
            var entity = await this.perStore.Query(q => q.SubjectId == filter.SubjectId && q.ClientId == filter.ClientId).ToListAsync();
            if (entity.Any())
            {
                this.serviceDbContext.Set<PersistedGrantEntity>().RemoveRange(entity);
                await this.serviceDbContext.SaveChangesAsync();
            }
        }

        public async Task RemoveAsync(string key)
        {
            var entity = await this.perStore.Query(q => q.Key == key).FirstOrDefaultAsync();
            if (entity != null)
            {
                this.serviceDbContext.Set<PersistedGrantEntity>().Remove(entity);
                await this.serviceDbContext.SaveChangesAsync();
            }
        }

        public Task StoreAsync(PersistedGrant grant)
        {
            return this.perStore.CreateSaveAsync(
                new PersistedGrantEntity()
                {
                    Key = grant.Key,
                    ClientId = grant.ClientId,
                    Type = grant.Type,
                    SubjectId = grant.SubjectId,
                    Data = grant.Data,
                    CreationTime = grant.CreationTime,
                    Expiration = grant.Expiration
                }, default);
        }

        private PersistedGrant GetPersistedGrant(PersistedGrantEntity? entity)
        {
            if (entity == null)
            {
                return new PersistedGrant();
            }

            return new PersistedGrant()
            {
                Key = entity.Key,
                ClientId = entity.ClientId,
                Type = entity.Type,
                SubjectId = entity.SubjectId,
                Data = entity.Data,
                CreationTime = entity.CreationTime,
                Expiration = entity.Expiration,
            };
        }
    }
}
