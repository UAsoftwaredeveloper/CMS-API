using AutoMapper;
using Cms.Services.Extensions;
using Cms.Services.Filters.TMM;
using Cms.Services.Interfaces.TMM;
using Cms.Services.Models.TMMModals;
using CMS.Repositories.Interfaces.TMM;
using DataManager.TMMDbClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Services.Services.TMM
{
    public class HotelSearchDetailsService : IHotelSearchDetailsService
    {
        private readonly IHotelSearchDetailsRepository _hotelSearchDetailsRepository;
        private readonly IMapper _mapper;
        public HotelSearchDetailsService(IHotelSearchDetailsRepository hotelSearchDetailsRepository, IMapper mapper)
        {
            _hotelSearchDetailsRepository = hotelSearchDetailsRepository ?? throw new ArgumentNullException(nameof(hotelSearchDetailsRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PaginatedList<HotelSearchDetailsModal>> GetAllHotelSearchDetails(HotelSearchDetailsFilter filter)
        {
            if (filter != null)
            {
                var result =
                _hotelSearchDetailsRepository.GetAll(deleted: false)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(filter.ChildCount != null, x => x.ChildCount == filter.ChildCount)
                .WhereIf(filter.RoomCount != null, x => x.RoomCount == filter.RoomCount)
                .WhereIf(filter.AdultCount != null, x => x.AdultCount == filter.AdultCount)
                .WhereIf(filter.PortalID != null, x => x.PortalID == filter.PortalID)
                .WhereIf(filter.CheckInFrom != null, x => x.CheckIn >= filter.CheckInFrom)
                .WhereIf(filter.CheckInTo != null, x => x.CheckOut <= filter.CheckInTo)
                .WhereIf(filter.CheckOutFrom != null, x => x.CheckOut >= filter.CheckOutFrom)
                .WhereIf(filter.CheckOutTo != null, x => x.CheckOut <= filter.CheckOutTo)
                .WhereIf(filter.FromDate != null, x => x.SearchDate >= filter.FromDate)
                .WhereIf(filter.ToDate != null, x => x.SearchDate <= filter.ToDate)
                .WhereIf(!string.IsNullOrEmpty(filter.CountryCode), x => x.CountryCode == filter.CountryCode)
                .WhereIf(!string.IsNullOrEmpty(filter.Destination), x => x.Destination == filter.Destination)
                .WhereIf(!string.IsNullOrEmpty(filter.CountryName), x => x.CountryName== filter.CountryName)
                .WhereIf(!string.IsNullOrEmpty(filter.UTM_Campaign), x => x.UTM_Source == filter.UTM_Campaign)
                .WhereIf(!string.IsNullOrEmpty(filter.UTM_Source), x => x.UTM_Source == filter.UTM_Source)
                .WhereIf(!string.IsNullOrEmpty(filter.DeviceType), x => x.DeviceType == filter.DeviceType)
                .WhereIf(!string.IsNullOrEmpty(filter.Latitude), x => x.Latitude== filter.Latitude)
                .WhereIf(!string.IsNullOrEmpty(filter.Longitude), x => x.Longitude == filter.Longitude)
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x =>
                EF.Functions.Like(x.Destination, $"%{filter.Query}%") ||
                EF.Functions.Like(x.Latitude, $"%{filter.Query}%") ||
                EF.Functions.Like(x.Longitude, $"%{filter.Query}%") ||
                EF.Functions.Like(x.DeviceType, $"%{filter.Query}%") ||
                EF.Functions.Like(x.UTM_Source, $"%{filter.Query}%") ||
                EF.Functions.Like(x.CountryCode, $"%{filter.Query}%") ||
                EF.Functions.Like(x.CountryName, $"%{filter.Query}%")).OrderByDescending(x => x.Id);

                return await Task.FromResult(_mapper.Map<PaginatedList<HotelSearchDetails>, PaginatedList<HotelSearchDetailsModal>>(new PaginatedList<HotelSearchDetails>(result.ToList
                    (), filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _hotelSearchDetailsRepository.GetAll(deleted: false);
                return await Task.FromResult(_mapper.Map<PaginatedList<HotelSearchDetails>, PaginatedList<HotelSearchDetailsModal>>(new PaginatedList<HotelSearchDetails>(result, filter.PageSize, filter.PageNumber)));
            }
        }

        public async Task<HotelSearchDetailsModal> GetById(int Id)
        {
            return _mapper.Map<HotelSearchDetails, HotelSearchDetailsModal>(await _hotelSearchDetailsRepository.Entites().FirstOrDefaultAsync(x => x.Id == Id));

        }
    }
}
