using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MvcTemplate.Components.Mvc;
using MvcTemplate.Data.Mapping;
using MvcTemplate.Objects;
using System;
using System.Linq;
using System.Reflection;

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
                if (builder.Model.FindEntityType(model.FullName) == null)
                    builder.Model.AddEntityType(model);

            foreach (IMutableEntityType entity in builder.Model.GetEntityTypes())
                foreach (PropertyInfo property in entity.ClrType.GetProperties())
                    if (property.GetCustomAttribute<IndexAttribute>(false) is IndexAttribute index)
                        builder.Entity(entity.ClrType).HasIndex(property.Name).IsUnique(index.IsUnique);

            foreach (IMutableForeignKey key in builder.Model.GetEntityTypes().SelectMany(entity => entity.GetForeignKeys()))
                key.DeleteBehavior = DeleteBehavior.Restrict;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseLazyLoadingProxies();
        }
    }
}
