using DataManager.DataClasses;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Repositories.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        Task<T> Delete(T entity);
        Task<T> Delete(int Id);
        Task<IQueryable<T>> Get(int id);
        IEnumerable<T> GetAll();
        IQueryable<T> GetAll(IQueryable<T> query, bool includeDeleted);
        IEnumerable<T> GetAll(IQueryable<T> queryable);
        IQueryable<T> GetAll(bool deleted = false);
        IQueryable<HolidayPackages_Trails> GetAllHolidayPackages_Trails(bool deleted = false);
        IQueryable<PackageItenaries_Trails> GetAllPackageItenaries_Trails(bool deleted = false);
        IQueryable<SectionContent_Trails> GetAllSectionContentTrails(bool deleted = false);
        IQueryable<SectionContent_Trails> GetAllSectionContentTrails(IQueryable<SectionContent_Trails> query, bool includeDeleted = false);
        IQueryable<Section_Trails> GetAllSection_Trails(bool deleted = false);
        IQueryable<TemplateDetails_Trails> GetAllTemplateDetails_Trails(bool deleted = false);
        Task<T> Insert(T entity);
        Task Insert(TemplateDetails_Trails entity);
        Task Insert(Section_Trails entity);
        Task Insert(HolidayPackages_Trails entity);
        Task Insert(PackageItenaries_Trails entity);
        Task Insert(SectionContent_Trails entity);
        Task InsertDuplicate(TemplateDetails_Trails entity, List<Section_Trails> entity1, List<SectionContent_Trails> entity2);
        Task<List<T>> InsertRange(List<T> entities);
        Task<T> Update(T entity);
        Task<List<T>> UpdateRange(List<T> entity);
    }
}