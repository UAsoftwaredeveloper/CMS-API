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
    public class BlogEnqueryPageDetailsService : IBlogEnqueryPageDetailsService
    {
        private readonly IBlogEnqueryPageDetailsRepository _blogEnqueryPageDetailsRepository;
        private readonly IMapper _mapper;
        public BlogEnqueryPageDetailsService(IBlogEnqueryPageDetailsRepository blogEnqueryPageDetailsRepository, IMapper mapper)
        {
            _blogEnqueryPageDetailsRepository = blogEnqueryPageDetailsRepository ?? throw new ArgumentNullException(nameof(blogEnqueryPageDetailsRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PaginatedList<BlogEnqueryPageDetailsModal>> GetAllBlogEnqueryPageDetails(BlogEnqueryPageDetailsFilter filter)
        {
            if (filter != null)
            {
                var result =
                _blogEnqueryPageDetailsRepository.GetAll(deleted: false)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(filter.PageId > 0, x => x.PageId == filter.PageId)
                .WhereIf(filter.FromDate !=null, x => x.CreatedOn >= filter.FromDate.Value)
                .WhereIf(filter.ToDate !=null, x => x.CreatedOn <= filter.ToDate.Value)
                .WhereIf(!string.IsNullOrEmpty(filter.CustomerEmail), x => EF.Functions.Like(x.CustomerEmail, $"%{filter.CustomerEmail}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.CustomerPhone), x => EF.Functions.Like(x.CustomerPhone, $"%{filter.CustomerPhone}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.DestinationName), x => EF.Functions.Like(x.DestinationName, $"%{filter.DestinationName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Page_Name), x => EF.Functions.Like(x.Page_Name, $"%{filter.Page_Name}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.PageType), x => EF.Functions.Like(x.PageType, $"%{filter.PageType}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.PageUrl), x => EF.Functions.Like(x.PageUrl, $"%{filter.PageUrl}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.TravelDate), x => EF.Functions.Like(x.TravelDate, $"%{filter.TravelDate}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.TravellerCount), x => EF.Functions.Like(x.TravellerCount, $"%{filter.TravellerCount}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.EnquiryRefId), x => EF.Functions.Like(x.EnquiryRefId, $"%{filter.EnquiryRefId}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.DeviceType), x => EF.Functions.Like(x.DeviceType, $"%{filter.DeviceType}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Duration), x => EF.Functions.Like(x.Duration, $"%{filter.Duration}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.CustomerEmail, $"%{filter.Query}%")).OrderByDescending(x => x.Id);

                return await Task.FromResult(_mapper.Map<PaginatedList<BlogEnqueryPageDetails>, PaginatedList<BlogEnqueryPageDetailsModal>>(new PaginatedList<BlogEnqueryPageDetails>(result.ToList
                    (), filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _blogEnqueryPageDetailsRepository.GetAll(deleted: false);
                return await Task.FromResult(_mapper.Map<PaginatedList<BlogEnqueryPageDetails>, PaginatedList<BlogEnqueryPageDetailsModal>>(new PaginatedList<BlogEnqueryPageDetails>(result, filter.PageSize, filter.PageNumber)));
            }
        }

        public async Task<BlogEnqueryPageDetailsModal> GetById(int Id)
        {
            return _mapper.Map<BlogEnqueryPageDetails, BlogEnqueryPageDetailsModal>(await _blogEnqueryPageDetailsRepository.Entites().FirstOrDefaultAsync(x => x.Id == Id));

        }
    }
}
