using CMS.Repositories.Interfaces;
using CMS.Repositories.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CMS.Repositories.Extensions
{
    public static class DependancyRegistrar
    {
        public static void RegisterDependancy(this IServiceCollection services)
        {
            //TMM
            services.AddScoped<Interfaces.TMM.IDynamicDestinationEnquiryRepository, Repositories.TMM.DynamicDestinationEnquiryRepository>();
            services.AddScoped<Interfaces.TMM.IGroupTravelFlightEnqueryDetailsRepository, Repositories.TMM.GroupTravelFlightEnqueryDetailsRepository>();
            services.AddScoped<Interfaces.TMM.IEnqueryPageDetailsRepository, Repositories.TMM.EnqueryPageDetailsRepository>();
            services.AddScoped<Interfaces.TMM.IFlightSearchDetailsRepository, Repositories.TMM.FlightSearchDetailsRepository>();
            services.AddScoped<Interfaces.TMM.IHotelSearchDetailsRepository, Repositories.TMM.HotelSearchDetailsRepository>();
            services.AddScoped<Interfaces.TMM.ICruiseSearchDetailsRepository, Repositories.TMM.CruiseSearchDetailsRepository>();
            services.AddScoped<Interfaces.TMM.IUsersRepository, Repositories.TMM.UsersRepository>();
            services.AddScoped<Interfaces.TMM.IContactUsRepository, Repositories.TMM.ContactUsRepository>();
            services.AddScoped<Interfaces.TMM.ISubscribeRepository, Repositories.TMM.SubscribeRepository>();
            services.AddScoped<Interfaces.TMM.ICruiseEnquiryRepository, Repositories.TMM.CruiseEnquiryRepository>();
            services.AddScoped<Interfaces.TMM.IPriceTrackingCustomerInfoRepository, Repositories.TMM.PriceTrackingCustomerInfoRepository>();
            services.AddScoped<Interfaces.TMM.IBlogEnqueryPageDetailsRepository, Repositories.TMM.BlogEnqueryPageDetailsRepository>();
            services.AddScoped<Interfaces.TMM.IVideoConsulationRepository, Repositories.TMM.VideoConsulationRepository>();
            services.AddScoped<Interfaces.TMM.IFlightsEnquiryRepository, Repositories.TMM.FlightsEnquiryRepository>();
            services.AddScoped<Interfaces.TMM.IBookingJourneyDetailsRepository, Repositories.TMM.BookingJourneyDetailsRepository>();
            services.AddScoped<Interfaces.TMM.IBookingPaxDetailsRepository, Repositories.TMM.BookingPaxDetailsRepository>();
            services.AddScoped<Interfaces.TMM.IBookingTransactionDetailsRepository, Repositories.TMM.BookingTransactionDetailsRepository>();
            services.AddScoped<Interfaces.TMM.ICruiseBookingTransactionDetailsRepository, Repositories.TMM.CruiseBookingTransactionDetailsRepository>();
            services.AddScoped<Interfaces.TMM.ICustomerReviewRatingsRepository, Repositories.TMM.CustomerReviewRatingsRepository>();
            services.AddScoped<Interfaces.TMM.IQuotationEmailSupportRepository, Repositories.TMM.QuotationEmailSupportRepository>();
            services.AddScoped(typeof(Interfaces.TMM.IRepository<>), typeof(Repositories.TMM.Repository<>));
            // HotelAdmin
            services.AddScoped(typeof(Interfaces.HotelAdmin.IRepository<>), typeof(Repositories.HotelAdmin.Repository<>));
            services.AddScoped<Interfaces.HotelAdmin.IHotelBookingDetailsRepository, Repositories.HotelAdmin.HotelBookingDetailsRepository>();
            // TransferAdmin
            services.AddScoped(typeof(Interfaces.TransferAdmin.IRepository<>), typeof(Repositories.TransferAdmin.Repository<>));
            services.AddScoped<Interfaces.TransferAdmin.ICarBookingTransactionDetailsRepository, Repositories.TransferAdmin.CarBookingTransactionDetailsRepository>();
          // ActivityAdmin
            services.AddScoped(typeof(Interfaces.ActivityAdmin.IRepository<>), typeof(Repositories.ActivityAdmin.Repository<>));
            services.AddScoped<Interfaces.ActivityAdmin.IActivitySearchLogsRepository, Repositories.ActivityAdmin.ActivitySearchLogsRepository>();
            services.AddScoped<Interfaces.ActivityAdmin.IActivityBookingDetailsRepository, Repositories.ActivityAdmin.ActivityBookingDetailsRepository>();
            //CMS
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ITemplateMasterRepository, TemplateMasterRepository>();
            services.AddScoped<ICouponMasterRepository, CouponMasterRepository>();
            services.AddScoped<ITemplateConfigurationRepository, TemplateConfigurationRepository>();
            services.AddScoped<ITemplateDetailsRepository, TemplateDetailsRepository>();
            services.AddScoped<ISectionRepository, SectionRepository>();
            services.AddScoped<ISectionContentRepository, SectionContentRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IPortalRepository, PortalRepository>();
            services.AddScoped<ITemplateCategoryRepository, TemplateCategoryRepository>();
            services.AddScoped<ISectionTypeRepository, SectionTypeRepository>();
            services.AddScoped<IFlightDealManagementRepository, FlightDealManagementRepository>();
            services.AddScoped<IMasterAirlinesRepository, MasterAirlinesRepository>();
            services.AddScoped<IAirportDetailsRepository, AirportDetailsRepository>();
            services.AddScoped<IHotelDealsRepository, HotelDealsRepository>();
            services.AddScoped<ICityCountryRepository, CityCountryRepository>();
            services.AddScoped<IHolidayPackagesRepository, HolidayPackagesRepository>();
            services.AddScoped<IPackageItenariesRepository, PackageItenariesRepository>();
            services.AddScoped<IFlightFaresDetailsRepository, FlightFaresDetailsRepository>();
            services.AddScoped<IMenuMasterRepository, MenuMasterRepository>();
            services.AddScoped<IUserSearchLogsRepository, UserSearchLogsRepository>();
            services.AddScoped<IUserRoleMenuPermissionRepository, UserRoleMenuPermissionRepository>();
            services.AddScoped<IDummyVacationPackageMasterRepository, DummyVacationPackageMasterRepository>();
            services.AddScoped<ICarHireDealsRepository, CarHireDealsRepository>();
            
           
        }
    }
}
