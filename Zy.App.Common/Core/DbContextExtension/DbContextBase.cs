using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.AppExtensions;
using Zy.App.Common.Interfaces;
using Zy.App.Common.Models;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Zy.App.Common.Core.DbContextExtension
{
    public class DbContextBase : DbContext
    {
        protected IConfiguration configuration;

        private readonly GlobalQueryFilter _globalQueryFilter;

        protected IZyAppContext singlarContex = EmptyZyAppContext.Empty;

        public DbContextBase(Microsoft.EntityFrameworkCore.DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this._globalQueryFilter = new GlobalQueryFilter();
            this.configuration = configuration;
            this.ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.ChangeTracker.DetectChanges();
            this.UpdateUpadtedProperty();
            return base.SaveChangesAsync(cancellationToken);
        }

        public void SetByzanContext(IZyAppContext byzanContext)
        {
            singlarContex = byzanContext;
        }
        protected virtual void UpdateEntry(EntityEntry entity, UpdateContextInfo updateContextInfo)
        {
            this.SetUpdateInfo(entity, updateContextInfo);
            this.SetCommonInfo(entity, updateContextInfo);
            this.SetRowVersion(entity);
        }

        protected virtual EntityTypeBuilder<TEntity> EntityBase<TEntity>(ModelBuilder builder)
        where TEntity : EntityBase
        {
            var b = this.Entity<TEntity>(builder);

            b.HasQueryFilter(_ => this._globalQueryFilter.IgnoreDeleted || _.IsDeleted == this._globalQueryFilter.Deleted);
            return b;
        }

        protected virtual EntityTypeBuilder<TEntity> EntityBase<TEntity>(
          ModelBuilder builder,
          Action<EntityTypeBuilder<TEntity>> buildAction)
          where TEntity : EntityBase
        {
            var b = this.EntityBase<TEntity>(builder);
            buildAction(b);
            return b;
        }


        protected virtual EntityTypeBuilder<TEntity> Entity<TEntity>(ModelBuilder builder)
            where TEntity : class, IEntity<long>
        {
            var b = builder.Entity<TEntity>();
            this.AppendAdditionColumns(b).Property(_ => _.Id).ValueGeneratedNever();
            b.HasKey(k => k.Id);
            return b;
        }

        protected virtual EntityTypeBuilder<TEntity> AppendAdditionColumns<TEntity>(
         EntityTypeBuilder<TEntity> entityTypeBuilder)
         where TEntity : class, IEntity
        {
            entityTypeBuilder.Property(typeof(long), nameof(IEntityAdditionColumns.CreateByUserId)).HasDefaultValue(0L);
            entityTypeBuilder.Property(typeof(DateTime), nameof(IEntityAdditionColumns.CreateDateTime)).HasDefaultValue(DateTime.MinValue);
            entityTypeBuilder.Property(typeof(long), nameof(IEntityAdditionColumns.LastUpdateByUserId)).HasDefaultValue(0);
            entityTypeBuilder.Property(typeof(DateTime), nameof(IEntityAdditionColumns.LastUpdateDateTime)).HasDefaultValue(DateTime.MinValue);
            entityTypeBuilder.Property(nameof(IEntityLogicDelete.IsDeleted)).HasDefaultValue(false);
            entityTypeBuilder.Property(nameof(IEntityVersion.RowVersion)).HasDefaultValue(0);
            return entityTypeBuilder;
        }

        private void SetRowVersion(EntityEntry entity)
        {
            if (entity.Entity is not IEntityVersion)
            {
                return;
            }

            if (entity.State == EntityState.Added)
            {
                entity.Property(nameof(IEntityVersion.RowVersion)).CurrentValue = 1;
            }

            if (entity.State != EntityState.Modified)
            {
                return;
            }

            var properyRowVersion = entity.Property(nameof(IEntityVersion.RowVersion));
            properyRowVersion.OriginalValue = properyRowVersion.CurrentValue;
            properyRowVersion.IsModified = false;
            properyRowVersion.CurrentValue = (int)(properyRowVersion.OriginalValue ?? 1) + 1;
        }

        private void SetCommonInfo(EntityEntry entity, UpdateContextInfo updateContextInfo)
        {
            if (NeedToSetUpdateUser(entity))
            {
                entity.Property(nameof(IEntityAdditionColumns.LastUpdateByUserId)).CurrentValue = updateContextInfo.UserId;
                entity.Property(nameof(IEntityAdditionColumns.LastUpdateByUserId)).IsModified = true;
                entity.Property(nameof(IEntityAdditionColumns.LastUpdateDateTime)).CurrentValue = DateTime.Now;
                entity.Property(nameof(IEntityAdditionColumns.LastUpdateDateTime)).IsModified = true;
            }
        }

        private bool NeedToSetUpdateUser(EntityEntry entity)
        {
            if (entity.Entity is not IEntityAdditionColumns)
            {
                return false;
            }

            if (entity.Entity is not IEntityAdditionColumnsSupport)
            {
                return true;
            }

            var entry = entity.Entity as IEntityAdditionColumnsSupport;

            if (entry == null || entry.UpdateCreateUser == null || entry.UpdateCreateUser == true)
            {
                return true;
            }

            return false;
        }

        private void SetUpdateInfo(EntityEntry entity, UpdateContextInfo updateContextInfo)
        {
            var now = DateTime.Now;
            if (entity.State == EntityState.Added)
            {
                entity.Property(nameof(IEntityAdditionColumns.CreateByUserId)).CurrentValue = updateContextInfo.UserId;
                entity.Property(nameof(IEntityAdditionColumns.CreateDateTime)).CurrentValue = now;
            }
            else
            {
                if (entity.Entity is IEntityAdditionColumns)
                {
                    entity.Property(nameof(IEntityAdditionColumns.CreateByUserId)).IsModified = false;
                    entity.Property(nameof(IEntityAdditionColumns.CreateDateTime)).IsModified = false;
                }
            }
        }

        private void UpdateUpadtedProperty()
        {
            var modifiedSourceInfo = this.ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);
            var updateContentInfo = new UpdateContextInfo(this.singlarContex);

            foreach (var entity in modifiedSourceInfo)
            {
                this.UpdateEntry(entity, updateContentInfo);
            }
        }
    }
}
