using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using MvcTemplate.Data.Logging;
using MvcTemplate.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTemplate.Data.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private IAuditLogger Logger { get; }
        private DbContext Context { get; }

        private IDbContextTransaction Transaction;
        public UnitOfWork(DbContext context, IAuditLogger logger = null)
        {
            Context = context;
            Logger = logger;
        }

        public TDestination GetAs<TModel, TDestination>(Int32? id) where TModel : BaseModel
        {
            return id == null
                ? default(TDestination)
                : Context.Set<TModel>().Where(model => model.Id == id).ProjectTo<TDestination>().FirstOrDefault();
        }


        public async Task<TDestination> GetAsAsync<TModel, TDestination>(Int32? id) where TModel : BaseModel
        {
            return id == null
                ? default(TDestination)
                : await Context.Set<TModel>().Where(model => model.Id == id).ProjectTo<TDestination>().FirstOrDefaultAsync();
        }
        public TModel Get<TModel>(Int32? id) where TModel : BaseModel
        {
            return id == null ? null : Context.Find<TModel>(id);
        }

        public async Task<TModel> GetAsync<TModel>(Int32? id) where TModel : BaseModel
        {
            return id == null ? null : await Context.FindAsync<TModel>(id);
        }

        public TDestination To<TDestination>(Object source)
        {
            return Mapper.Map<TDestination>(source);
        }

        public IQuery<TModel> Select<TModel>() where TModel : BaseModel
        {
            return new Query<TModel>(Context.Set<TModel>());
        }

        public void InsertRange<TModel>(IEnumerable<TModel> models) where TModel : BaseModel
        {
            foreach (TModel model in models)
                model.Id = 0;

            Context.AddRange(models);
        }

        public async Task InsertRangeAsync<TModel>(IEnumerable<TModel> models) where TModel : BaseModel
        {
            foreach (TModel model in models)
                model.Id = 0;

            await Context.AddRangeAsync(models);
        }
        public void Insert<TModel>(TModel model) where TModel : BaseModel
        {
            model.Id = 0;

            Context.Add(model);
        }

        public async Task InsertAsync<TModel>(TModel model) where TModel : BaseModel
        {
            model.Id = 0;

            await Context.AddAsync(model);
        }
        public void Update<TModel>(TModel model) where TModel : BaseModel
        {
            EntityEntry<TModel> entry = Context.Entry(model);
            if (entry.State != EntityState.Modified && entry.State != EntityState.Unchanged)
                entry.State = EntityState.Modified;

            entry.Property(property => property.CreationDate).IsModified = false;
        }

        public void DeleteRange<TModel>(IEnumerable<TModel> models) where TModel : BaseModel
        {
            Context.RemoveRange(models);
        }

        public void Delete<TModel>(TModel model) where TModel : BaseModel
        {
            Context.Remove(model);
        }
        public void Delete<TModel>(Int32 id) where TModel : BaseModel
        {
            Delete(Context.Find<TModel>(id));
        }

        public async Task DeleteAsync<TModel>(Int32 id) where TModel : BaseModel
        {
            Delete(await Context.FindAsync<TModel>(id));
        }


        public void StartTransaction()
        {
            Transaction = Context.Database.BeginTransaction();
        }

        public void Commit()
        {
            Transaction.Commit();
        }

        public async Task SaveChangesAsync()
        {
            Logger?.Log(Context.ChangeTracker.Entries<BaseModel>());

            await Context.SaveChangesAsync();

            await Logger?.SaveAsync();
        }

        public void SaveChanges()
        {
            Logger?.Log(Context.ChangeTracker.Entries<BaseModel>());

            Context.SaveChanges();

            Logger?.Save();
        }

        public void Dispose()
        {
            Logger?.Dispose();
            Context.Dispose();
        }
    }
}
