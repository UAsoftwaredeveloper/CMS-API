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
    public class BookingJourneyDetailsService : IBookingJourneyDetailsService
    {
        private readonly IBookingJourneyDetailsRepository _subscribeRepository;
        private readonly IMapper _mapper;
        public BookingJourneyDetailsService(IBookingJourneyDetailsRepository usersRepository, IMapper mapper)
        {
            _subscribeRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PaginatedList<BookingJourneyDetailsModal>> GetAllBookingJourneyDetails(BookingJourneyDetailsFilter filter)
        {
            if (filter != null)
            {
                var result =
                _subscribeRepository.GetAll(deleted: false).Include(x=>x.BookingTransactionDetails)
                .WhereIf(filter.TransactionId !=null&&filter.TransactionId > 0, x => x.TransactionId == filter.TransactionId)
                .WhereIf(filter.FromDate !=null, x => x.BookingTransactionDetails.InsertedOn >= filter.FromDate.Value)
                .WhereIf(filter.ToDate !=null, x => x.BookingTransactionDetails.InsertedOn <= filter.ToDate.Value)
                .WhereIf(!string.IsNullOrEmpty(filter.AirlineCode), x => EF.Functions.Like(x.AirlineCode, $"%{filter.AirlineCode}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.AirlineClass), x => EF.Functions.Like(x.AirlineClass, $"%{filter.AirlineClass}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.AirlineName), x => EF.Functions.Like(x.AirlineName, $"%{filter.AirlineName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.CabinClass), x => EF.Functions.Like(x.CabinClass, $"%{filter.CabinClass}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.DestAirportCode), x => EF.Functions.Like(x.DestAirportCode, $"%{filter.DestAirportCode}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.DestAirportName), x => EF.Functions.Like(x.DestAirportName, $"%{filter.DestAirportName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.DestCityCode), x => EF.Functions.Like(x.DestCityCode, $"%{filter.DestCityCode}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.DestCityName), x => EF.Functions.Like(x.DestCityName, $"%{filter.DestCityName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.DestCountryCode), x => EF.Functions.Like(x.DestCountryCode, $"%{filter.DestCountryCode}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.DestCountryName), x => EF.Functions.Like(x.DestCountryName, $"%{filter.DestCountryName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.FlightName), x => EF.Functions.Like(x.FlightName, $"%{filter.FlightName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.FlightStatus), x => EF.Functions.Like(x.FlightStatus, $"%{filter.FlightStatus}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.JourneyType), x => EF.Functions.Like(x.JourneyType, $"%{filter.JourneyType}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.OriginAirportCode), x => EF.Functions.Like(x.OriginAirportCode, $"%{filter.OriginAirportCode}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.OriginAirportName), x => EF.Functions.Like(x.OriginAirportName, $"%{filter.OriginAirportName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.OriginCityCode), x => EF.Functions.Like(x.OriginCityCode, $"%{filter.OriginCityCode}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.OriginCityName), x => EF.Functions.Like(x.OriginCityName, $"%{filter.OriginCityName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.OriginCountryCode), x => EF.Functions.Like(x.OriginCountryCode, $"%{filter.OriginCountryCode}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.OriginCountryName), x => EF.Functions.Like(x.OriginCountryName, $"%{filter.OriginCountryName}%")).OrderByDescending(x => x.Id);

                return await Task.FromResult(_mapper.Map<PaginatedList<BookingJourneyDetails>, PaginatedList<BookingJourneyDetailsModal>>(new PaginatedList<BookingJourneyDetails>(result.ToList
                    (), filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _subscribeRepository.GetAll(deleted: false);
                return await Task.FromResult(_mapper.Map<PaginatedList<BookingJourneyDetails>, PaginatedList<BookingJourneyDetailsModal>>(new PaginatedList<BookingJourneyDetails>(result, filter.PageSize, filter.PageNumber)));
            }
        }

        public async Task<BookingJourneyDetailsModal> GetById(int Id)
        {
            return _mapper.Map<BookingJourneyDetails, BookingJourneyDetailsModal>(await _subscribeRepository.Entites().FirstOrDefaultAsync(x => x.Id == Id));

        }
    }
}
