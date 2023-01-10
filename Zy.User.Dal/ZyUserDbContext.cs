using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Zy.User.DAL.Entitys;

namespace Zy.User.Dal
{
    public class ZyUserDbContext : IdentityDbContext<UserEntity, RoleEntity, long, UserClaimEntity, UserRoleEntity, UserLoginEntity, RoleClaimEntity, UserTokenEntity>
    {
        private readonly IConfiguration configuration;

        public ZyUserDbContext(DbContextOptions<ZyUserDbContext> options, IConfiguration configuration)
              : base(options)
        {
            this.configuration = configuration;
            this.ChangeTracker.AutoDetectChangesEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            BuildIdentityModel(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = this.configuration.GetSection("ConnectionString").Value;
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), e => e.MigrationsAssembly("Zy.App.Api"));
        }

        private static void BuildIdentityModel(ModelBuilder builder)
        {
            builder.Entity<UserEntity>(
                b =>
                {
                    b.HasKey(u => u.Id);
                    b.HasMany<UserClaimEntity>().WithOne().HasForeignKey(u => u.UserId).IsRequired();
                    b.HasMany<UserLoginEntity>().WithOne().HasForeignKey(u => u.UserId).IsRequired();
                    b.HasMany<UserTokenEntity>().WithOne().HasForeignKey(u => u.UserId).IsRequired();
                    b.HasMany<UserRoleEntity>().WithOne().HasForeignKey(u => u.UserId).IsRequired();
                    b.ToTable("User.User");
                });

            builder.Entity<UserClaimEntity>(
                b =>
                {
                    b.HasKey(k => k.Id);
                    b.ToTable("User.UserClaim");
                });

            builder.Entity<UserLoginEntity>(
                b =>
                {
                    b.HasKey(l => new { l.LoginProvider, l.ProviderKey });
                    b.ToTable("User.UserLogin");
                });

            builder.Entity<UserTokenEntity>(
                b =>
                {
                    b.HasKey(l => new { l.UserId, l.LoginProvider, l.Name });
                    b.ToTable("User.UserToken");
                });

            builder.Entity<RoleEntity>(
                b =>
                {
                    b.HasKey(k => k.Id);
                    b.HasMany<UserRoleEntity>().WithOne().HasForeignKey(u => u.RoleId).IsRequired();
                    b.HasMany<RoleClaimEntity>().WithOne().HasForeignKey(u => u.RoleId).IsRequired();
                    b.ToTable("User.Role");
                });

            builder.Entity<RoleClaimEntity>(
                b =>
                {
                    b.HasKey(k => k.Id);
                    b.ToTable("User.RoleClaim");
                });

            builder.Entity<UserRoleEntity>(
                b =>
                {
                    b.HasKey(k => new { k.UserId, k.RoleId });
                    b.ToTable("User.UserRole");
                });
        }

    }
}
