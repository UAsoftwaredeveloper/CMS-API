using CMS.Repositories.Interfaces.TMM;
using DataManager;
using DataManager.TMMDbClasses;

namespace CMS.Repositories.Repositories.TMM
{
    public class ContactUsRepository : Repository<ContactUs>, IContactUsRepository
    {
        public ContactUsRepository(TMMDBContext tMMDBContext) : base(tMMDBContext) { }
    }
}
