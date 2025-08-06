using Cms.Services.Filters;
using Cms.Services.Interfaces;
using Cms.Services.Services;
using CMS.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Cms.Services.Extensions
{
    public static class DependancyRegistrar
    {
        public static void RegisterServiceDependancy(this IServiceCollection services)
        {
            // TMM depandancies
            services.AddScoped<Interfaces.TMM.IDynamicDestinationEnquiryService, Services.TMM.DynamicDestinationEnquiryService>();
            services.AddScoped<Interfaces.TMM.IQuotationEmailSupportService, Services.TMM.QuotationEmailSupportService>();
            services.AddScoped<Interfaces.TMM.IGroupTravelFlightEnqueryDetailsService, Services.TMM.GroupTravelFlightEnqueryDetailsService>();
            services.AddScoped<Interfaces.TMM.IUsersService, Services.TMM.UsersService>();
            services.AddScoped<Interfaces.TMM.IContactUsService, Services.TMM.ContactUsService>();
            services.AddScoped<Interfaces.TMM.ISubscribeService, Services.TMM.SubscribeService>();
            services.AddScoped<Interfaces.TMM.IEnqueryPageDetailsService, Services.TMM.EnqueryPageDetailsService>();
            services.AddScoped<Interfaces.TMM.IFlightSearchDetailsService, Services.TMM.FlightSearchDetailsService>();
            services.AddScoped<Interfaces.TMM.IHotelSearchDetailsService, Services.TMM.HotelSearchDetailsService>();
            services.AddScoped<Interfaces.TMM.ICruiseSearchDetailsService, Services.TMM.CruiseSearchDetailsService>();
            services.AddScoped<Interfaces.TMM.ICruiseEnquiryService, Services.TMM.CruiseEnquiryService>();
            services.AddScoped<Interfaces.TMM.IBlogEnqueryPageDetailsService, Services.TMM.BlogEnqueryPageDetailsService>();
            services.AddScoped<Interfaces.TMM.IPriceTrackingCustomerInfoService, Services.TMM.PriceTrackingCustomerInfoService>();
            services.AddScoped<Interfaces.TMM.IVideoConsulationService, Services.TMM.VideoConsulationService>();
            services.AddScoped<Interfaces.TMM.IFlightsEnquiryService, Services.TMM.FlightsEnquiryService>();
            services.AddScoped<Interfaces.TMM.IBookingTransactionDetailsService, Services.TMM.BookingTransactionDetailsService>();
            services.AddScoped<Interfaces.TMM.IBookingJourneyDetailsService, Services.TMM.BookingJourneyDetailsService>();
            services.AddScoped<Interfaces.TMM.IBookingPaxDetailsService, Services.TMM.BookingPaxDetailsService>();
            services.AddScoped<Interfaces.TMM.ICruiseBookingTransactionDetailsService, Services.TMM.CruiseBookingTransactionDetailsService>();
            services.AddScoped<Interfaces.TMM.ICustomerReviewRatingsService, Services.TMM.CustomerReviewRatingsService>();
            services.AddScoped<Interfaces.TMM.IAllInOneReportService, Services.TMM.AllInOneReportService>();
            
            // Activity Search Service Dependancies
            services.AddScoped<Interfaces.ActivityAdmin.IActivitySearchLogsService, Services.ActivityAdmin.ActivitySearchLogsService>();
            services.AddScoped<Interfaces.ActivityAdmin.IActivityBookingDetailsService, Services.ActivityAdmin.ActivityBookingDetailsService>();
            // Hotel Booking Service Dependancies
            services.AddScoped<Interfaces.HotelAdmin.IHotelBookingDetailsService, Services.HotelAdmin.HotelBookingDetailsService>();
             // Car Booking Service Dependancies
            services.AddScoped<Interfaces.TransferAdmin.ICarBookingTransactionDetailsService, Services.TransferAdmin.CarBookingTransactionDetailsService>();

            // CMS depandacies
            services.AddScoped<IDataCachingService, DataCachingService>();
            services.AddScoped<ITemplateMasterService, TemplateMasterService>();
            services.AddScoped<ITemplateConfigurationService, TemplateConfigurationService>();
            services.AddScoped<ITemplateDetailsService, TemplateDetailsService>();
            services.AddScoped<ISectionService, SectionService>();
            services.AddScoped<ISectionContentService, SectionContentService>();
            services.AddScoped<ISectionContentAuditTrailsService, SectionContentAuditTrailsService>();
            services.AddScoped<IUserRoleService, UserRoleService>();         
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IPortalService, PortalService>();
            services.AddScoped<ITemplateCategoryService, TemplateCategoryService>();
            services.AddScoped<ITemplateDetailsDataService, TemplateDetailsDataService>();
            services.AddScoped<ISectionTypeService, SectionTypeService>();
            services.AddScoped<IFlightDealManagementService, FlightDealManagementService>();
            services.AddScoped<IMasterAirlinesService, MasterAirlinesService>();
            services.AddScoped<IAirportDetailsService, AirportDetailsService>();
            services.AddScoped<IFlightDealManagementDataService, FlightDealManagementDataService>();
            services.AddScoped<IHotelDealsService, HotelDealsService>();
            services.AddScoped<ICityCountryService, CityCountryService>();
            services.AddScoped<IHolidayPackagesService, HolidayPackagesService>();
            services.AddScoped<IHolidayPackagesDataService, HolidayPackagesDataService>();
            services.AddScoped<IHotelDealsDataService, HotelDealsDataService>();
            services.AddScoped<IPackageItenariesService, PackageItenariesService>();
            services.AddScoped<IFlightFaresDetailsService, FlightFaresDetailsService>();
            services.AddScoped<IFlightFaresDetailsDataService, FlightFaresDetailsDataService>();
            services.AddScoped<IMenuMasterService, MenuMasterService>();
            services.AddScoped<IUserSearchLogsService, UserSearchLogsService>();
            services.AddScoped<IUserRoleMenuPermissionService, UserRoleMenuPermissionService>();
            services.AddScoped<IDummyVacationPackageMasterService, DummyVacationPackageMasterService>();
            services.AddScoped<ICouponMasterService, CouponMasterService>();
            services.AddScoped<ICouponMasterDataService, CouponMasterDataService>();
            services.AddScoped<ICarHireDealsService, CarHireDealsService>();
            services.AddScoped<ICarHireDealsDataServices, CarHireDealsDataServices>();
                               
        }
        public static bool startJobWorker { get; set; }
        public static bool UseCacheForTemplateDetails{ get; set; }
        public static bool UseCacheForHolidayPackages { get; set; }
    }
}
