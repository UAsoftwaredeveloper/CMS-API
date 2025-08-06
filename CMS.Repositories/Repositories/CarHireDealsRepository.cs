using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Repositories.Interfaces;
using DataManager;
using DataManager.DataClasses;

namespace CMS.Repositories.Repositories
{
    public class CarHireDealsRepository: Repository<CarHireDeals>, ICarHireDealsRepository
    {
        public CarHireDealsRepository(CMSDBContext cMSDBContext) : base(cMSDBContext) { }
    }
}
