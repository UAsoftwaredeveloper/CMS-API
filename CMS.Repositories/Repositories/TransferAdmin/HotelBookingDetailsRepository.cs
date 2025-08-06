using CMS.Repositories.Interfaces.TransferAdmin;
using DataManager;
using DataManager.TransferAdmin;

namespace CMS.Repositories.Repositories.TransferAdmin
{
    public class CarBookingTransactionDetailsRepository : Repository<CarBookingTransactionDetails>, ICarBookingTransactionDetailsRepository
    {
        public CarBookingTransactionDetailsRepository(TransferAdminDBContext transferAdminDB) : base(transferAdminDB) { }
    }
}
