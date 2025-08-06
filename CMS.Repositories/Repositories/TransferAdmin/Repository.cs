using CMS.Repositories.Interfaces.TransferAdmin;
using DataManager;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Repositories.Repositories.TransferAdmin
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly TransferAdminDBContext TransferAdminDBContext;
        public readonly DbSet<T> Entites;
        public Repository(TransferAdminDBContext context)
        {
            TransferAdminDBContext = context;
            Entites = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return Entites.AsNoTracking().ToList();
        }
        public IQueryable<T> GetAll(bool deleted = false)
        {
            return Entites.AsNoTracking();
        }
        public async Task<T> Insert(T entity)
        {
            try
            {

                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                Entites.Add(entity);
                await TransferAdminDBContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
            return entity;
        }
        public async Task<List<T>> InsertRange(List<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            using (var transaction = await TransferAdminDBContext.Database.BeginTransactionAsync())
            {
                try
                {
                    Entites.AddRange(entities);
                    await TransferAdminDBContext.SaveChangesAsync();

                    await transaction.CommitAsync();
                    return entities;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
        public async Task<T> Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            Entites.Update(entity);
            await TransferAdminDBContext.SaveChangesAsync();
            return entity;
        }
        public async Task<List<T>> UpdateRange(List<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            using (var transaction = await TransferAdminDBContext.Database.BeginTransactionAsync())
            {
                try
                {

                    Entites.UpdateRange(entities);
                    await TransferAdminDBContext.SaveChangesAsync();

                    await transaction.CommitAsync();
                    return entities;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
        public async Task<T> Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            Entites.Remove(entity);
            await TransferAdminDBContext.SaveChangesAsync();
            return entity;
        }
        public virtual IQueryable<T> GetAll(IQueryable<T> query, bool includeDeleted = false)
        {
            return query.AsNoTracking();
        }
        public virtual IEnumerable<T> GetAll(IQueryable<T> queryable)
        {
            return queryable.AsEnumerable();
        }
        public async Task<T> FirstOrDefaultAsync(IQueryable<T> queryable)
        {
            return await queryable.FirstOrDefaultAsync();
        }

        DbSet<T> IRepository<T>.Entites()
        {
            return Entites;
        }
    }
}
