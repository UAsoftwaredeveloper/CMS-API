using CMS.Repositories.Interfaces;
using DataManager;
using DataManager.DataClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Repositories.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly CMSDBContext CMSDBContext;
        private string ErrorMessage = string.Empty;
        private DbSet<T> Entites;
        public Repository(CMSDBContext context)
        {
            CMSDBContext = context;
            Entites = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return Entites.Where(x => x.Deleted == false).AsNoTracking().ToList();
        }
        public IQueryable<T> GetAll(bool deleted = false)
        {
            return Entites.Where(x => x.Deleted == false).OrderByDescending(x => x.Id).AsNoTracking();
        }
        public IQueryable<SectionContent_Trails> GetAllSectionContentTrails(bool deleted = false)
        {
            return CMSDBContext.SectionContent_Trails
                .Where(x => x.Deleted == deleted)
                .OrderByDescending(x => x.Id)
                .AsNoTracking();
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<IQueryable<T>> Get(int id)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            var result = Entites.Where(x => x.Id == id).AsQueryable();
            if (result == null)
            {
                throw new KeyNotFoundException("Data Not Found");
            }
            return result;
        }
        public async Task<T> Insert(T entity)
        {
            try
            {

                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                entity.CreatedOn = DateTime.UtcNow;
                Entites.Add(entity);
                await CMSDBContext.SaveChangesAsync();
            }
            catch (Exception exc)
            {
                throw;
            }
            return entity;
        }
        public IQueryable<PackageItenaries_Trails> GetAllPackageItenaries_Trails(bool deleted = false)
        {
            return CMSDBContext.PackageItenaries_Trails
                .Where(x => x.Deleted == deleted)
                .OrderByDescending(x => x.Id)
                .AsNoTracking();
        }
        public IQueryable<HolidayPackages_Trails> GetAllHolidayPackages_Trails(bool deleted = false)
        {
            return CMSDBContext.HolidayPackages_Trails
                .Where(x => x.Deleted == deleted)
                .OrderByDescending(x => x.Id)
                .AsNoTracking();
        }
        public IQueryable<Section_Trails> GetAllSection_Trails(bool deleted = false)
        {
            return CMSDBContext.Sections_Trails
                .Where(x => x.Deleted == deleted)
                .OrderByDescending(x => x.Id)
                .AsNoTracking();
        }
        public IQueryable<TemplateDetails_Trails> GetAllTemplateDetails_Trails(bool deleted = false)
        {
            return CMSDBContext.TemplateDetails_Trails
                .Where(x => x.Deleted == deleted)
                .OrderByDescending(x => x.Id)
                .AsNoTracking();
        }

        public async Task Insert(TemplateDetails_Trails entity)
        {
            try
            {

                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                CMSDBContext.TemplateDetails_Trails.Add(entity);
                await CMSDBContext.SaveChangesAsync();
            }
            catch (Exception exc)
            {
                throw;
            }
        }
        public async Task InsertDuplicate(TemplateDetails_Trails entity, List<Section_Trails> entity1, List<SectionContent_Trails> entity2)
        {
            try
            {

                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                CMSDBContext.TemplateDetails_Trails.Add(entity);
                CMSDBContext.Sections_Trails.AddRange(entity1);
                CMSDBContext.SectionContent_Trails.AddRange(entity2);
                await CMSDBContext.SaveChangesAsync();
            }
            catch (Exception exc)
            {
                throw;
            }
        }
        public async Task Insert(Section_Trails entity)
        {
            try
            {

                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                CMSDBContext.Sections_Trails.Add(entity);
                await CMSDBContext.SaveChangesAsync();
            }
            catch (Exception exc)
            {
                throw;
            }
        }
        public async Task Insert(SectionContent_Trails entity)
        {
            try
            {

                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                CMSDBContext.SectionContent_Trails.Add(entity);
                await CMSDBContext.SaveChangesAsync();
            }
            catch (Exception exc)
            {
                throw;
            }
        }
        public async Task Insert(HolidayPackages_Trails entity)
        {
            try
            {

                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                CMSDBContext.HolidayPackages_Trails.Add(entity);
                await CMSDBContext.SaveChangesAsync();
            }
            catch (Exception exc)
            {
                throw;
            }
        }
        public async Task Insert(PackageItenaries_Trails entity)
        {
            try
            {

                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                CMSDBContext.PackageItenaries_Trails.Add(entity);
                await CMSDBContext.SaveChangesAsync();
            }
            catch (Exception exc)
            {
                throw;
            }
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
                    entities.ForEach(x => x.CreatedOn = DateTime.UtcNow);
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
            try
            {
                var oldData = Entites.FirstOrDefault(x => x.Id == entity.Id);
                entity.CreatedOn = oldData.CreatedOn;
                entity.CreatedBy = oldData.CreatedBy;
                entity.ModifiedOn = DateTime.UtcNow;
                CMSDBContext.Update(entity);
                await CMSDBContext.SaveChangesAsync();
                return entity;
            }catch(Exception)
            {
                throw;
            }
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
                    var ids = entities.Select(x => x.Id).ToList();
                    var oldData = Entites.AsNoTracking().Where(x => ids.Contains(x.Id) && x.Deleted == false).ToList();

                    entities.ForEach(x =>
                    {
                        var cEntity = oldData.FirstOrDefault(y => y.Id == x.Id);
                        if (cEntity != null)
                        {
                            x.CreatedOn = cEntity.CreatedOn;
                            x.CreatedBy = cEntity.CreatedBy;
                            x.ModifiedOn = DateTime.UtcNow;
                        }
                    });

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
            entity.Deleted = true;
            Entites.Update(entity);
            await CMSDBContext.SaveChangesAsync();
            return (T)entity;
        }
        public async Task<T> Delete(int Id)
        {
            if (Id < 1)
            {
                throw new ArgumentNullException("entity");
            }
            var entity = Entites.FirstOrDefault(x => x.Id == Id);
            if (entity == null)
            {
                throw new KeyNotFoundException("Record Not Found");
            }
            entity.Deleted = true;
            entity.ModifiedOn = DateTime.UtcNow;
            Entites.Update(entity);
            await CMSDBContext.SaveChangesAsync();
            return (T)entity;
        }
        public virtual IQueryable<T> GetAll(IQueryable<T> query, bool includeDeleted = false)
        {
            return query.Where(x => x.Deleted == includeDeleted).AsNoTracking();
        }
        public virtual IQueryable<SectionContent_Trails> GetAllSectionContentTrails(IQueryable<SectionContent_Trails> query, bool includeDeleted = false)
        {
            return query.Where(x => x.Deleted == includeDeleted).AsNoTracking();
        }

        public virtual IEnumerable<T> GetAll(IQueryable<T> queryable)
        {
            return queryable.Where(x => (bool)x.Deleted).AsEnumerable();
        }
    }
}
