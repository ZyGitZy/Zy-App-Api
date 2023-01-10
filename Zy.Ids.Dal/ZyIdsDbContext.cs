using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Zy.App.Common.Core.DbContextExtension;
using Zy.Ids.DAL.Entitys;

namespace Zy.Ids.Dal
{
    public class ZyIdsDbContext : DbContextBase
    {
        public ZyIdsDbContext(DbContextOptions<ZyIdsDbContext> options, IConfiguration configuration) : base(options, configuration)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            this.EntityBase<ClientEntity>(modelBuilder);
            this.EntityBase<ClientGrantTypeEntity>(modelBuilder);
            this.EntityBase<ClientRedirectUriEntity>(modelBuilder);
            this.EntityBase<ClientScopeEntity>(modelBuilder);
            this.EntityBase<ClientSecretEntity>(modelBuilder);
            this.EntityBase<PersistedGrantEntity>(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

    }
}