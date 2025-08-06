using AutoMapper;
using Cms.Services.Extensions;
using Cms.Services.Filters;
using Cms.Services.Interfaces;
using Cms.Services.Models.AirportDetails;
using CMS.Repositories.Interfaces;
using CMS.Repositories.Interfaces.ActivityAdmin;
using DataManager.DataClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Services.Services
{
    public class AirportDetailsService : IAirportDetailsService
    {
        public IAirportDetailsRepository _sectionRepository;
        public IMapper _mapper;
        public AirportDetailsService(IAirportDetailsRepository usersRepository, IMapper mapper)
        {
            _sectionRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PaginatedList<AirportDetailsModal>> GetAllAirportDetails(AirportDetailsFilter filter)
        {
            if (filter != null)
            {
                var result =
                _sectionRepository.GetAll(deleted: false)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.AirportCode, $"%{filter.Query}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.AirportName, $"%{filter.Query}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.CityCode, $"%{filter.Query}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.CityCode, $"%{filter.Query}%"))
                ;

                return await Task.FromResult(_mapper.Map<PaginatedList<AirportDetails>, PaginatedList<AirportDetailsModal>>(new PaginatedList<AirportDetails>(result.ToList
                    (), filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _sectionRepository.GetAll(deleted: false);
                return await Task.FromResult(_mapper.Map<PaginatedList<AirportDetails>, PaginatedList<AirportDetailsModal>>(new PaginatedList<AirportDetails>(result, filter.PageSize, filter.PageNumber)));
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
        public async Task<AirportDetailsModal> GetById(int Id)
        {
            return _mapper.Map<AirportDetails, AirportDetailsModal>(await _sectionRepository.Get(Id).Result.FirstOrDefaultAsync());

        }
    }
}
