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
    public class SubscribeService : ISubscribeService
    {
        private readonly ISubscribeRepository _subscribeRepository;
        private readonly IMapper _mapper;
        public SubscribeService(ISubscribeRepository usersRepository, IMapper mapper)
        {
            _subscribeRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PaginatedList<SubscribeModal>> GetAllSubscribe(SubscribeFilter filter)
        {
            try
            {
                if (filter != null)
                {
                    var result =
                    _subscribeRepository.GetAll(deleted: false).Include(x => x.Users)
                    .WhereIf(filter.Id > 0, x => x.ID == filter.Id)
                    .WhereIf(filter.PortalID > 0, x => x.PortalID == filter.PortalID)
                    .WhereIf(filter.Active != null, x => x.IsActive == filter.Active)
                    .WhereIf(filter.PageId != null, x => x.PageId == filter.PageId)
                    .WhereIf(filter.FromDate != null, x => x.CreationTime >= filter.FromDate.Value)
                    .WhereIf(filter.ToDate != null, x => x.CreationTime <= filter.ToDate.Value)
                    .WhereIf(!string.IsNullOrEmpty(filter.Email), x => EF.Functions.Like(x.Email, $"%{filter.Email}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.PageName), x => EF.Functions.Like(x.PageName, $"%{filter.PageName}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.Email, $"%{filter.Query}%")).OrderByDescending(x=>x.ID);

                    return await Task.FromResult(_mapper.Map<PaginatedList<Subscribe>, PaginatedList<SubscribeModal>>(new PaginatedList<Subscribe>(result.ToList
                        (), filter.PageSize, filter.PageNumber)));
                }
                else
                {
                    var result =
                    _subscribeRepository.GetAll(deleted: false);
                    return await Task.FromResult(_mapper.Map<PaginatedList<Subscribe>, PaginatedList<SubscribeModal>>(new PaginatedList<Subscribe>(result, filter.PageSize, filter.PageNumber)));
                }
            }
            catch (Exception ex) { throw; }

        }

        public async Task<SubscribeModal> GetById(int Id)
        {
            return _mapper.Map<Subscribe, SubscribeModal>(await _subscribeRepository.Entites().Include(x => x.Users).FirstOrDefaultAsync(x => x.ID == Id));

        }
    }
}
