using CMS.Repositories.Interfaces.TMM;
using DataManager;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Repositories.Repositories.TMM
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly TMMDBContext CMSDBContext;
        public readonly DbSet<T> Entites;
        public Repository(TMMDBContext context)
        {
            CMSDBContext = context;
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
            await CMSDBContext.SaveChangesAsync();
            }catch(Exception exc)
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

            using (var transaction = await CMSDBContext.Database.BeginTransactionAsync())
            {
                try
                {
                    Entites.AddRange(entities);
                    await CMSDBContext.SaveChangesAsync();

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
            await CMSDBContext.SaveChangesAsync();
            return entity;
        }
        public async Task<List<T>> UpdateRange(List<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            using (var transaction = await CMSDBContext.Database.BeginTransactionAsync())
            {
                try
                {

                    Entites.UpdateRange(entities);
                    await CMSDBContext.SaveChangesAsync();

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
            await CMSDBContext.SaveChangesAsync();
            return (T)entity;
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
