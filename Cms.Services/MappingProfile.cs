using AutoMapper;
using Cms.Services.Models.AirportDetails;
using Cms.Services.Models.CarHireDeals;
using Cms.Services.Models.CityCountry;
using Cms.Services.Models.CouponMaster;
using Cms.Services.Models.DummyVacationPackageMaster;
using Cms.Services.Models.FlightDealManagement;
using Cms.Services.Models.FlightFaresDetails;
using Cms.Services.Models.HolidayPackages;
using Cms.Services.Models.HotelDeals;
using Cms.Services.Models.MasterAirlines;
using Cms.Services.Models.MenuMaster;
using Cms.Services.Models.OpenAPIDataModel.CarHireDealsData;
using Cms.Services.Models.OpenAPIDataModel.CouponMaster;
using Cms.Services.Models.OpenAPIDataModel.FlightDealManagement;
using Cms.Services.Models.OpenAPIDataModel.FlightFaresDetails;
using Cms.Services.Models.OpenAPIDataModel.HolidayPackages;
using Cms.Services.Models.OpenAPIDataModel.HotelDealsData;
using Cms.Services.Models.OpenAPIDataModel.PackageItenaries;
using Cms.Services.Models.OpenAPIDataModel.Section;
using Cms.Services.Models.OpenAPIDataModel.SectionContent;
using Cms.Services.Models.OpenAPIDataModel.TemplateDetails;
using Cms.Services.Models.PackageItenaries;
using Cms.Services.Models.Portals;
using Cms.Services.Models.Section;
using Cms.Services.Models.SectionContent;
using Cms.Services.Models.SectionType;
using Cms.Services.Models.TemplateCategory;
using Cms.Services.Models.TemplateConfiguration;
using Cms.Services.Models.TemplateDetails;
using Cms.Services.Models.TemplateMaster;
using Cms.Services.Models.UserRole;
using Cms.Services.Models.UserRoleMenuPermissions;
using Cms.Services.Models.Users;
using Cms.Services.Models.UserSearchLogs;
using DataManager.DataClasses;

