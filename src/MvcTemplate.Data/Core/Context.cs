using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MvcTemplate.Components.Mvc;
using MvcTemplate.Data.Mapping;
using MvcTemplate.Objects;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace MvcTemplate.Data.Core
{
    public class Context : DbContext
    {
        static Context()
        {
            ObjectMapper.MapObjects();
        }
        protected Context()
        {
        }
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            Type[] models = typeof(BaseModel)
                .Assembly
                .GetTypes()
                .Where(type =>
                    type.IsAbstract == false &&
                    typeof(BaseModel).IsAssignableFrom(type))
                .ToArray();

            foreach (Type model in models)
            {
                if (builder.Model.FindEntityType(model.FullName) == null)
                {
                    builder.Model.AddEntityType(model);
                }
            }

            foreach (IMutableEntityType entity in builder.Model.GetEntityTypes())
            {
                foreach (PropertyInfo property in entity.ClrType.GetProperties())
                {
                    if (property.GetCustomAttribute<IndexAttribute>(false) is IndexAttribute index)
                    {
                        builder.Entity(entity.ClrType).HasIndex(property.Name).IsUnique(index.IsUnique);
                    }
                }

                // 1. Add the IsDeleted property
                entity.GetOrAddProperty(nameof(BaseSoftDeleteModel.IsDeleted), typeof(bool));

                // 2. Create the query filter

                var parameter = Expression.Parameter(entity.ClrType);

                // EF.Property<bool>(post, "IsDeleted")
                var propertyMethodInfo = typeof(EF).GetMethod("Property").MakeGenericMethod(typeof(bool));
                var isDeletedProperty = Expression.Call(propertyMethodInfo, parameter, Expression.Constant(nameof(BaseSoftDeleteModel.IsDeleted)));

                // EF.Property<bool>(post, "IsDeleted") == false
                BinaryExpression compareExpression = Expression.MakeBinary(ExpressionType.Equal, isDeletedProperty, Expression.Constant(false));

                // post => EF.Property<bool>(post, "IsDeleted") == false
                var lambda = Expression.Lambda(compareExpression, parameter);

                builder.Entity(entity.ClrType).HasQueryFilter(lambda);
            }

            foreach (IMutableForeignKey key in builder.Model.GetEntityTypes().SelectMany(entity => entity.GetForeignKeys()))
            {
                key.DeleteBehavior = DeleteBehavior.Restrict;
            }

            builder.Entity<Account>().HasOne(x => x.Role).WithMany(x => x.Accounts);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseLazyLoadingProxies();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues[nameof(BaseSoftDeleteModel.IsDeleted)] = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues[nameof(BaseSoftDeleteModel.IsDeleted)] = true;
                        break;
                }
            }
        }

    }
}
