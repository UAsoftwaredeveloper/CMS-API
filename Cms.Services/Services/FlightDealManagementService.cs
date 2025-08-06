using AutoMapper;
using Cms.Services.Extensions;
using Cms.Services.Filters;
using Cms.Services.Interfaces;
using Cms.Services.Models.FlightDealManagement;
using CMS.Repositories.Interfaces;
using DataManager.DataClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Services.Services
{
    public class FlightDealManagementService : IFlightDealManagementService
    {
        public IFlightDealManagementRepository _flightDealRepository;
        private readonly IMapper _mapper;
        public FlightDealManagementService(IFlightDealManagementRepository usersRepository, IMapper mapper)
        {
            _flightDealRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<CreateFlightDealManagementModal> CreateFlightDealManagement(CreateFlightDealManagementModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {
                var result = await _flightDealRepository.Insert(_mapper.Map<CreateFlightDealManagementModal, FlightDealManagement>(modal));
                return _mapper.Map<FlightDealManagement, CreateFlightDealManagementModal>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<UpdateFlightDealManagementModal> UpdateFlightDealManagement(UpdateFlightDealManagementModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {
                var result = await _flightDealRepository.Update(_mapper.Map<UpdateFlightDealManagementModal, FlightDealManagement>(modal));
                return _mapper.Map<FlightDealManagement, UpdateFlightDealManagementModal>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<PaginatedList<FlightDealManagementModal>> GetAllFlightDealManagement(FlightDealManagementFilter filter)
        {
            if (filter != null)
            {
                var result =
                _flightDealRepository.GetAll(deleted: false).Include(x => x.Portal)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(filter.PortalId != null && filter.PortalId.Value > 0, x => x.PortalId == filter.PortalId.Value)
                .WhereIf(filter.Active != null && filter.Active.Value, x => x.Active == filter.Active)
                .WhereIf(filter.DealAmount != null && filter.DealAmount.Value > 0, x => x.DealAmount == filter.DealAmount.Value)
                .WhereIf((filter.DealAmountFrom != null && filter.DealAmountFrom.Value > 0) && (filter.DealAmountTo != null && filter.DealAmountTo.Value > 0),
                        x => x.DealAmount >= filter.DealAmountFrom.Value && x.DealAmount <= filter.DealAmountTo)
                .WhereIf(!string.IsNullOrEmpty(filter.AirlineCode), x => EF.Functions.Like(x.AirlineCode, $"%{filter.AirlineCode}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.AirlineName), x => EF.Functions.Like(x.AirlineName, $"%{filter.AirlineCode}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.OriginName), x => EF.Functions.Like(x.Origin, $"%{filter.Origin}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.OriginName), x => EF.Functions.Like(x.OriginName, $"%{filter.OriginName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Destination), x => EF.Functions.Like(x.Destination, $"%{filter.Destination}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.DestinationName), x => EF.Functions.Like(x.DestinationName, $"%{filter.DestinationName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.ClassType), x => EF.Functions.Like(x.ClassType, $"%{filter.ClassType}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.TripType), x => EF.Functions.Like(x.TripType, $"%{filter.TripType}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.DealType), x => EF.Functions.Like(x.DealType, $"%{filter.DealType}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.Portal.Name, $"%{filter.Query}%")
                || EF.Functions.Like(x.AirlineCode, $"%{filter.Query}%")
                || EF.Functions.Like(x.AirlineName, $"%{filter.Query}%")
                || EF.Functions.Like(x.Origin, $"%{filter.Query}%")
                || EF.Functions.Like(x.OriginName, $"%{filter.Query}%")
                || EF.Functions.Like(x.Destination, $"%{filter.Query}%")
                || EF.Functions.Like(x.DestinationName, $"%{filter.Query}%")
                || EF.Functions.Like(x.ClassType, $"%{filter.Query}%")
                || EF.Functions.Like(x.TripType, $"%{filter.Query}%")
                || EF.Functions.Like(x.DealType, $"%{filter.Query}%"))
                ;

                return await Task.FromResult(_mapper.Map<PaginatedList<FlightDealManagement>, PaginatedList<FlightDealManagementModal>>(new PaginatedList<FlightDealManagement>(result, filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _flightDealRepository.GetAll(deleted: false).Include(x => x.Portal);
                return await Task.FromResult(_mapper.Map<PaginatedList<FlightDealManagement>, PaginatedList<FlightDealManagementModal>>(new PaginatedList<FlightDealManagement>(result, filter.PageSize, filter.PageNumber)));
            }
        }
        public async Task<bool> SoftDelete(int Id)
        {
            var result = await _flightDealRepository.Delete(Id);
            if ((bool)result.Deleted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<FlightDealManagementModal> GetById(int Id)
        {
            return _mapper.Map<FlightDealManagement, FlightDealManagementModal>(await _flightDealRepository.Get(Id).Result.Include(x=>x.Portal).FirstOrDefaultAsync());

        }
    }
}
