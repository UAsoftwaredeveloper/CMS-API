using System;
using System.Collections.Generic;
using System.Linq;


namespace Cms.Services
{
    public class PaginatedList<TEntity>
    {
        #region properties
        public int CurrentPage { get; private set; } = 1;
        public int PageSize {  get; private set; }
        public int TotalCount { get; private set; } = 0;
        public int TotalPageCount { get; private set; } = 0;
        public int TotalPages { get; private set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => TotalPages > CurrentPage;
        public List<TEntity> Result {  get; set; } 
        #endregion
        public PaginatedList()
        {

        }
        public PaginatedList(IQueryable<TEntity> query, int pageSize, int pageIndex)
        {
            if (pageIndex > 0 && pageSize > 0)
            {
                int totalItems = query.Count();
                var data = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                int itemsCount = data.Count();
                Result = data.ToList();
                TotalCount = totalItems;
                CurrentPage = pageIndex;
                TotalPageCount = (int)Math.Ceiling((double)totalItems / pageSize);
                TotalPages = TotalPageCount > 0 ? TotalPageCount : 1;
                PageSize = pageSize;
            }

        }

        public PaginatedList(IList<TEntity> query, int pageSize, int pageIndex)
        {
            if(pageIndex>0 && pageSize>0) 
            {
                int totalItems= query.Count();
                var data= query.Skip((pageIndex-1)*pageSize).Take(pageSize).ToList();
                int itemsCount=data.Count();
                Result= data.ToList();
                TotalCount= totalItems;
                CurrentPage = pageIndex;
                PageSize = pageSize;
                TotalPageCount = (int)Math.Ceiling((double)totalItems / pageSize);
                TotalPages = TotalPageCount > 0 ? TotalPageCount : 1;
            }
           
        }
    }
}
