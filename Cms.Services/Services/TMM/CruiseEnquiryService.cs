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
    public class CruiseEnquiryService : ICruiseEnquiryService
    {
        private readonly ICruiseEnquiryRepository _cruiseEnquiryRepository;
        private readonly IMapper _mapper;
        public CruiseEnquiryService(ICruiseEnquiryRepository cruiseEnquiryRepository, IMapper mapper)
        {
            _cruiseEnquiryRepository = cruiseEnquiryRepository ?? throw new ArgumentNullException(nameof(cruiseEnquiryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PaginatedList<CruiseEnquiryModal>> GetAllCruiseEnquiry(CruiseEnquiryFilter filter)
        {
            if (filter != null)
            {
                var result =
                _cruiseEnquiryRepository.GetAll(deleted: false)
                .WhereIf(filter.Id > 0, x => x.ID == filter.Id)
                .WhereIf(filter.PortalID > 0, x => x.PortalID == filter.PortalID)
                .WhereIf(filter.RoomAdultCount > 0, x => x.RoomAdultCount == filter.RoomAdultCount)
                .WhereIf(filter.RoomChildCount > 0, x => x.RoomChildCount == filter.RoomChildCount)
                .WhereIf(filter.FromDate !=null, x => x.CreationTime >= filter.FromDate.Value)
                .WhereIf(filter.ToDate !=null, x => x.CreationTime <= filter.ToDate.Value)
                .WhereIf(!string.IsNullOrEmpty(filter.Email), x => EF.Functions.Like(x.Email, $"%{filter.Email}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.ShipCode), x => EF.Functions.Like(x.ShipCode, $"%{filter.ShipCode}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.ShipName), x => EF.Functions.Like(x.ShipName, $"%{filter.ShipName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Airports), x => EF.Functions.Like(x.Airports, $"%{filter.Airports}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Departure), x => EF.Functions.Like(x.Departure, $"%{filter.Departure}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.NeedAccessibleCabin), x => EF.Functions.Like(x.NeedAccessibleCabin, $"%{filter.NeedAccessibleCabin}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Duration), x => EF.Functions.Like(x.Duration, $"%{filter.Duration}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.MobileNo), x => EF.Functions.Like(x.MobileNo, $"%{filter.MobileNo}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.SelectedCabinCount), x => EF.Functions.Like(x.SelectedCabinCount, $"%{filter.SelectedCabinCount}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.SelectedCabin), x => EF.Functions.Like(x.SelectedCabin, $"%{filter.SelectedCabin}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Ref_No), x => EF.Functions.Like(x.Ref_No, $"%{filter.Ref_No}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.CruiseLineCode), x => EF.Functions.Like(x.CruiseLineCode, $"%{filter.CruiseLineCode}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.CruiseLineName), x => EF.Functions.Like(x.CruiseLineName, $"%{filter.CruiseLineName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Ref_No), x => EF.Functions.Like(x.Ref_No, $"%{filter.Ref_No}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.Email, $"%{filter.Query}%"));

                return await Task.FromResult(_mapper.Map<PaginatedList<CruiseEnquiry>, PaginatedList<CruiseEnquiryModal>>(new PaginatedList<CruiseEnquiry>(result.ToList
                    (), filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _cruiseEnquiryRepository.GetAll(deleted: false);
                return await Task.FromResult(_mapper.Map<PaginatedList<CruiseEnquiry>, PaginatedList<CruiseEnquiryModal>>(new PaginatedList<CruiseEnquiry>(result, filter.PageSize, filter.PageNumber)));
            }
        }
        public async Task<PaginatedList<CruiseEnquiryModal>> GetUsersAllCruiseEnquiry(CruiseEnquiryFilter filter)
        {
            if (filter != null)
            {
                var result =
                _cruiseEnquiryRepository.GetAll(deleted: false)
                .WhereIf(filter.Id > 0, x => x.ID == filter.Id)
                .WhereIf(filter.PortalID > 0, x => x.PortalID == filter.PortalID)
                .WhereIf(filter.RoomAdultCount > 0, x => x.RoomAdultCount == filter.RoomAdultCount)
                .WhereIf(filter.RoomChildCount > 0, x => x.RoomChildCount == filter.RoomChildCount)
                .WhereIf(filter.FromDate !=null, x => x.CreationTime >= filter.FromDate.Value)
                .WhereIf(filter.ToDate !=null, x => x.CreationTime <= filter.ToDate.Value)
                .WhereIf(!string.IsNullOrEmpty(filter.Email), x => EF.Functions.Like(x.Email, $"%{filter.Email}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.ShipCode), x => EF.Functions.Like(x.ShipCode, $"%{filter.ShipCode}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.ShipName), x => EF.Functions.Like(x.ShipName, $"%{filter.ShipName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Airports), x => EF.Functions.Like(x.Airports, $"%{filter.Airports}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Departure), x => EF.Functions.Like(x.Departure, $"%{filter.Departure}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.NeedAccessibleCabin), x => EF.Functions.Like(x.NeedAccessibleCabin, $"%{filter.NeedAccessibleCabin}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Duration), x => EF.Functions.Like(x.Duration, $"%{filter.Duration}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.MobileNo), x => EF.Functions.Like(x.MobileNo, $"%{filter.MobileNo}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.SelectedCabinCount), x => EF.Functions.Like(x.SelectedCabinCount, $"%{filter.SelectedCabinCount}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.SelectedCabin), x => EF.Functions.Like(x.SelectedCabin, $"%{filter.SelectedCabin}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Ref_No), x => EF.Functions.Like(x.Ref_No, $"%{filter.Ref_No}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.CruiseLineCode), x => EF.Functions.Like(x.CruiseLineCode, $"%{filter.CruiseLineCode}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.CruiseLineName), x => EF.Functions.Like(x.CruiseLineName, $"%{filter.CruiseLineName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Ref_No), x => EF.Functions.Like(x.Ref_No, $"%{filter.Ref_No}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.Email, $"%{filter.Query}%")).OrderByDescending(x => x.ID);

                return await Task.FromResult(_mapper.Map<PaginatedList<CruiseEnquiry>, PaginatedList<CruiseEnquiryModal>>(new PaginatedList<CruiseEnquiry>(result.ToList
                    (), filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _cruiseEnquiryRepository.GetAll(deleted: false);
                return await Task.FromResult(_mapper.Map<PaginatedList<CruiseEnquiry>, PaginatedList<CruiseEnquiryModal>>(new PaginatedList<CruiseEnquiry>(result, filter.PageSize, filter.PageNumber)));
            }
        }

        public async Task<CruiseEnquiryModal> GetById(int Id)
        {
            return _mapper.Map<CruiseEnquiry, CruiseEnquiryModal>(await _cruiseEnquiryRepository.Entites().FirstOrDefaultAsync(x => x.ID == Id));

        }
    }
}
