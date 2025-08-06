using CMS.Repositories.Interfaces.TMM;
using DataManager;
using DataManager.TMMDbClasses;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CMS.Repositories.Repositories.TMM
{
    public class SubscribeRepository : Repository<Subscribe>, ISubscribeRepository
    {
        public SubscribeRepository(TMMDBContext tMMDBContext) : base(tMMDBContext) {
        }
        public Users Users { get; set; }
        public override IQueryable<Subscribe> GetAll(IQueryable<Subscribe> query, bool includeDeleted = false)
        {
            return base.GetAll(query, includeDeleted).Include(x=>x.Users);
        }
    }
}
