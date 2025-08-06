using AutoMapper;
using Cms.Services.Extensions;
using Cms.Services.Filters;
using Cms.Services.Interfaces;
using Cms.Services.Models.CouponMaster;
using CMS.Repositories.Interfaces;
using DataManager.DataClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Cms.Services.Services
{
    public class CouponMasterService : ICouponMasterService
    {
        public ICouponMasterRepository _couponMasterRepository;
        public IMapper _mapper;
        public CouponMasterService(ICouponMasterRepository CouponMasterRepository, IMapper mapper)
        {
            _couponMasterRepository = CouponMasterRepository ?? throw new ArgumentNullException(nameof(CouponMasterRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<CouponMasterModal> CreateCoupon(CouponMasterModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {
                var result = await _couponMasterRepository.Insert(_mapper.Map<CouponMasterModal, CouponMaster>(modal));
                return _mapper.Map<CouponMaster, CouponMasterModal>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public async Task<CouponMasterModal> UpdateCoupon(CouponMasterModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {
                var result = await _couponMasterRepository.Update(_mapper.Map<CouponMasterModal, CouponMaster>(modal));
                return _mapper.Map<CouponMaster, CouponMasterModal>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<PaginatedList<CouponMasterModal>> GetAllCoupons(CouponMasterFilter filter)
        {
            if (filter != null)
            {
                var result =
                _couponMasterRepository.GetAll(deleted: false).Include(x => x.Portal)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(filter.PortalId != null, x => x.PortalId == filter.PortalId.Value)
                .WhereIf(filter.StartDate != null && filter.EndDate != null, x => x.StartDate >= filter.StartDate.Value && x.EndDate <= filter.EndDate.Value)
                .WhereIf(!string.IsNullOrEmpty(filter.CouponType), x => EF.Functions.Like(x.CouponType, $"%{filter.CouponType}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.CouponCode), x => EF.Functions.Like(x.CouponCode, $"%{filter.CouponCode}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.ServiceCategory), x => EF.Functions.Like(x.ServiceCategory, $"%{filter.ServiceCategory}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Status), x => EF.Functions.Like(x.Status, $"%{filter.Status}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.CouponCode, $"%{filter.Query}%"))
                .WhereIf(filter.ShowOnHomePage != null, x => x.ShowOnHomePage == filter.ShowOnHomePage)
                .WhereIf(filter.Active != null, x => x.Active == filter.Active);

                return await Task.FromResult(_mapper.Map<PaginatedList<CouponMaster>, PaginatedList<CouponMasterModal>>(new PaginatedList<CouponMaster>(result, filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _couponMasterRepository.GetAll(deleted: false);
                return await Task.FromResult(_mapper.Map<PaginatedList<CouponMaster>, PaginatedList<CouponMasterModal>>(new PaginatedList<CouponMaster>(result, filter.PageSize, filter.PageNumber)));
            }
        }
        public async Task<bool> SoftDelete(int Id)
        {
            var result = await _couponMasterRepository.Delete(Id);
            if ((bool)result.Deleted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<CouponMasterModal> GetById(int Id)
        {
            return _mapper.Map<CouponMaster, CouponMasterModal>(await _couponMasterRepository.GetAll(false).Include(x => x.Portal).FirstOrDefaultAsync(x => x.Id == Id));
        }
    }
}
