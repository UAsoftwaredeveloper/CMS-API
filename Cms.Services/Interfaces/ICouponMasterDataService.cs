using Cms.Services.Filters;
using Cms.Services.Models.OpenAPIDataModel.CouponMaster;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces
{
    public interface ICouponMasterDataService
    {
        Task<PaginatedList<CouponMasterData>> GetAllCoupons(CouponMasterFilter filter);
        Task<CouponMasterData> GetById(int Id);
    }
}