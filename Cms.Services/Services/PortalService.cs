using AutoMapper;
using Cms.Services.Extensions;
using Cms.Services.Filters;
using Cms.Services.Interfaces;
using Cms.Services.Models.Portals;
using CMS.Repositories.Interfaces;
using DataManager.DataClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Services.Services
{
    public class PortalService : IPortalService
    {
        public IPortalRepository _sectionRepository;
        public IMapper _mapper;
        public PortalService(IPortalRepository usersRepository, IMapper mapper)
        {
            _sectionRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<CreatePortalModal> CreatePortal(CreatePortalModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {
                var exist = await _sectionRepository.GetAll(false).AnyAsync(x => (x.Name == modal.Name));
                if (exist)
                    throw new DuplicateNameException("409");

                var result = await _sectionRepository.Insert(_mapper.Map<CreatePortalModal, Portals>(modal));
                return _mapper.Map<Portals, CreatePortalModal>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<UpdatePortalModal> UpdatePortal(UpdatePortalModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {
                var exist = await _sectionRepository.GetAll(false).AnyAsync(x => (x.Id != modal.Id && x.Name == modal.Name));
                if (exist)
                    throw new DuplicateNameException("409");

                var result = await _sectionRepository.Update(_mapper.Map<UpdatePortalModal, Portals>(modal));
                return _mapper.Map<Portals, UpdatePortalModal>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<PaginatedList<PortalModal>> GetAllPortal(PortalFilter filter)
        {
            if (filter != null)
            {
                var result =
                _sectionRepository.GetAll(deleted: false)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(!string.IsNullOrEmpty(filter.Name), x => EF.Functions.Like(x.Name, $"%{filter.Name}%"));
                return await Task.FromResult(_mapper.Map<PaginatedList<Portals>, PaginatedList<PortalModal>>(new PaginatedList<Portals>(result.ToList
                    (), filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _sectionRepository.GetAll(deleted: false);
                return await Task.FromResult(_mapper.Map<PaginatedList<Portals>, PaginatedList<PortalModal>>(new PaginatedList<Portals>(result, filter.PageSize, filter.PageNumber)));
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
        public async Task<PortalModal> GetById(int Id)
        {
            return _mapper.Map<Portals, PortalModal>(await _sectionRepository.Get(Id).Result.FirstOrDefaultAsync());

        }
    }
}
