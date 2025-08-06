using AutoMapper;
using Cms.Services.Extensions;
using Cms.Services.Filters;
using Cms.Services.Interfaces;
using Cms.Services.Models.TemplateMaster;
using CMS.Repositories.Interfaces;
using DataManager.DataClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Services.Services
{
    public class TemplateMasterService : ITemplateMasterService
    {
        public ITemplateMasterRepository _userTemplateMasterRepository;
        public IMapper _mapper;
        public TemplateMasterService(ITemplateMasterRepository usersRepository, IMapper mapper)
        {
            _userTemplateMasterRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<CreateTemplateMasterModal> CreateTemplateMaster(CreateTemplateMasterModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {
                var exist = await _userTemplateMasterRepository.GetAll(false).AnyAsync(x => x.Name == modal.Name);
                if (exist)
                    throw new DuplicateNameException("This record with these details already exist");

                var result = await _userTemplateMasterRepository.Insert(_mapper.Map<CreateTemplateMasterModal, TemplateMaster>(modal));
                return _mapper.Map<TemplateMaster, CreateTemplateMasterModal>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<UpdateTemplateMasterModal> UpdateTemplateMaster(UpdateTemplateMasterModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {
                //var exist=await _userTemplateMasterRepository.GetAll(false).AnyAsync(x=>x.Name == modal.Name);
                //if (exist)
                //    throw new DuplicateNameException("This record with these details already exist");
                    var result = await _userTemplateMasterRepository.Update(_mapper.Map<UpdateTemplateMasterModal, TemplateMaster>(modal));
                return _mapper.Map<TemplateMaster, UpdateTemplateMasterModal>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<PaginatedList<TemplateMasterModal>> GetAllTemplateMaster(TemplateMasterFilter filter)
        {
            if (filter != null)
            {
                var result =
                _userTemplateMasterRepository.GetAll(deleted: false).Include(x=>x.Portal)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(filter.Active != null, x => x.Active == filter.Active)
                .WhereIf(filter.PortalId!=null && filter.PortalId.Value > 0, x => x.PortalId == filter.PortalId)
                .WhereIf(!string.IsNullOrEmpty(filter.PortalName), x => EF.Functions.Like(x.Portal.Name, $"%{filter.PortalName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.TemplateName), x => EF.Functions.Like(x.Name, $"%{filter.TemplateName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.Name, $"%{filter.Query}%")
                || EF.Functions.Like(x.Description, $"%{filter.Query}%"));
                return await Task.FromResult(_mapper.Map<PaginatedList<TemplateMaster>, PaginatedList<TemplateMasterModal>>(new PaginatedList<TemplateMaster>(result, filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _userTemplateMasterRepository.GetAll(deleted: false);
                return await Task.FromResult(_mapper.Map<PaginatedList<TemplateMaster>, PaginatedList<TemplateMasterModal>>(new PaginatedList<TemplateMaster>(result, filter.PageSize, filter.PageNumber)));
            }
        }
        public async Task<bool> SoftDelete(int Id)
        {
            var result = await _userTemplateMasterRepository.Delete(Id);
            if ((bool)result.Deleted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<TemplateMasterModal> GetById(int Id)
        {
            return _mapper.Map<TemplateMaster,TemplateMasterModal>(await _userTemplateMasterRepository.Get(Id).Result.Include(x => x.Portal).FirstOrDefaultAsync());

        }
    }
}
