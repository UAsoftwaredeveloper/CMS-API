using AutoMapper;
using Cms.Services.Extensions;
using Cms.Services.Filters;
using Cms.Services.Interfaces;
using Cms.Services.Models.MasterAirlines;
using CMS.Repositories.Interfaces;
using DataManager.DataClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Services.Services
{
    public class MasterAirlinesService : IMasterAirlinesService
    {
        public IMasterAirlinesRepository _sectionRepository;
        public IMapper _mapper;
        public MasterAirlinesService(IMasterAirlinesRepository usersRepository, IMapper mapper)
        {
            _sectionRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PaginatedList<MasterAirlinesModal>> GetAllMasterAirlines(MasterAirlinesFilter filter)
        {
            if (filter != null)
            {
                var result =
                _sectionRepository.GetAll(deleted: false)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.MasterID, $"%{filter.Query}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.Name, $"%{filter.Query}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.Slug, $"%{filter.Query}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.Code, $"%{filter.Query}%"))
                ;

                return await Task.FromResult(_mapper.Map<PaginatedList<MasterAirlines>, PaginatedList<MasterAirlinesModal>>(new PaginatedList<MasterAirlines>(result.ToList
                    (), filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _sectionRepository.GetAll(deleted: false);
                return await Task.FromResult(_mapper.Map<PaginatedList<MasterAirlines>, PaginatedList<MasterAirlinesModal>>(new PaginatedList<MasterAirlines>(result, filter.PageSize, filter.PageNumber)));
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
        public async Task<MasterAirlinesModal> GetById(int Id)
        {
            return _mapper.Map<MasterAirlines, MasterAirlinesModal>(await _sectionRepository.Get(Id).Result.FirstOrDefaultAsync());

        }
    }
}
