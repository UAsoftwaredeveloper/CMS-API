using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Repositories.Interfaces.ActivityAdmin
{
    public interface IRepository<T> where T : class
    {
        DbSet<T> Entites();
        Task<T> Delete(T entity);
        IEnumerable<T> GetAll();
        IQueryable<T> GetAll(IQueryable<T> query, bool includeDeleted);
        IEnumerable<T> GetAll(IQueryable<T> queryable);
        IQueryable<T> GetAll(bool deleted = false);
        Task<T> Insert(T entity);
        Task<List<T>> InsertRange(List<T> entities);
        Task<T> Update(T entity);
        Task<List<T>> UpdateRange(List<T> entity);
        Task<T> FirstOrDefaultAsync(IQueryable<T> queryable);
    }
}