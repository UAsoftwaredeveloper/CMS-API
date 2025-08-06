using AutoMapper;
using Cms.Services.Extensions;
using Cms.Services.Filters;
using Cms.Services.Interfaces;
using Cms.Services.Models.OpenAPIDataModel.CouponMaster;
using CMS.Repositories.Interfaces;
using DataManager.DataClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Cms.Services.Services
{
    public class CouponMasterDataService : ICouponMasterDataService
    {
        public ICouponMasterRepository _couponMasterDataRepository;
        public IMapper _mapper;
        public CouponMasterDataService(ICouponMasterRepository couponMasterDataRepository, IMapper mapper)
        {
            _couponMasterDataRepository = couponMasterDataRepository ?? throw new ArgumentNullException(nameof(couponMasterDataRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PaginatedList<CouponMasterData>> GetAllCoupons(CouponMasterFilter filter)
        {
            if (filter != null)
            {
                var result =
                _couponMasterDataRepository.GetAll(deleted: false).Include(x => x.Portal)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(filter.PortalId != null, x => x.PortalId == filter.PortalId.Value)
                .WhereIf(true, x => x.StartDate.Date != DateTime.MinValue && x.StartDate.Date <= DateTime.Today.Date && x.EndDate >= DateTime.Today.Date)
                .WhereIf(!string.IsNullOrEmpty(filter.CouponType), x => EF.Functions.Like(x.CouponType, $"%{filter.CouponType}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.CouponCode), x => EF.Functions.Like(x.CouponCode, $"%{filter.CouponCode}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.ServiceCategory), x => EF.Functions.Like(x.ServiceCategory, $"%{filter.ServiceCategory}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Status), x => x.Status.ToLower() != "inactive" && EF.Functions.Like(x.Status, $"%{filter.Status}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.CouponCode, $"%{filter.Query}%"))
                .WhereIf(filter.ShowOnHomePage != null, x => x.ShowOnHomePage == filter.ShowOnHomePage)
                .WhereIf(filter.Active != null, x => x.Active == filter.Active);

                return await Task.FromResult(_mapper.Map<PaginatedList<CouponMaster>, PaginatedList<CouponMasterData>>(new PaginatedList<CouponMaster>(result, filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _couponMasterDataRepository.GetAll(deleted: false);
                return await Task.FromResult(_mapper.Map<PaginatedList<CouponMaster>, PaginatedList<CouponMasterData>>(new PaginatedList<CouponMaster>(result, filter.PageSize, filter.PageNumber)));
            }
        }
        public async Task<CouponMasterData> GetById(int Id)
        {
            return _mapper.Map<CouponMaster, CouponMasterData>(await _couponMasterDataRepository.GetAll(false).Include(x => x.Portal).FirstOrDefaultAsync(x => x.Id == Id));
        }
    }
}
