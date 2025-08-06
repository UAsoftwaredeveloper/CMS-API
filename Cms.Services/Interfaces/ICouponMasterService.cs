using Cms.Services.Filters;
using Cms.Services.Models.CouponMaster;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces
{
    public interface ICouponMasterService
    {
        Task<CouponMasterModal> CreateCoupon(CouponMasterModal modal);
        Task<PaginatedList<CouponMasterModal>> GetAllCoupons(CouponMasterFilter filter);
        Task<CouponMasterModal> GetById(int Id);
        Task<bool> SoftDelete(int Id);
        Task<CouponMasterModal> UpdateCoupon(CouponMasterModal modal);
    }
}