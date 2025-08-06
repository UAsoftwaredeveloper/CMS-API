using AutoMapper;
using Cms.Services.Extensions;
using Cms.Services.Filters;
using Cms.Services.Interfaces;
using Cms.Services.Models.FlightFareResponse;
using Cms.Services.Models.FlightFaresDetails;
using Cms.Services.Models.OpenAPIDataModel.FlightFaresDetails;
using CMS.Repositories.Interfaces;
using CMS.Repositories.Interfaces.ActivityAdmin;
using CMS.Repositories.Repositories;
using DataManager.DataClasses;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Services.Services
{
    public class FlightFaresDetailsService : IFlightFaresDetailsService
    {
        public IFlightFaresDetailsRepository _sectionRepository;
        public IMapper _mapper;

        public FlightFaresDetailsService(IFlightFaresDetailsRepository usersRepository, IMapper mapper)
        {
            _sectionRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<FlightFaresDetailsModal> CreateFlightFaresDetails(FlightFaresDetailsModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {

                var result = await _sectionRepository.Insert(_mapper.Map<FlightFaresDetailsModal, FlightFaresDetails>(modal));
                return _mapper.Map<FlightFaresDetails, FlightFaresDetailsModal>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<FlightFaresDetailsModal>> CreateFlightFaresDetailsList(List<FlightFaresDetailsModal> modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {

                var result = await _sectionRepository.InsertRange(_mapper.Map<List<FlightFaresDetailsModal>, List<FlightFaresDetails>>(modal));
                return _mapper.Map<List<FlightFaresDetails>, List<FlightFaresDetailsModal>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<FlightFaresDetailsModal> UpdateFlightFaresDetails(FlightFaresDetailsModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {

                var result = await _sectionRepository.Update(_mapper.Map<FlightFaresDetailsModal, FlightFaresDetails>(modal));
                return _mapper.Map<FlightFaresDetails, FlightFaresDetailsModal>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<FlightFaresDetailsModal>> UpdateFlightFaresDetailsList(List<FlightFaresDetailsModal> modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {

                var result = await _sectionRepository.UpdateRange(_mapper.Map<List<FlightFaresDetailsModal>, List<FlightFaresDetails>>(modal));
                return _mapper.Map<List<FlightFaresDetails>, List<FlightFaresDetailsModal>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<PaginatedList<FlightFaresDetailsModal>> GetAllFlightFaresDetails(FlightFaresDetailsFilter filter)
        {
            if (filter != null)
            {
                var result = _sectionRepository.GetAll(deleted: false).WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                    .WhereIf(!string.IsNullOrEmpty(filter.ArrCityName), x => x.ArrCityName != null && x.ArrCityName.ToLower() == filter.ArrCityName.ToLower())
                    .WhereIf(!string.IsNullOrEmpty(filter.DepCityName), x => x.ArrCityName != null && x.DepCityName.ToLower() == filter.DepCityName.ToLower())
                    ;
                return await Task.FromResult(_mapper.Map<PaginatedList<FlightFaresDetails>, PaginatedList<FlightFaresDetailsModal>>(new PaginatedList<FlightFaresDetails>(result, filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _sectionRepository.GetAll(deleted: false);
                return await Task.FromResult(_mapper.Map<PaginatedList<FlightFaresDetails>, PaginatedList<FlightFaresDetailsModal>>(new PaginatedList<FlightFaresDetails>(result, filter.PageSize, filter.PageNumber)));
            }
        }
        public async Task<List<FlightFaresDetailsData>> GetAllFlightFaresDetailsData(FlightFaresDetailsFilter filter)
        {
            if (filter != null)
            {
                var result = _sectionRepository.GetAll(deleted: false).WhereIf(filter.Id > 0, x => x.Id == filter.Id).ToList();
                var distinctResult = result.GroupBy(x => new { x.TotalPrice, x.DepAirpCode, x.ArrAirpCode })
                                    .Select(g => g.First())
                                    .ToList();
                return await Task.FromResult(_mapper.Map<List<FlightFaresDetails>, List<FlightFaresDetailsData>>(distinctResult));
            }
            else
            {
                var result =
                _sectionRepository.GetAll(deleted: false);
                return await Task.FromResult(_mapper.Map<List<FlightFaresDetails>, List<FlightFaresDetailsData>>(result.ToList()));
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
        public async Task<FlightFaresDetailsModal> GetById(int Id)
        {
            return _mapper.Map<FlightFaresDetails, FlightFaresDetailsModal>(await _sectionRepository.Get(Id).Result.FirstOrDefaultAsync());

        }
    }
    public class FlightFaresDetailsDataService : IFlightFaresDetailsDataService
    {
        public IFlightFaresDetailsRepository _flightFaresDetailsRepository;
        private IAirportDetailsRepository _airportDetailsRepository;
        public FlightFaresDetailsDataService(IFlightFaresDetailsRepository usersRepository, IMapper mapper, IAirportDetailsRepository airportDetailsRepository)
        {
            _flightFaresDetailsRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _airportDetailsRepository = airportDetailsRepository ?? throw new ArgumentNullException(nameof(_airportDetailsRepository));
        }

        public async Task<List<Itinerary>> GetAllFlightFaresDetailsData(FlightSearchDetails airSearchQuery)
        {
            try
            {
                bool isRoundTripRequest = airSearchQuery.Segments.Count > 1;

                var seg1 = airSearchQuery.Segments.FirstOrDefault();
                var seg2 = isRoundTripRequest ? airSearchQuery.Segments.LastOrDefault() : null;

                // Extract CityCodes
                var seg1DepCityCode = GetCityCode(seg1.Origin);
                var seg1ArrCityCode = GetCityCode(seg1.Destination);
                var seg2DepCityCode = seg2 != null ? GetCityCode(seg2.Origin) : string.Empty;
                var seg2ArrCityCode = seg2 != null ? GetCityCode(seg2.Destination) : string.Empty;
                CultureInfo provider = new CultureInfo("en-US");
                var oneSideDate = !string.IsNullOrEmpty(seg1.Date.Trim()) ? DateTime.ParseExact(seg1.Date, "dd-MM-yyyy", provider).ToString("yyyy-MM-dd") : string.Empty;
                var returnSideDate = isRoundTripRequest && !string.IsNullOrEmpty(seg2.Date.Trim()) ? DateTime.ParseExact(seg2.Date, "dd-MM-yyyy", provider).ToString("yyyy-MM-dd") : string.Empty;
                // Filter the fares based on departure/arrival city codes and dates
                var oneSideFlightFares = await _flightFaresDetailsRepository.GetAll(deleted: false)
                    .Where(x => x.Active == true
                                && (x.DepAirpCode == seg1.Origin || x.DepCityCode == seg1DepCityCode)
                                && (x.ArrAirpCode == seg1.Destination || x.ArrCityCode == seg1ArrCityCode)
                                && x.DepDate == oneSideDate && x.CabinClassCode == airSearchQuery.Cabin.Class)
                    .GroupBy(x => new
                    {
                        x.DepDate,
                        x.ArrDate,
                        x.EquipType,
                        x.ArrAirpCode,
                        x.ArrAirpName
        ,
                        x.ArrCityCode,
                        x.ArrCountryCode,
                        x.TotalPrice,
                        x.markUp,
                        x.BaseFare,
                        x.DepAirpCode,
                        x.DepCityCode,
                        x.DepCountryCode,
                        x.FltNum,
                        x.ValCarrierCode,
                        x.CabinClassCode,
                        x.Class
                    }).Select(g => g.FirstOrDefault()).ToListAsync();

                List<Itinerary> itineraries = new List<Itinerary>();


                var returnFlightFares = isRoundTripRequest? await _flightFaresDetailsRepository.GetAll(deleted: false)

                    .Where(x => x.Active == true
                                && (x.DepAirpCode == seg2.Origin || x.DepCityCode == seg2DepCityCode)
                                && (x.ArrAirpCode == seg2.Destination || x.ArrCityCode == seg2ArrCityCode)
                                && (x.DepDate == returnSideDate || x.CabinClassCode == airSearchQuery.Cabin.Class))
                    .GroupBy(x => new
                    {
                        x.DepDate,
                        x.ArrDate,
                        x.EquipType,
                        x.ArrAirpCode,
                        x.ArrAirpName
    ,
                        x.ArrCityCode,
                        x.ArrCountryCode,
                        x.TotalPrice,
                        x.markUp,
                        x.BaseFare,
                        x.DepAirpCode,
                        x.DepCityCode,
                        x.DepCountryCode,
                        x.FltNum,
                        x.ValCarrierCode,
                        x.CabinClassCode,
                        x.Class
                    }).Select(g => g.FirstOrDefault()).ToListAsync():new List<FlightFaresDetails>();
                int index = 0;
                foreach (var oneSideFare in oneSideFlightFares)
                {
                    index++;
                    decimal baseFare = 0.0M;
                    decimal baseTax = 0.0M;
                    decimal baseMarkup = 0.0M;
                    int noAdult = airSearchQuery.PaxDetail.NoAdult;
                    int noChild = airSearchQuery.PaxDetail.NoChild;
                    int noInfant = airSearchQuery.PaxDetail.NoInfant;
                    int noInfantOfSeat = airSearchQuery.PaxDetail.NoInfantOfSeat;
                    decimal grossBase = 0.0M;
                    decimal grossTax = 0.0M;
                    decimal grossMarkup = 0.0M;
                    decimal exactTotal = 0.0M;
                    decimal grandTotal = 0.0M;
                    int totalPaxes = (noAdult + noChild + noInfant + noInfantOfSeat);
                    #region outboundpax fare breakup
                    if (oneSideFare.BaseFare != null)
                    {
                        baseFare += oneSideFare.BaseFare.Value;
                    }
                    if (oneSideFare.Taxes != null)
                    {
                        baseTax += oneSideFare.Taxes.Value;
                    }
                    if (oneSideFare.markUp != null)
                    {
                        baseMarkup += oneSideFare.markUp.Value;
                    }
                    #endregion
                    if (returnFlightFares.Any())
                    {

                        foreach (var returnFare in returnFlightFares)
                        {

                            #region inboundpax fare breakup
                            if (returnFare.BaseFare != null)
                            {
                                baseFare += returnFare.BaseFare.Value;
                            }
                            if (returnFare.Taxes != null)
                            {
                                baseTax += returnFare.Taxes.Value;
                            }
                            if (returnFare.markUp != null)
                            {
                                baseMarkup += returnFare.markUp.Value;
                            }
                            #endregion
                            #region total fare breakup calculation                            
                            grossBase = baseFare * totalPaxes;
                            grossTax = baseTax * totalPaxes;
                            grossMarkup = baseMarkup * totalPaxes;
                            exactTotal = baseFare + baseMarkup;
                            grandTotal = grossBase + grossTax + grossMarkup;
                            #endregion

                            var sectors = new List<Sector>
                            {
                                CreateSector(oneSideFare), // One side
                                CreateSector(returnFare, true) // Return
                            };

                            itineraries.Add(new Itinerary
                            {
                                Sectors = new Sectors
                                {
                                    Sector = sectors
                                }
                                ,
                                Adult = new Adult
                                {
                                    NoAdult = noAdult,
                                    AdTax = noAdult < 1 ? null : baseTax.ToString(),
                                    AdtBFare = noAdult < 1 ? null : baseFare.ToString(),
                                    Commission = noAdult < 1 ? null : "0.00",
                                    markUp = noAdult < 1 ? null : baseMarkup.ToString(),
                                },
                                Child = new Child
                                {
                                    NoChild = noChild,
                                    CHTax = noChild < 1 ? null : baseTax.ToString(),
                                    ChdBFare = noChild < 1 ? null : baseFare.ToString(),
                                    Commission = noChild < 1 ? null : "0.00",
                                    markUp = noChild < 1 ? null : baseMarkup.ToString(),
                                },
                                Infant = new Infant
                                {
                                    NoInfant = noInfant,
                                    InTax = noInfant < 1 ? null : baseTax.ToString(),
                                    InfBFare = noInfant < 1 ? null : baseFare.ToString(),
                                    Commission = "0.00",
                                    markUp = noInfant < 1 ? null : baseMarkup.ToString(),
                                },
                                InfantOfSeat = new InfantOfSeat
                                {
                                    NoInfantOfSeat = noInfantOfSeat,
                                    InsTax = noInfantOfSeat < 1 ? null : baseTax.ToString(),
                                    InsBFare = noInfantOfSeat < 1 ? null : baseFare.ToString(),
                                    Commission = noInfantOfSeat < 1 ? null : "0.00",
                                    markUp = noInfantOfSeat < 1 ? null : baseMarkup.ToString(),
                                },
                                markUp = grossTax.ToString(),
                                BaseFare = grossBase.ToString(),
                                ExactTotalPrice = exactTotal.ToString(),
                                GrandTotal = grandTotal,
                                Currency = new Currency
                                {
                                    Client = airSearchQuery.Master.CoustmerType,
                                    ClientCurrency = "USD",
                                    ROE = "1",
                                    Supplier = oneSideFare.Provider,
                                    text = "USD",

                                },
                                Provider = oneSideFare.Provider,
                                InBound = 1,
                                OutBound = 1,
                                Commission = "0.00",
                                FareType = oneSideFare.FareType,
                                Taxes = grossTax.ToString(),
                                TotalPrice = exactTotal.ToString(),
                                ValCarrier = oneSideFare.ValCarrierCode,
                                IndexNumber = index,
                                IsAltDate = "false",
                                IsNearBy = "true",
                                GroupPriority = "0",
                                Safi = "0",
                                LastTicketingDate = "",
                                Key = oneSideFare.Key + " | " + returnFare.Key,

                                // Add other properties to Itinerary as needed
                            });
                        }
                    }
                    else
                    {
                        #region total fare breakup calculation                            
                        grossBase = baseFare * totalPaxes;
                        grossTax = baseTax * totalPaxes;
                        grossMarkup = baseMarkup * totalPaxes;
                        exactTotal = baseFare + baseMarkup;
                        grandTotal = grossBase + grossTax + grossMarkup;
                        #endregion

                        // If no return flights, create itinerary with only the one-side flight
                        var sectors = new List<Sector>
                        {
                            CreateSector(oneSideFare)
                        };

                        itineraries.Add(new Itinerary
                        {
                            Sectors = new Sectors
                            {
                                Sector = sectors
                            }
                            ,
                            Adult = new Adult
                            {
                                NoAdult = noAdult,
                                AdTax = noAdult < 1 ? null : baseTax.ToString(),
                                AdtBFare = noAdult < 1 ? null : baseFare.ToString(),
                                Commission = noAdult < 1 ? null : "0.00",
                                markUp = noAdult < 1 ? null : baseMarkup.ToString(),
                            },
                            Child = new Child
                            {
                                NoChild = noAdult,
                                CHTax = noAdult < 1 ? null : baseTax.ToString(),
                                ChdBFare = noAdult < 1 ? null : baseFare.ToString(),
                                Commission = noAdult < 1 ? null : "0.00",
                                markUp = noAdult < 1 ? null : baseMarkup.ToString(),
                            },
                            Infant = new Infant
                            {
                                NoInfant = noInfant,
                                InTax = noInfant < 1 ? null : baseTax.ToString(),
                                InfBFare = noInfant < 1 ? null : baseFare.ToString(),
                                Commission = noInfant < 1 ? null : "0.00",
                                markUp = noInfant < 1 ? null : baseMarkup.ToString(),
                            },
                            InfantOfSeat = new InfantOfSeat
                            {
                                NoInfantOfSeat = noInfantOfSeat,
                                InsTax = noInfant < 1 ? null : baseTax.ToString(),
                                InsBFare = noInfant < 1 ? null : baseFare.ToString(),
                                Commission = "0.00",
                                markUp = noInfant < 1 ? null : baseMarkup.ToString(),
                            },
                            markUp = grossTax.ToString(),
                            BaseFare = grossBase.ToString(),
                            ExactTotalPrice = exactTotal.ToString(),
                            GrandTotal = grandTotal,
                            Currency = new Currency
                            {
                                Client = airSearchQuery.Master.CoustmerType,
                                ClientCurrency = "USD",
                                ROE = "1",
                                Supplier = oneSideFare.Provider,
                                text = "USD",

                            },
                            Provider = oneSideFare.Provider,
                            InBound = 1,
                            OutBound = 1,
                            Commission = "0.00",
                            FareType = oneSideFare.FareType,
                            Taxes = grossTax.ToString(),
                            TotalPrice = exactTotal.ToString(),
                            ValCarrier = oneSideFare.ValCarrierCode,
                            IndexNumber = index,
                            IsAltDate = "false",
                            IsNearBy = "true",
                            GroupPriority = "0",
                            Safi = "0",
                            LastTicketingDate = "",
                            Key = oneSideFare.Key,

                            // Add other properties to Itinerary as needed
                        });
                    }
                }


                return itineraries;
            }
            catch (Exception ex)
            {
                throw;
            }
            // Retrieve all flight fare details from the repository
            // Filter the data based on search details

        }

        public async Task<bool> SoftDelete(int Id)
        {
            var result = await _flightFaresDetailsRepository.Delete(Id);
            if ((bool)result.Deleted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private string GetCityCode(string airportCode)
        {
            return _airportDetailsRepository.GetAll(false)
                .FirstOrDefault(x => x.AirportCode == airportCode)?.CityCode;
        }
        private Sector CreateSector(FlightFaresDetails item, bool isReturn = false)
        {
            CultureInfo provider = new CultureInfo("en-US");
            var depDate = !string.IsNullOrEmpty(item.DepDate.Trim()) ? DateTime.ParseExact(item.DepDate, "yyyy-MM-dd", provider).ToString("dd-MM-yyyy") : string.Empty;
            var arrDate = !string.IsNullOrEmpty(item.ArrDate.Trim()) ? DateTime.ParseExact(item.ArrDate, "yyyy-MM-dd", provider).ToString("dd-MM-yyyy") : string.Empty;
            return new Sector
            {
                ActualTime = item.ActualTime,
                AirlineName = item.ValCarrierName,
                Arrival = new Arrival
                {
                    AirpCode = item.ArrAirpCode,
                    AirpName = item.ArrAirpName,
                    CityName = item.ArrCityName,
                    CountryCode = item.ArrCountryCode,
                    CountryName = item.ArrCountryName,
                    Date = arrDate,
                    StateName = item.ArrStateName,
                    Terminal = item.ArrTerminal,
                    Time = item.ArrTime,
                },
                AirV = item.ValCarrierCode,
                BaggageInfo = item.BaggageInfo,
                CabinClass = new CabinClassWp
                {
                    Code = item.CabinClassCode,
                    Des = item.CabinClassName
                },
                EquipType = item.EquipType,
                ElapsedTime = item.ElapsedTime,
                FltNum = item.FltNum?.ToString(),
                Class = item.Class,
                isConnect = "false",
                Departure = new Departure
                {
                    AirpCode = item.DepAirpCode,
                    AirpName = item.DepAirpName,
                    CityName = item.DepCityName,
                    CountryCode = item.DepCountryCode,
                    CountryName = item.DepCountryName,
                    Date = depDate,
                    StateName = item.DepStateName,
                    Terminal = item.DepTerminal,
                    Time = item.DepTime,
                },
                MrktCarrierDes = "",
                NoSeats = item.NoSeats?.ToString(),
                OptrCarrier = item.ValCarrierCode,
                OptrCarrierDes = item.ValCarrierName,
                SegmentIndex = "0",
                TransitTime = new TransitTime
                {
                    time = item.ElapsedTime,
                },
                Key = item.Key,
                isReturn = isReturn.ToString().ToLower() // Set isReturn based on the parameter value
            };
        }
    }
}
