using CMS.Repositories.Interfaces.TMM;
using DataManager;
using DataManager.TMMDbClasses;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CMS.Repositories.Repositories.TMM
{
    public class QuotationEmailSupportRepository : Repository<QuotationEmailSupport>, IQuotationEmailSupportRepository
    {
        public QuotationEmailSupportRepository(TMMDBContext tMMDBContext) : base(tMMDBContext) {
        }
    }
}