namespace Cms.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //TMM Mapping
            CreateMap<DataManager.TMMDbClasses.DynamicDestinationEnquiry, Models.TMMModals.DynamicDestinationEnquiryModal>().ReverseMap();
            CreateMap<DataManager.TMMDbClasses.GroupTravelFlightEnqueryDetails, Models.TMMModals.GroupTravelFlightEnqueryDetailsModal>().ReverseMap();
            CreateMap<DataManager.TMMDbClasses.Users, Models.TMMModals.UsersModal>().ReverseMap();
            CreateMap<DataManager.TMMDbClasses.Subscribe, Models.TMMModals.SubscribeModal>().ReverseMap();
            CreateMap<DataManager.TMMDbClasses.ContactUs, Models.TMMModals.ContactUsModal>().ReverseMap();
            CreateMap<DataManager.TMMDbClasses.EnqueryPageDetails, Models.TMMModals.EnqueryPageDetailsModal>().ReverseMap();
            CreateMap<DataManager.TMMDbClasses.FlightSearchDetails, Models.TMMModals.FlightSearchDetailsModal>().ReverseMap();
            CreateMap<DataManager.TMMDbClasses.HotelSearchDetails, Models.TMMModals.HotelSearchDetailsModal>().ReverseMap();
            CreateMap<DataManager.TMMDbClasses.CruiseSearchDetails, Models.TMMModals.CruiseSearchDetailsModal>().ReverseMap();
            CreateMap<DataManager.TMMDbClasses.CruiseEnquiry, Models.TMMModals.CruiseEnquiryModal>().ReverseMap();
            CreateMap<DataManager.TMMDbClasses.PriceTrackingCustomerInfo, Models.TMMModals.PriceTrackingCustomerInfoModal>().ReverseMap();
            CreateMap<DataManager.TMMDbClasses.BlogEnqueryPageDetails, Models.TMMModals.BlogEnqueryPageDetailsModal>().ReverseMap();
            CreateMap<DataManager.TMMDbClasses.VideoConsulation, Models.TMMModals.VideoConsulationModal>().ReverseMap();
            CreateMap<DataManager.TMMDbClasses.BookingPaxDetails, Models.TMMModals.BookingPaxDetailsModal>().ReverseMap();
            CreateMap<DataManager.TMMDbClasses.BookingJourneyDetails, Models.TMMModals.BookingJourneyDetailsModal>().ReverseMap();
            CreateMap<DataManager.TMMDbClasses.BookingTransactionDetails, Models.TMMModals.BookingTransactionDetailsModal>().ReverseMap();
            CreateMap<DataManager.TMMDbClasses.CruiseBookingTransactionDetails, Models.TMMModals.CruiseBookingTransactionDetailsModal>().ReverseMap();
            CreateMap<DataManager.TMMDbClasses.CustomerReviewRatings, Models.TMMModals.CustomerReviewRatingsModal>().ReverseMap();
            CreateMap<DataManager.TMMDbClasses.CustomerReviewRatings, Models.TMMModals.CustomerReviewRatingsData>().ReverseMap();
            CreateMap<DataManager.TMMDbClasses.QuotationEmailSupport, Models.TMMModals.QuotationEmailSupportModal>().ReverseMap();
            CreateMap<DataManager.TMMDbClasses.FlightsEnquiry, Models.TMMModals.FlightsEnquiryModal>().ReverseMap();
            // Hotel Admin Mapping
            CreateMap<DataManager.HotelAdmin.HotelBookingDetails, Models.HotelAdmin.HotelBookingDetailsModal>().ReverseMap();
            // Travel Admin Mapping
            CreateMap<DataManager.TransferAdmin.CarBookingTransactionDetails, Models.TransferAdmin.CarBookingTransactionDetailsModal>().ReverseMap();
            // Activity Admin Mapping
            CreateMap<DataManager.ActivityAdmin.ActivitySearchLogs, Models.ActivityAdmin.ActivitySearchLogsModal>().ReverseMap();
            CreateMap<DataManager.ActivityAdmin.ActivityBookingDetails, Models.ActivityAdmin.ActivityBookingDetailsModal>().ReverseMap();

            CreateMap<TemplateMaster, TemplateMasterModal>().ReverseMap();
            CreateMap<CreateTemplateMasterModal, TemplateMaster>().ReverseMap();
            CreateMap<UpdateTemplateMasterModal, TemplateMaster>().ReverseMap();
            CreateMap<TemplateConfiguration, TemplateConfigurationModal>().ReverseMap();
            CreateMap<CreateTemplateConfigurationModal, TemplateConfiguration>().ReverseMap();
            CreateMap<UpdateTemplateConfigurationModal, TemplateConfiguration>().ReverseMap();
            CreateMap<TemplateDetails, TemplateDetails_Trails>().ReverseMap();
            CreateMap<TemplateDetails, TemplateDetailsModal>().ReverseMap();
            CreateMap<CreateTemplateDetailsModal, TemplateDetails>().ReverseMap();
            CreateMap<UpdateTemplateDetailsModal, TemplateDetails>().ReverseMap();

            CreateMap<SectionContent, SectionContent_Trails>().ReverseMap();
            CreateMap<SectionContent, SectionContentModal>().ReverseMap();
            CreateMap<CreateSectionContentModal, SectionContent>().ReverseMap();
            CreateMap<UpdateSectionContentModal, SectionContent>().ReverseMap();
            
            CreateMap<Section, Section_Trails>().ReverseMap();
            CreateMap<Section, SectionModal>().ReverseMap();
            CreateMap<CreateSectionModal, Section>().ReverseMap();
            CreateMap<UpdateSectionModal, Section>().ReverseMap();

            CreateMap<UserRole, UserRoleModal>().ReverseMap();
            CreateMap<CreateUserRoleModal, UserRole>().ReverseMap();
            CreateMap<UpdateUserRoleModal, UserRole>().ReverseMap();

            CreateMap<Users, UsersModal>().ReverseMap();
            CreateMap<CreateUsersModal, Users>().ReverseMap();
            CreateMap<UpdateUsersModal, Users>().ReverseMap();

            CreateMap<Portals, PortalModal>().ReverseMap();
            CreateMap<CreatePortalModal, Portals>().ReverseMap();
            CreateMap<UpdatePortalModal, Portals>().ReverseMap();

            CreateMap<TemplateCategory, TemplateCategoryModal>().ReverseMap();
            CreateMap<CreateTemplateCategoryModal, TemplateCategory>().ReverseMap();
            CreateMap<UpdateTemplateCategoryModal, TemplateCategory>().ReverseMap();

            CreateMap<SectionType, SectionTypeModal>().ReverseMap();
            CreateMap<CreateSectionTypeModal, SectionType>().ReverseMap();
            CreateMap<UpdateSectionTypeModal, SectionType>().ReverseMap();
            
            CreateMap<FlightDealManagement, FlightDealManagementModal>().ReverseMap();
            CreateMap<CreateFlightDealManagementModal, FlightDealManagement>().ReverseMap();
            CreateMap<UpdateFlightDealManagementModal, FlightDealManagement>().ReverseMap();
            
            CreateMap<HotelDeals, HotelDealsModal>().ReverseMap();
            CreateMap<CreateHotelDealsModal, HotelDeals>().ReverseMap();
            CreateMap<UpdateHotelDealsModal, HotelDeals>().ReverseMap();

            CreateMap<HotelDeals, HotelDealsData>().ReverseMap();
            CreateMap<FlightDealManagement, FlightDealManagementData>().ReverseMap();
            CreateMap<TemplateDetails, TemplateDetailsData>().ForMember(dest => dest.TemplateType, opt => opt.MapFrom(src => src != null && src.TemplateMaster != null ? src.TemplateMaster.Name : ""));
            CreateMap<SectionContent, SectionContentData>().ReverseMap();
            CreateMap<Section, SectionData>().ForMember(dest => dest.SectionType, opt => opt.MapFrom(src => src!=null&& src.SectionType!=null?src.SectionType.Name:""));
            CreateMap<SectionData, Section>();

            CreateMap<CityCountry, CityCountryModal>().ReverseMap();
            CreateMap<AirportDetails, AirportDetailsModal>().ReverseMap();
            CreateMap<MasterAirlines, MasterAirlinesModal>().ReverseMap();
            CreateMap<HolidayPackages, HolidayPackages_Trails>().ReverseMap();
            CreateMap<HolidayPackages, HolidayPackagesModal>().ReverseMap();
            CreateMap<HolidayPackages, HolidayPackagesData>().ReverseMap();
            CreateMap<PackageItenaries, PackageItenariesModal>().ReverseMap();
            CreateMap<PackageItenaries, PackageItenaries_Trails>().ReverseMap();
            CreateMap<FlightFaresDetails, FlightFaresDetailsModal>().ReverseMap();
            CreateMap<FlightFaresDetails, FlightFaresDetailsData>().ReverseMap();
            CreateMap<PackageItenaries, PackageItenariesData>().ReverseMap();
            CreateMap<UserSearchLogs, UserSearchLogsModal>().ReverseMap();
            CreateMap<MenuMaster, MenuMasterModal>().ReverseMap();
            CreateMap<UserRoleMenuPermission, UserRoleMenuPermissionModal>().ReverseMap();
            CreateMap<DummyVacationPackageMaster, DummyVacationPackageMasterModal>().ReverseMap();
            //CreateMap(typeof(PaginatedList<>),typeof(PaginatedList<>));
            CreateMap(typeof(PaginatedList<>),typeof(PaginatedList<>));
            CreateMap<CouponMaster, CouponMasterModal>().ReverseMap();
            CreateMap<CouponMaster, CouponMasterData>()
                .ForMember(destinationMember=>destinationMember.PortalCode,opt=>opt.MapFrom(src=>src!=null && src.Portal!=null ? src.Portal.PortalCode:""))
                .ForMember(destinationMember=>destinationMember.PortalName,opt=>opt.MapFrom(src=>src!=null && src.Portal!=null ? src.Portal.Name:""));
            CreateMap<CouponMasterData, CouponMaster>();

            CreateMap<CarHireDeals, CarHireDealsModal>().ReverseMap();
            CreateMap<CreateCarHireDealsModel, CarHireDeals>().ReverseMap();
            CreateMap<UpdateCarHireModel, CarHireDeals>().ReverseMap();
            CreateMap<CarHireDeals, CarHireDealsData>().ReverseMap();

        }
    }
}
