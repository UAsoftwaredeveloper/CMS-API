using AutoMapper;
using Cms.Services.Extensions;
using Cms.Services.Filters;
using Cms.Services.Interfaces;
using Cms.Services.Models.CarHireDeals;
using CMS.Repositories.Interfaces;
using DataManager.DataClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Cms.Services.Services
{
    public class CarHireDealsService : ICarHireDealsService
    {
        //CarHireDealsRepositoryRepository
        public ICarHireDealsRepository _careHireDealRepository;
        public IMapper _mapper;
        public CarHireDealsService(ICarHireDealsRepository usersRepository, IMapper mapper) 
        {
            _careHireDealRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async Task<PaginatedList<CarHireDealsModal>> GetCarHireDealManagement(CarHireDealFilter filter)
        {
            if (filter != null)
            {
                var result =
                 _careHireDealRepository.GetAll(deleted: false).Include(x => x.Portal)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(filter.PortalId != null && filter.PortalId.Value > 0, x => x.PortalId == filter.PortalId.Value)
                .WhereIf(filter.Active != null && filter.Active.Value, x => x.Active == filter.Active)
                .WhereIf(filter.DealAmount != null && filter.DealAmount.Value > 0, x => x.PricePerDay == filter.DealAmount.Value)
                .WhereIf((filter.DealAmountFrom != null && filter.DealAmountFrom.Value > 0) && (filter.DealAmountTo != null && filter.DealAmountTo.Value > 0),
                        x => x.PricePerDay >= filter.DealAmountFrom.Value && x.PricePerDay <= filter.DealAmountTo)

                .WhereIf(!string.IsNullOrEmpty(filter.Origin), x => EF.Functions.Like(x.Origin, $"%{filter.Origin}%"))

                .WhereIf(!string.IsNullOrEmpty(filter.Destination), x => EF.Functions.Like(x.Destination, $"%{filter.Destination}%"))

                .WhereIf(!string.IsNullOrEmpty(filter.DealName), x => EF.Functions.Like(x.DealName, $"%{filter.DealName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.DealType), x => EF.Functions.Like(x.DealType, $"%{filter.DealType}%"))
                .WhereIf(!string.IsNullOrEmpty(Convert.ToString(filter.PricePerDay)), x => EF.Functions.Like(Convert.ToString(x.PricePerDay), $"%{filter.PricePerDay}%"))
               

                .WhereIf(!string.IsNullOrEmpty(filter.Query), x =>

                  EF.Functions.Like(x.Portal.Name, $"%{filter.Query}%")|| EF.Functions.Like(x.Origin, $"%{filter.Query}%")

                || EF.Functions.Like(x.Destination, $"%{filter.Query}%")
                || EF.Functions.Like(x.Origin, $"%{filter.Query}%")

                || EF.Functions.Like(x.DealType, $"%{filter.Query}%")
                || EF.Functions.Like(x.DealName, $"%{filter.Query}%")
                
                || EF.Functions.Like(Convert.ToString(x.PricePerDay), $"%{filter.Query}%") );
                

                return await Task.FromResult(_mapper.Map<PaginatedList<CarHireDeals>, PaginatedList<CarHireDealsModal>>(new PaginatedList<CarHireDeals>(result, filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _careHireDealRepository.GetAll(deleted: false).Include(x => x.Portal);
                return await Task.FromResult(_mapper.Map<PaginatedList<CarHireDeals>, PaginatedList<CarHireDealsModal>>(new PaginatedList<CarHireDeals>(result, filter.PageSize, filter.PageNumber)));
            }
        }

        public async Task<CarHireDealsModal> GetById(int Id)
        {
            return _mapper.Map<CarHireDeals, CarHireDealsModal>(await _careHireDealRepository.Get(Id).Result.Include(x => x.Portal).FirstOrDefaultAsync());
        }

        public async Task<CreateCarHireDealsModel> CreateDealManagement(CreateCarHireDealsModel modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {
                var result = await _careHireDealRepository.Insert(_mapper.Map<CreateCarHireDealsModel, CarHireDeals>(modal));
                return _mapper.Map<CarHireDeals, CreateCarHireDealsModel>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<UpdateCarHireModel> UpdateDealManagement(UpdateCarHireModel modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {
                var result = await _careHireDealRepository.Update(_mapper.Map<UpdateCarHireModel, CarHireDeals>(modal));
                return _mapper.Map<CarHireDeals, UpdateCarHireModel>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
