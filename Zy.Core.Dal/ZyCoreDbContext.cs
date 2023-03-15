using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Core.DbContextExtension;
using Zy.Core.Dal.Entitys;

namespace Zy.Core.Dal
{
    public class ZyCoreDbContext : DbContextBase
    {
        public ZyCoreDbContext(DbContextOptions<ZyCoreDbContext> options, IConfiguration configuration) : base(options, configuration)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            this.EntityBase<MenuEntity>(modelBuilder);
            this.EntityBase<RoleMenuEntity>(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}
