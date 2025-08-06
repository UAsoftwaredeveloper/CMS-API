using AutoMapper;
using Cms.Services.Extensions;
using Cms.Services.Filters;
using Cms.Services.Interfaces;
using Cms.Services.Models.HotelDeals;
using CMS.Repositories.Interfaces;
using DataManager.DataClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Cms.Services.Services
{
    public class HotelDealsService : IHotelDealsService
    {
        public IHotelDealsRepository _sectionRepository;
        public IMapper _mapper;
        public HotelDealsService(IHotelDealsRepository usersRepository, IMapper mapper)
        {
            _sectionRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<CreateHotelDealsModal> CreateDealManagement(CreateHotelDealsModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {
                var result = await _sectionRepository.Insert(_mapper.Map<CreateHotelDealsModal, HotelDeals>(modal));
                return _mapper.Map<HotelDeals, CreateHotelDealsModal>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message,ex);
            }
        }
        public async Task<UpdateHotelDealsModal> UpdateDealManagement(UpdateHotelDealsModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {
                var result = await _sectionRepository.Update(_mapper.Map<UpdateHotelDealsModal, HotelDeals>(modal));
                return _mapper.Map<HotelDeals, UpdateHotelDealsModal>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<PaginatedList<HotelDealsModal>> GetAllDealManagement(HotelDealsFilter filter)
        {
            if (filter != null)
            {
                var result =
                _sectionRepository.GetAll(deleted: false).Include(x => x.Portal)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(filter.PortalId != null && filter.PortalId.Value > 0, x => x.PortalId == filter.PortalId.Value)
                .WhereIf(filter.Active != null && filter.Active.Value, x => x.Active == filter.Active)
                .WhereIf(filter.DealAmount != null && filter.DealAmount.Value > 0, x => x.Price == filter.DealAmount.Value)
                .WhereIf((filter.DealAmountFrom != null && filter.DealAmountFrom.Value > 0) && (filter.DealAmountTo != null && filter.DealAmountTo.Value > 0),
                        x => x.Price >= filter.DealAmountFrom.Value && x.Price <= filter.DealAmountTo)
                .WhereIf(!string.IsNullOrEmpty(filter.DealType), x => EF.Functions.Like(x.DealType, $"%{filter.DealType}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.HotelName), x => EF.Functions.Like(x.HotelName, $"%{filter.HotelName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.HotelCode), x => EF.Functions.Like(x.DealType, $"%{filter.HotelCode}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.PortalName), x => EF.Functions.Like(x.Portal.Name, $"%{filter.PortalName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => 
                EF.Functions.Like(x.Portal.Name, $"%{filter.Query}%")
                || EF.Functions.Like(x.HotelCode, $"%{filter.Query}%")
                || EF.Functions.Like(x.HotelName, $"%{filter.Query}%")
                || EF.Functions.Like(x.CountryName, $"%{filter.Query}%")
                || EF.Functions.Like(x.CountryCode, $"%{filter.Query}%")
                || EF.Functions.Like(x.CityName, $"%{filter.Query}%")
                || EF.Functions.Like(x.CityCode, $"%{filter.Query}%")
                || EF.Functions.Like(x.DealType, $"%{filter.Query}%"))
                ;

                return await Task.FromResult(_mapper.Map<PaginatedList<HotelDeals>, PaginatedList<HotelDealsModal>>(new PaginatedList<HotelDeals>(result, filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _sectionRepository.GetAll(deleted: false);
                return await Task.FromResult(_mapper.Map<PaginatedList<HotelDeals>, PaginatedList<HotelDealsModal>>(new PaginatedList<HotelDeals>(result, filter.PageSize, filter.PageNumber)));
            }
        }
        public async Task<bool> SoftDelete(int Id)
        {
            var result = await _sectionRepository.Delete(Id);
            if ((bool)result.Deleted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<HotelDealsModal> GetById(int Id)
        {
            return _mapper.Map<HotelDeals, HotelDealsModal>(await _sectionRepository.Get(Id).Result.Include(x=>x.Portal).FirstOrDefaultAsync());

        }
    }
}
