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
    public class CustomerReviewRatingsService : ICustomerReviewRatingsService
    {
        private readonly ICustomerReviewRatingsRepository _bookingRepository;
        private readonly IMapper _mapper;
        public CustomerReviewRatingsService(ICustomerReviewRatingsRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<CustomerReviewRatingsModal> UpdateCustomerReviewRatings(CustomerReviewRatingsModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {
                var result = await _bookingRepository.Update(_mapper.Map<CustomerReviewRatingsModal, CustomerReviewRatings>(modal));
                if (result.Id > 0)
                {
                    return _mapper.Map<CustomerReviewRatings, CustomerReviewRatingsModal>(result);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public async Task<CustomerReviewRatingsModal> CreateCustomerReviewRatings(CustomerReviewRatingsModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {
                var result = await _bookingRepository.Insert(_mapper.Map<CustomerReviewRatingsModal, CustomerReviewRatings>(modal));
                if (result.Id > 0)
                {
                    return _mapper.Map<CustomerReviewRatings, CustomerReviewRatingsModal>(result);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public async Task<PaginatedList<CustomerReviewRatingsModal>> GetAllCustomerReviewRatings(CustomerReviewRatingsFilter filter)
        {
            if (filter != null)
            {
                var result =
                _bookingRepository.GetAll(deleted: false).Include(x => x.Users)
                .WhereIf(filter.PortalId != null, x => x.PortalId == filter.PortalId)
                .WhereIf(filter.UserId != null, x => x.UserId == filter.UserId)
                .WhereIf(filter.Approved != null, x => x.Approved == filter.Approved)
                .WhereIf(filter.FromDate != null, x => x.Created_On >= filter.FromDate.Value)
                .WhereIf(filter.ToDate != null, x => x.Created_On <= filter.ToDate.Value)
                .WhereIf(filter.Ratings > 0, x => x.Ratings == filter.Ratings)
                .WhereIf(!string.IsNullOrEmpty(filter.emailId), x => EF.Functions.Like(x.emailId, $"%{filter.emailId}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.FullName), x => EF.Functions.Like(x.FullName, $"%{filter.FullName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.PhoneNumber), x => EF.Functions.Like(x.PhoneNumber, $"%{filter.PhoneNumber}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.PageType), x => EF.Functions.Like(x.PageType, $"%{filter.PageType}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.FileType), x => EF.Functions.Like(x.FileType, $"%{filter.FileType}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.emailId, $"%{filter.Query}%")
                || EF.Functions.Like(x.ReviewComments, $"%{filter.Query}%")
                || EF.Functions.Like(x.UserType, $"%{filter.Query}%")
                || EF.Functions.Like(x.FullName, $"%{filter.Query}%")
                || EF.Functions.Like(x.PageType, $"%{filter.Query}%")
                || EF.Functions.Like(x.FileType, $"%{filter.Query}%")
                ).OrderByDescending(x => x.Id);
                return await Task.FromResult(_mapper.Map<PaginatedList<CustomerReviewRatings>, PaginatedList<CustomerReviewRatingsModal>>(new PaginatedList<CustomerReviewRatings>(result.ToList
                    (), filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _bookingRepository.GetAll(deleted: false);
                return await Task.FromResult(_mapper.Map<PaginatedList<CustomerReviewRatings>, PaginatedList<CustomerReviewRatingsModal>>(new PaginatedList<CustomerReviewRatings>(result, filter.PageSize, filter.PageNumber)));
            }
        }
        public async Task<PaginatedList<CustomerReviewRatingsData>> GetAllCustomerReviewRatingsPublic(CustomerReviewRatingsFilter filter)
        {
            if (filter != null)
            {
                var query = _bookingRepository
                            .GetAll(deleted: false)
                            .Include(x => x.Users)
                            .WhereIf(filter.PortalId != null, x => x.PortalId == filter.PortalId)
                            .WhereIf(filter.Approved != null, x => x.Approved == filter.Approved)
                            .WhereIf(filter.FromDate != null, x => x.Created_On >= filter.FromDate.Value)
                            .WhereIf(filter.ToDate != null, x => x.Created_On <= filter.ToDate.Value)
                            .WhereIf(filter.Ratings > 0, x => x.Ratings == filter.Ratings)
                            .WhereIf(!string.IsNullOrEmpty(filter.PageType), x => x.PageType == filter.PageType)
                            .Where(x =>
                                (filter.UserId == null && x.Approved == true)
                                ||
                                (filter.UserId != null && (x.UserId == filter.UserId || x.Approved == true))
                            );
                if (filter.SortDescending != null && filter.SortDescending.Value)
                    query = query.OrderByDescending(x => x.Created_On);
                else if (filter.SortHighestRating != null && filter.SortHighestRating.Value)
                    query = query.OrderByDescending(x => x.Ratings);
                else if (filter.SortLowestRating != null && filter.SortLowestRating.Value)
                    query = query.OrderBy(x => x.Ratings);
                else
                    query = query.OrderByDescending(x => x.Created_On);
                return await Task.FromResult(_mapper.Map<PaginatedList<CustomerReviewRatings>, PaginatedList<CustomerReviewRatingsData>>(new PaginatedList<CustomerReviewRatings>(query.ToList(), filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _bookingRepository.GetAll(deleted: false).Include(x => x.Users);
                return await Task.FromResult(_mapper.Map<PaginatedList<CustomerReviewRatings>, PaginatedList<CustomerReviewRatingsData>>(new PaginatedList<CustomerReviewRatings>(result, filter.PageSize, filter.PageNumber)));
            }
        }

        public async Task<CustomerReviewRatingsModal> GetById(long Id)
        {
            return _mapper.Map<CustomerReviewRatings, CustomerReviewRatingsModal>(await _bookingRepository.Entites().Include(x => x.Users).FirstOrDefaultAsync(x => x.Id == Id));

        }
    }
}
