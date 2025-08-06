using AutoMapper;
using Cms.Services.Extensions;
using Cms.Services.Filters.TMM;
using Cms.Services.Interfaces.TMM;
using Cms.Services.Models.TMMModals.AllInOne;
using CMS.Repositories.Interfaces.TMM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Services.Services.TMM
{
    public class AllInOneReportService : IAllInOneReportService
    {
        private readonly IQuotationEmailSupportRepository _quotationEmailSupportRepository;
        private readonly IBlogEnqueryPageDetailsRepository _blogEnqueryPageDetailsRepository;
        private readonly IContactUsRepository _contactUsRepository;
        private readonly ICruiseEnquiryRepository _cruiseEnquiryRepository;
        private readonly ICustomerReviewRatingsRepository _customerReviewRatingsRepository;
        private readonly IEnqueryPageDetailsRepository _enqueryPageDetailsRepository;
        private readonly IFlightsEnquiryRepository _flightEnquiryRepository;
        private readonly IGroupTravelFlightEnqueryDetailsRepository _groupTravelFlightEnqueryDetailsRepository;
        private readonly IPriceTrackingCustomerInfoRepository _priceTrackingCustomerInfoRepository;
        private readonly IVideoConsulationRepository _videoConsulationRepository;
        private readonly IDynamicDestinationEnquiryRepository _dynamicDestinationEnquiryRepository;
        public AllInOneReportService(
            IQuotationEmailSupportRepository quotationEmailSupportRepository,
            IBlogEnqueryPageDetailsRepository blogEnqueryPageDetailsRepository,
            IContactUsRepository contactUsRepository,
            ICruiseEnquiryRepository cruiseEnquiryRepository,
            ICustomerReviewRatingsRepository customerReviewRatingsRepository,
            IEnqueryPageDetailsRepository enqueryPageDetailsRepository,
            IFlightsEnquiryRepository flightEnquiryRepository,
            IGroupTravelFlightEnqueryDetailsRepository groupTravelFlightEnqueryDetailsRepository,
            IPriceTrackingCustomerInfoRepository priceTrackingCustomerInfoRepository,
            IVideoConsulationRepository videoConsulationRepository,
            IDynamicDestinationEnquiryRepository dynamicDestinationEnquiryRepository)
        {
            _quotationEmailSupportRepository = quotationEmailSupportRepository ?? throw new ArgumentNullException(nameof(quotationEmailSupportRepository));
            _blogEnqueryPageDetailsRepository = blogEnqueryPageDetailsRepository ?? throw new ArgumentNullException(nameof(blogEnqueryPageDetailsRepository));
            _contactUsRepository = contactUsRepository ?? throw new ArgumentNullException(nameof(contactUsRepository));
            _cruiseEnquiryRepository = cruiseEnquiryRepository ?? throw new ArgumentNullException(nameof(cruiseEnquiryRepository));
            _customerReviewRatingsRepository = customerReviewRatingsRepository ?? throw new ArgumentNullException(nameof(customerReviewRatingsRepository));
            _enqueryPageDetailsRepository = enqueryPageDetailsRepository ?? throw new ArgumentNullException(nameof(enqueryPageDetailsRepository));
            _flightEnquiryRepository = flightEnquiryRepository ?? throw new ArgumentNullException(nameof(flightEnquiryRepository));
            _groupTravelFlightEnqueryDetailsRepository = groupTravelFlightEnqueryDetailsRepository ?? throw new ArgumentNullException(nameof(groupTravelFlightEnqueryDetailsRepository));
            _priceTrackingCustomerInfoRepository = priceTrackingCustomerInfoRepository ?? throw new ArgumentNullException(nameof(priceTrackingCustomerInfoRepository));
            _videoConsulationRepository = videoConsulationRepository ?? throw new ArgumentNullException(nameof(videoConsulationRepository));
            _dynamicDestinationEnquiryRepository = dynamicDestinationEnquiryRepository;
        }
        public async Task<PaginatedList<GenericTmmReportModel>> GetAllEnquiryReportSearchAsync(GeneralReportsFilter filter)
        {
            var reportModel = new List<GenericTmmReportModel>();
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            try
            {
                var blogEnquery = _blogEnqueryPageDetailsRepository.GetAll(false)
                .WhereIf(!string.IsNullOrEmpty(filter.ReferenceNumber), x => EF.Functions.Like(x.EnquiryRefId, $"%{filter.ReferenceNumber}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.CustomerName), x => EF.Functions.Like(x.CustomerName, $"%{filter.CustomerName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.CustomerEmail), x => EF.Functions.Like(x.CustomerEmail, $"%{filter.CustomerEmail}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.CustomerPhone), x => EF.Functions.Like(x.CustomerPhone, $"%{filter.CustomerPhone}%"));
                var blogEnqueryList = await blogEnquery
                    .Select(x => new GenericTmmReportModel
                    {
                        Id = x.Id,
                        ReferenceNumber = x.EnquiryRefId,
                        ReportName = "Blog Inquiry",
                        CustomerName = x.CustomerName,
                        CustomerEmail = x.CustomerEmail,
                        CustomerPhone = x.CustomerPhone,
                        ServiceName = "Inquiry",
                        ServiceType = "BlogEnqueryPageDetails",
                        CreatedOn= x.CreatedOn,
                    })
                    .ToListAsync();
                var qotationEmailSupport = _quotationEmailSupportRepository.GetAll(false)
                    .WhereIf(filter.PortalId != null && filter.PortalId > 0, x => x.PortalId == filter.PortalId)
                    .WhereIf(!string.IsNullOrEmpty(filter.ReferenceNumber), x => EF.Functions.Like(x.Id.ToString(), $"%{filter.ReferenceNumber}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.CustomerName), x => EF.Functions.Like(x.FirstName, $"%{filter.CustomerName}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.CustomerEmail), x => EF.Functions.Like(x.CustomerEmail, $"%{filter.CustomerEmail}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.CustomerPhone), x => EF.Functions.Like(x.CustomerPhone, $"%{filter.CustomerPhone}%"));
                var quotationEmailSupportList = await qotationEmailSupport
                    .Select(x => new GenericTmmReportModel
                    {
                        Id = x.Id,
                        ReferenceNumber = x.Id.ToString(),
                        ReportName = "Quotation Email Support",
                        CustomerName = x.FirstName,
                        CustomerEmail = x.CustomerEmail,
                        CustomerPhone = x.CustomerPhone,
                        ServiceName = "Inquiry",
                        ServiceType = "QuotationEmailSupport",
                        CreatedOn= x.CreatedOn,
                    })
                    .ToListAsync();
                var contactUs = _contactUsRepository.GetAll(false)
                    .WhereIf(filter.PortalId != null && filter.PortalId > 0, x => x.PortalID == filter.PortalId)
                    .WhereIf(!string.IsNullOrEmpty(filter.ReferenceNumber), x => EF.Functions.Like(x.Ref_No, $"%{filter.ReferenceNumber}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.CustomerName), x => EF.Functions.Like(x.Name, $"%{filter.CustomerName}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.CustomerEmail), x => EF.Functions.Like(x.Email, $"%{filter.CustomerEmail}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.CustomerPhone), x => EF.Functions.Like(x.MobileNo, $"%{filter.CustomerPhone}%"));
                var contactUsList = await contactUs
                    .Select(x => new GenericTmmReportModel
                    {
                        Id = x.ID,
                        ReferenceNumber = x.Ref_No,
                        ReportName = "Contact Us",
                        CustomerName = x.Name,
                        CustomerEmail = x.Email,
                        CustomerPhone = x.MobileNo,
                        ServiceName = "Inquiry",
                        ServiceType = "ContactUs",
                        CreatedOn= x.CreationTime,
                    })
                    .ToListAsync();
                var cruiseEnquiry = _cruiseEnquiryRepository.GetAll(false)
                    .WhereIf(filter.PortalId != null && filter.PortalId > 0, x => x.PortalID == filter.PortalId)
                    .WhereIf(!string.IsNullOrEmpty(filter.ReferenceNumber), x => EF.Functions.Like(x.Ref_No, $"%{filter.ReferenceNumber}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.CustomerName), x => EF.Functions.Like(x.FirstName + " " + x.LastName, $"%{filter.CustomerName}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.CustomerEmail), x => EF.Functions.Like(x.Email, $"%{filter.CustomerEmail}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.CustomerPhone), x => EF.Functions.Like(x.MobileNo, $"%{filter.CustomerPhone}%"));
                var cruiseEnquiryList = await cruiseEnquiry
                    .Select(x => new GenericTmmReportModel
                    {
                        Id = x.ID,
                        ReferenceNumber = x.Ref_No,
                        ReportName = "Cruise Enquiry",
                        CustomerName = x.FirstName + " " + x.LastName,
                        CustomerEmail = x.Email,
                        CustomerPhone = x.MobileNo,
                        ServiceName = "Inquiry",
                        ServiceType = "CruiseEnquiry",
                        CreatedOn= x.CreationTime,
                    })
                    .ToListAsync();
                var customerReviewRatings = _customerReviewRatingsRepository.GetAll(false)
                    .WhereIf(filter.PortalId != null && filter.PortalId > 0, x => x.PortalId == filter.PortalId)
                    .WhereIf(!string.IsNullOrEmpty(filter.ReferenceNumber), x => EF.Functions.Like(x.Id.ToString(), $"{filter.ReferenceNumber}"))
                    .WhereIf(!string.IsNullOrEmpty(filter.CustomerName), x => EF.Functions.Like(x.FullName, $"%{filter.CustomerName}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.CustomerEmail), x => EF.Functions.Like(x.emailId, $"%{filter.CustomerEmail}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.CustomerPhone), x => EF.Functions.Like(x.PhoneNumber, $"%{filter.CustomerPhone}%"));
                var customerReviewRatingsList = await customerReviewRatings
                    .Select(x => new GenericTmmReportModel
                    {
                        Id = (int)x.Id,
                        ReferenceNumber = x.Id.ToString(),
                        ReportName = "Customer Review Ratings",
                        CustomerName = x.FullName,
                        CustomerEmail = x.emailId,
                        CustomerPhone = x.PhoneNumber,
                        ServiceName = "Inquiry",
                        ServiceType = "CustomerReviewRatings",
                        CreatedOn= x.Created_On,
                    })
                    .ToListAsync();
                var enqueryPageDetails = _enqueryPageDetailsRepository.GetAll(false)
                    .WhereIf(!string.IsNullOrEmpty(filter.ReferenceNumber), x => EF.Functions.Like(x.Id.ToString(), $"{filter.ReferenceNumber}"))
                    .WhereIf(!string.IsNullOrEmpty(filter.CustomerName), x => EF.Functions.Like(x.CustomerName, $"%{filter.CustomerName}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.CustomerEmail), x => EF.Functions.Like(x.CustomerEmail, $"%{filter.CustomerEmail}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.CustomerPhone), x => EF.Functions.Like(x.CustomerPhone, $"%{filter.CustomerPhone}%"));
                var enqueryPageDetailsList = await enqueryPageDetails.Select(x => new GenericTmmReportModel
                {
                    Id = x.Id,
                    ReferenceNumber = x.Id.ToString(),
                    ReportName = "Enquiry Page Details",
                    CustomerName = x.CustomerName,
                    CustomerEmail = x.CustomerEmail,
                    CustomerPhone = x.CustomerPhone,
                    ServiceName = "Inquiry",
                    ServiceType = "EnqueryPageDetails",
                    CreatedOn= x.CreatedOn,
                }).ToListAsync();
                var flightEnquiry = _flightEnquiryRepository.GetAll(false)
                    .WhereIf(filter.PortalId != null && filter.PortalId > 0, x => x.PortalID == filter.PortalId)
                    .WhereIf(!string.IsNullOrEmpty(filter.ReferenceNumber), x => EF.Functions.Like(x.Ref_No, $"{filter.ReferenceNumber}"))
                    .WhereIf(!string.IsNullOrEmpty(filter.CustomerName), x => EF.Functions.Like(x.FirstName + " " + x.LastName, $"%{filter.CustomerName}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.CustomerEmail), x => EF.Functions.Like(x.Email, $"%{filter.CustomerEmail}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.CustomerPhone), x => EF.Functions.Like(x.MobileNo, $"%{filter.CustomerPhone}%"));
                var flightEnquiryList = await flightEnquiry.Select(x => new GenericTmmReportModel
                {
                    Id = x.ID,
                    ReferenceNumber = x.Ref_No,
                    ReportName = "Flight Enquiry",
                    CustomerName = x.FirstName + " " + x.LastName,
                    CustomerEmail = x.Email,
                    CustomerPhone = x.MobileNo,
                    ServiceName = "Inquiry",
                    ServiceType = "FlightEnquiry",
                    CreatedOn= x.CreationTime,
                }).ToListAsync();
                var groupTravelFlightEnqueryDetails = _groupTravelFlightEnqueryDetailsRepository.GetAll(false)
                    .WhereIf(filter.PortalId != null && filter.PortalId > 0, x => x.PortalID == filter.PortalId)
                    .WhereIf(!string.IsNullOrEmpty(filter.ReferenceNumber), x => EF.Functions.Like(x.ReferenceId, $"%{filter.ReferenceNumber}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.CustomerName), x => EF.Functions.Like(x.FirstName + " " + x.LastName, $"%{filter.CustomerName}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.CustomerEmail), x => EF.Functions.Like(x.EmailId, $"%{filter.CustomerEmail}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.CustomerPhone), x => EF.Functions.Like(x.ContactNumber, $"%{filter.CustomerPhone}%"));
                var groupTravelFlightEnqueryDetailsList = await groupTravelFlightEnqueryDetails
                    .Select(x => new GenericTmmReportModel
                    {
                        Id = (int)x.Id,
                        ReferenceNumber = x.ReferenceId,
                        ReportName = "Group Travel Flight Enquiry",
                        CustomerName = x.FirstName + " " + x.LastName,
                        CustomerEmail = x.EmailId,
                        CustomerPhone = x.ContactNumber,
                        ServiceName = "Inquiry",
                        ServiceType = "GroupTravelFlightEnqueryDetails",
                        CreatedOn= x.SearchDate,
                    })
                    .ToListAsync();
                var priceTrackingCustomerInfo = _priceTrackingCustomerInfoRepository.GetAll(false)
                    .WhereIf(!string.IsNullOrEmpty(filter.ReferenceNumber), x => EF.Functions.Like(x.Id.ToString(), $"{filter.ReferenceNumber}"))
                    .WhereIf(!string.IsNullOrEmpty(filter.CustomerName), x => EF.Functions.Like(x.CustomerEmail, $"%{filter.CustomerName}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.CustomerEmail), x => EF.Functions.Like(x.CustomerEmail, $"%{filter.CustomerEmail}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.CustomerPhone), x => EF.Functions.Like(x.CustomerPhone, $"%{filter.CustomerPhone}%"));
                var priceTrackingCustomerInfoList = await priceTrackingCustomerInfo
                    .Select(x => new GenericTmmReportModel
                    {
                        Id = x.Id,
                        ReferenceNumber = x.Id.ToString(),
                        ReportName = "Price Tracking Customer Info",
                        CustomerName = x.CustomerEmail,
                        CustomerEmail = x.CustomerEmail,
                        CustomerPhone = x.CustomerPhone,
                        ServiceName = "Inquiry",
                        ServiceType = "PriceTrackingCustomerInfo",
                        CreatedOn = x.CreatedOn,
                    })
                    .ToListAsync();
                var videoConsulation = _videoConsulationRepository.GetAll(false)
                    .WhereIf(filter.PortalId != null && filter.PortalId > 0, x => x.PortalId == filter.PortalId)
                    .WhereIf(!string.IsNullOrEmpty(filter.ReferenceNumber), x => EF.Functions.Like(x.ID.ToString(), $"{filter.ReferenceNumber}"))
                    .WhereIf(!string.IsNullOrEmpty(filter.CustomerName), x => EF.Functions.Like(x.Name, $"%{filter.CustomerName}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.CustomerPhone), x => EF.Functions.Like(x.MobileNo, $"%{filter.CustomerPhone}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.CustomerEmail), x => EF.Functions.Like(x.Email, $"%{filter.CustomerEmail}%"));
                var videoConsulationList = await videoConsulation
                    .Select(x => new GenericTmmReportModel
                    {
                        Id = x.ID,
                        ReferenceNumber = x.ID.ToString(),
                        ReportName = "Video Consultation",
                        CustomerName = x.Name,
                        CustomerEmail = x.Email,
                        CustomerPhone = x.MobileNo,
                        ServiceName = "Inquiry",
                        ServiceType = "VideoConsulation",
                        CreatedOn=x.Created_On,
                    })
                    .ToListAsync();
                var dynamicDestinationEnquiry = _dynamicDestinationEnquiryRepository.GetAll(false)
                    .WhereIf(filter.PortalId != null && filter.PortalId > 0, x => x.PortalId == filter.PortalId)
                    .WhereIf(!string.IsNullOrEmpty(filter.ReferenceNumber), x => EF.Functions.Like(x.RefrenceId, $"%{filter.ReferenceNumber}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.CustomerName), x => EF.Functions.Like(x.FirstName + " " + x.LastName, $"%{filter.CustomerName}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.CustomerEmail), x => EF.Functions.Like(x.Email, $"%{filter.CustomerEmail}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.CustomerPhone), x => EF.Functions.Like(x.PhoneNo, $"%{filter.CustomerPhone}%"));
                var dynamicDestinationEnquiryList = await dynamicDestinationEnquiry
                    .Select(x => new GenericTmmReportModel
                    {
                        Id = x.Id,
                        ReferenceNumber = x.RefrenceId,
                        ReportName = "Dynamic Destination Enquiry",
                        CustomerName = x.FirstName + " " + x.LastName,
                        CustomerEmail = x.Email,
                        CustomerPhone = x.PhoneNo,
                        ServiceName = "Inquiry",
                        ServiceType = "DynamicDestinationEnquiry",
                        CreatedOn=x.CreatedOn,
                    })
                    .ToListAsync();
                reportModel.AddRange(blogEnqueryList);
                reportModel.AddRange(quotationEmailSupportList);
                reportModel.AddRange(contactUsList);
                reportModel.AddRange(cruiseEnquiryList);
                reportModel.AddRange(customerReviewRatingsList);
                reportModel.AddRange(enqueryPageDetailsList);
                reportModel.AddRange(flightEnquiryList);
                reportModel.AddRange(groupTravelFlightEnqueryDetailsList);
                reportModel.AddRange(priceTrackingCustomerInfoList);
                reportModel.AddRange(videoConsulationList);
                reportModel.AddRange(dynamicDestinationEnquiryList);
                reportModel = reportModel.OrderByDescending(x => x.CreatedOn).ToList();

                return new PaginatedList<GenericTmmReportModel>(reportModel, filter.PageSize, filter.PageNumber);
            }
            catch (Exception ex)
            {
                // Log the exception here (e.g., using ILogger)
                throw new Exception("An error occurred while fetching the report data.", ex);
            }
            finally
            {
                // Any cleanup code if necessary
            }
        }
    }
}
