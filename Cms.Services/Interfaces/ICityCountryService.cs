using Cms.Services.Filters;
using Cms.Services.Models.CityCountry;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces
{
    public interface ICityCountryService
    {
        Task<List<CityCountryModal>> GetAllCityCountry(CityCountryFilter filter);
        Task<CityCountryModal> GetById(int Id);
        Task<bool> SoftDelete(int Id);
    }
}