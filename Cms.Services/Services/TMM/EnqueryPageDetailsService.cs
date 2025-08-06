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
    public class EnqueryPageDetailsService : IEnqueryPageDetailsService
    {
        private readonly IEnqueryPageDetailsRepository _enqueryPageDetailsRepository;
        private readonly IMapper _mapper;
        public EnqueryPageDetailsService(IEnqueryPageDetailsRepository EnqueryPageDetailsRepository, IMapper mapper)
        {
            _enqueryPageDetailsRepository = EnqueryPageDetailsRepository ?? throw new ArgumentNullException(nameof(EnqueryPageDetailsRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PaginatedList<EnqueryPageDetailsModal>> GetAllEnqueryPageDetails(EnqueryPageDetailsFilter filter)
        {
            if (filter != null)
            {
                var result =
                _enqueryPageDetailsRepository.GetAll(deleted: false)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(filter.PageId != null && filter.PageId.Value > 0, x => x.Id == filter.Id)
                .WhereIf(filter.FromDate != null, x => x.CreatedOn >= filter.FromDate.Value)
                .WhereIf(filter.ToDate != null, x => x.CreatedOn <= filter.ToDate.Value)
                .WhereIf(filter.Active != null, x => x.Deleted == filter.Active)
                .WhereIf(!string.IsNullOrEmpty(filter.Email), x => EF.Functions.Like(x.CustomerEmail, $"%{filter.Email}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.PageType), x => EF.Functions.Like(x.PageType, $"%{filter.PageType}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.PageName), x => EF.Functions.Like(x.Page_Name, $"%{filter.PageName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.PageUrl), x => EF.Functions.Like(x.PageUrl, $"%{filter.PageUrl}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x =>
                EF.Functions.Like(x.PageType, $"%{filter.Query}%") ||
                EF.Functions.Like(x.Page_Name, $"%{filter.Query}%") ||
                EF.Functions.Like(x.PageUrl, $"%{filter.Query}%") ||
                EF.Functions.Like(x.CustomerEmail, $"%{filter.Query}%")).OrderByDescending(x => x.Id);

                return await Task.FromResult(_mapper.Map<PaginatedList<EnqueryPageDetails>, PaginatedList<EnqueryPageDetailsModal>>(new PaginatedList<EnqueryPageDetails>(result.ToList
                    (), filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _enqueryPageDetailsRepository.GetAll(deleted: false);
                return await Task.FromResult(_mapper.Map<PaginatedList<EnqueryPageDetails>, PaginatedList<EnqueryPageDetailsModal>>(new PaginatedList<EnqueryPageDetails>(result, filter.PageSize, filter.PageNumber)));
            }
        }

        public async Task<EnqueryPageDetailsModal> GetById(int Id)
        {
            return _mapper.Map<EnqueryPageDetails, EnqueryPageDetailsModal>(await _enqueryPageDetailsRepository.Entites().FirstOrDefaultAsync(x => x.Id == Id));

        }
    }
}
