using Microsoft.EntityFrameworkCore;
using RusMProject.Domain.Common;
using RusMProject.Persistance.DbContexts;
using RusMProject.Persistance.Utilities.CrossCustingConcerns.Aspects.Transaction;
using RusMProjectApplication.Abstraction.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RusMProject.Persistance.Statics;

namespace RusMProject.Persistance.Repositories
{
    public class WriteGenericRepository<T> : IWriteGenericRepository<T> where T : BaseEntity, IEntity
    {
        public AppDbContext Context { get; set; }
        public WriteGenericRepository(AppDbContext context)
        {
            Context = context;
        }
        public DbSet<T> Table => Context.Set<T>();

        public async Task<bool> Add(T model)
        {
            var entityEntry = await Table.AddAsync(model);

            if (entityEntry.State == EntityState.Added)
            {
                Commit();
                return true;
            }
            return false;
        }

        public async Task AddRange(List<T> models)
        {
            await Table.AddRangeAsync(models);
        }

        public bool Remove(T model)
        {
            var entityEntry = Table.Remove(model);
            if (entityEntry.State == EntityState.Deleted)
            {
                Commit();
                return true;
            }
            return false;
        }

        public void RemoveRange(List<T> models)
        {
            Table.RemoveRange(models);

        }

        public void Commit()
        {
            var entitiesEntryTracker = Context.ChangeTracker.Entries();

            foreach (var entityEntry in entitiesEntryTracker)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    entityEntry.Property("CreatorUser").CurrentValue = 1;
                    entityEntry.Property("CreatorDate").CurrentValue = DateTime.UtcNow;

                    entityEntry.Property("IsActive").CurrentValue = true;
                }
                else if (entityEntry.State == EntityState.Modified)
                {
                    entityEntry.Property("EditorUser").CurrentValue = 1;
                    entityEntry.Property("EditorUser").IsModified = true;
                    entityEntry.Property("EditorDate").CurrentValue = DateTime.UtcNow;
                    entityEntry.Property("EditorDate").IsModified = true;

                    entityEntry.Property("CreatorUser").IsModified = false;
                    entityEntry.Property("CreatorDate").IsModified = false;

                    entityEntry.Property("IsActive").CurrentValue = true;
                }
                else if (entityEntry.State == EntityState.Deleted)
                {
                    entityEntry.Property("IsActive").CurrentValue = false;

                    entityEntry.Property("CreatorUser").IsModified = false;
                    entityEntry.Property("CreatorDate").IsModified = false;
                    entityEntry.Property("EditorDate").IsModified = false;
                    entityEntry.Property("EditorDate").IsModified = false;
                }
            }
            Context.SaveChanges();
        }
        [TransactionAspect]
        public bool Update(T model)
        {
            var entity = Context.Entry(model);
            if (entity.State!=EntityState.Modified)
                return false;
            Commit();
            return true;
        }
    }
}
