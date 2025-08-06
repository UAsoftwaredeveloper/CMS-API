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
    public class ContactUsService : IContactUsService
    {
        private readonly IContactUsRepository _subscribeRepository;
        private readonly IMapper _mapper;
        public ContactUsService(IContactUsRepository usersRepository, IMapper mapper)
        {
            _subscribeRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PaginatedList<ContactUsModal>> GetAllContactUs(ContactUsFilter filter)
        {
            if (filter != null)
            {
                var result =
                _subscribeRepository.GetAll(deleted: false)
                .WhereIf(filter.Id > 0, x => x.ID == filter.Id)
                .WhereIf(filter.PortalID > 0, x => x.PortalID == filter.PortalID)
                .WhereIf(filter.FromDate !=null, x => x.CreationTime >= filter.FromDate.Value)
                .WhereIf(filter.ToDate !=null, x => x.CreationTime <= filter.ToDate.Value)
                .WhereIf(!string.IsNullOrEmpty(filter.Email), x => EF.Functions.Like(x.Email, $"%{filter.Email}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Name), x => EF.Functions.Like(x.Name, $"%{filter.Name}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Ref_No), x => EF.Functions.Like(x.Ref_No, $"%{filter.Ref_No}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.Email, $"%{filter.Query}%")).OrderByDescending(x => x.ID);

                return await Task.FromResult(_mapper.Map<PaginatedList<ContactUs>, PaginatedList<ContactUsModal>>(new PaginatedList<ContactUs>(result.ToList
                    (), filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _subscribeRepository.GetAll(deleted: false);
                return await Task.FromResult(_mapper.Map<PaginatedList<ContactUs>, PaginatedList<ContactUsModal>>(new PaginatedList<ContactUs>(result, filter.PageSize, filter.PageNumber)));
            }
        }

        public async Task<ContactUsModal> GetById(int Id)
        {
            return _mapper.Map<ContactUs, ContactUsModal>(await _subscribeRepository.Entites().FirstOrDefaultAsync(x => x.ID == Id));

        }
    }
}
