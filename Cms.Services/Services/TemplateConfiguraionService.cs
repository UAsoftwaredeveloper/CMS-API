using AutoMapper;
using Cms.Services.Extensions;
using Cms.Services.Filters;
using Cms.Services.Interfaces;
using Cms.Services.Models.OpenAPIDataModel.TemplateConfiguration;
using Cms.Services.Models.TemplateConfiguration;
using Cms.Services.Models.TemplateMaster;
using CMS.Repositories.Interfaces;
using CMS.Repositories.Repositories;
using DataManager.DataClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Services.Services
{
    public class TemplateConfigurationService : ITemplateConfigurationService
    {
        public ITemplateConfigurationRepository _userTemplateConfigurationRepository;
        public IMapper _mapper;
        public TemplateConfigurationService(ITemplateConfigurationRepository usersRepository, IMapper mapper)
        {
            _userTemplateConfigurationRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<CreateTemplateConfigurationModal> CreateTemplateConfiguration(CreateTemplateConfigurationModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {
                var exist = await _userTemplateConfigurationRepository.GetAll(false).AnyAsync(x => (x.TemplateDetailsId == modal.TemplateDetailsId));
                if (exist)
                    throw new DuplicateNameException("409");

                var result = await _userTemplateConfigurationRepository.Insert(_mapper.Map<CreateTemplateConfigurationModal, TemplateConfiguration>(modal));
                return _mapper.Map<TemplateConfiguration, CreateTemplateConfigurationModal>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<UpdateTemplateConfigurationModal> UpdateTemplateConfiguration(UpdateTemplateConfigurationModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {
                var exist = await _userTemplateConfigurationRepository.GetAll(false).AnyAsync(x => (x.Id!=modal.Id &&x.TemplateDetailsId == modal.TemplateDetailsId));
                if (exist)
                    throw new DuplicateNameException("409");

                var result = await _userTemplateConfigurationRepository.Update(_mapper.Map<UpdateTemplateConfigurationModal, TemplateConfiguration>(modal));
                return _mapper.Map<TemplateConfiguration, UpdateTemplateConfigurationModal>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<PaginatedList<TemplateConfigurationModal>> GetAllTemplateConfiguration(TemplateConfigurationFilter filter)
        {
            if (filter != null)
            {
                var result =
                _userTemplateConfigurationRepository.GetAll(deleted: false).Include(x => x.TemplateDetails)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(!string.IsNullOrEmpty(filter.TemplateName), x => EF.Functions.Like(x.TemplateDetails.Title, $"%{filter.TemplateName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.TemplateDetails.Title, $"%{filter.Query}%")
                || EF.Functions.Like(x.TemplateDetails.FullDescription, $"%{filter.Query}%"));
                return await Task.FromResult(_mapper.Map<PaginatedList<TemplateConfiguration>, PaginatedList<TemplateConfigurationModal>>(new PaginatedList<TemplateConfiguration>(result, filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _userTemplateConfigurationRepository.GetAll(deleted: false).Include(x => x.TemplateDetails);
                return await Task.FromResult(_mapper.Map<PaginatedList<TemplateConfiguration>, PaginatedList<TemplateConfigurationModal>>(new PaginatedList<TemplateConfiguration>(result, filter.PageSize, filter.PageNumber)));
            }
        }
        public async Task<bool> SoftDelete(int Id)
        {
            var result = await _userTemplateConfigurationRepository.Delete(Id);
            if ((bool)result.Deleted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<TemplateConfigurationModal> GetById(int Id)
        {
            return _mapper.Map<TemplateConfiguration, TemplateConfigurationModal>(await _userTemplateConfigurationRepository.Get(Id).Result.FirstOrDefaultAsync());

        }
    }
}
