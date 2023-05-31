using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Core.DbContextExtension;

namespace Zy.Other.Dal
{
    public class ZyOtherDbContext : DbContextBase
    {
        public ZyOtherDbContext(DbContextOptions<ZyOtherDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
