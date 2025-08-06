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
    public class BookingPaxDetailsService : IBookingPaxDetailsService
    {
        private readonly IBookingPaxDetailsRepository _subscribeRepository;
        private readonly IMapper _mapper;
        public BookingPaxDetailsService(IBookingPaxDetailsRepository usersRepository, IMapper mapper)
        {
            _subscribeRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PaginatedList<BookingPaxDetailsModal>> GetAllBookingPaxDetails(BookingPaxDetailsFilter filter)
        {
            if (filter != null)
            {
                var result =
                _subscribeRepository.GetAll(deleted: false).Include(x=>x.BookingTransactionDetails)
                .WhereIf(filter.TransactionId != null, x => x.TransactionId == filter.TransactionId)
                .WhereIf(filter.DOBFrom !=null, x => x.DOB >= filter.DOBFrom.Value)
                .WhereIf(filter.DOBTo !=null, x => x.DOB <= filter.DOBTo.Value)
                .WhereIf(filter.FromDate !=null, x => x.BookingTransactionDetails.InsertedOn >= filter.FromDate.Value)
                .WhereIf(filter.ToDate !=null, x => x.BookingTransactionDetails.InsertedOn <= filter.ToDate.Value)
                .WhereIf(!string.IsNullOrEmpty(filter.Gender), x => EF.Functions.Like(x.Gender, $"%{filter.Gender}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Nationality), x => EF.Functions.Like(x.Nationality, $"%{filter.Nationality}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.Nationality, $"%{filter.Query}%"))
                .OrderByDescending(x=>x.Id);

                return await Task.FromResult(_mapper.Map<PaginatedList<BookingPaxDetails>, PaginatedList<BookingPaxDetailsModal>>(new PaginatedList<BookingPaxDetails>(result.ToList
                    (), filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _subscribeRepository.GetAll(deleted: false);
                return await Task.FromResult(_mapper.Map<PaginatedList<BookingPaxDetails>, PaginatedList<BookingPaxDetailsModal>>(new PaginatedList<BookingPaxDetails>(result, filter.PageSize, filter.PageNumber)));
            }
        }

        public async Task<BookingPaxDetailsModal> GetById(int Id)
        {
            return _mapper.Map<BookingPaxDetails, BookingPaxDetailsModal>(await _subscribeRepository.Entites().FirstOrDefaultAsync(x => x.Id == Id));

        }
    }
}
