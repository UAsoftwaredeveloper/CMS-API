using AutoMapper;
using Cms.Services.Extensions;
using Cms.Services.Filters;
using Cms.Services.Interfaces;
using Cms.Services.Models.CityCountry;
using CMS.Repositories.Interfaces;
using DataManager.DataClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Services.Services
{
    public class CityCountryService : ICityCountryService
    {
        public ICityCountryRepository _sectionRepository;
        public IMapper _mapper;
        public CityCountryService(ICityCountryRepository usersRepository, IMapper mapper)
        {
            _sectionRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<List<CityCountryModal>> GetAllCityCountry(CityCountryFilter filter)
        {

            if (filter != null)
            {
                var result =
                _sectionRepository.GetAll(deleted: false)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.CountryCode, $"%{filter.Query}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.CountryName, $"%{filter.Query}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.CityCode, $"%{filter.Query}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.CityName, $"%{filter.Query}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.CountryCode, $"%{filter.Country}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.CountryName, $"%{filter.Country}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.CityCode, $"%{filter.City}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.CityName, $"%{filter.City}%"))
                ;

                return await Task.FromResult(_mapper.Map<PaginatedList<CityCountry>, PaginatedList<CityCountryModal>>(new PaginatedList<CityCountry>(result.ToList
                    (), filter.PageSize, filter.PageNumber)).Result);
            }
            else
            {
                var result =
                _sectionRepository.GetAll(deleted: false);
                return await Task.FromResult(_mapper.Map<PaginatedList<CityCountry>, PaginatedList<CityCountryModal>>(new PaginatedList<CityCountry>(result, filter.PageSize, filter.PageNumber)).Result);
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
        public async Task<CityCountryModal> GetById(int Id)
        {
            return _mapper.Map<CityCountry, CityCountryModal>(await _sectionRepository.Get(Id).Result.FirstOrDefaultAsync());

        }
    }
}
