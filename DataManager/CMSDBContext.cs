using DataManager.DataClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace DataManager
{
    public class CMSDBContext : DbContext
    {
        public CMSDBContext(DbContextOptions<CMSDBContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        public void Install()
        {
            var rdCreator = Database.GetService<IRelationalDatabaseCreator>();
            if (!rdCreator.Exists())
            {

                rdCreator.EnsureCreated();
            }
            else
            {

            }

        }
        public DbSet<MenuMaster> MenuMasters { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<TemplateMaster> TemplateMaster { get; set; }
        public DbSet<TemplateDetails> TemplateDetails { get; set; }
        public DbSet<TemplateDetails_Trails> TemplateDetails_Trails { get; set; }
        public DbSet<TemplateConfiguration> TemplateConfigurations { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Section_Trails> Sections_Trails { get; set; }
        public DbSet<SectionContent> SectionContent { get; set; }
        public DbSet<SectionContent_Trails> SectionContent_Trails { get; set; }
        public DbSet<TemplateCategory> TemplateCategory { get; set; }
        public DbSet<FlightDealManagement> DealManagements { get; set; }
        public DbSet<AirportDetails> AirportDetails { get; set; }
        public DbSet<MasterAirlines> MasterAirlines { get; set; }
        public DbSet<HotelDeals> HotelDeals { get; set; }
        public DbSet<CityCountry> City_Country { get; set; }
        public DbSet<HolidayPackages> HolidayPackages { get; set; }
        public DbSet<HolidayPackages_Trails> HolidayPackages_Trails { get; set; }
        public DbSet<PackageItenaries> PackageItenaries { get; set; }
        public DbSet<PackageItenaries_Trails> PackageItenaries_Trails { get; set; }
        public DbSet<FlightFaresDetails> FlightFaresDetails { get; set; }
        public DbSet<UserRoleMenuPermission> UserRoleMenuPermissions { get; set; }
        public DbSet<UserSearchLogs> UserSearchLogs { get; set; }
        public DbSet<DummyVacationPackageMaster> DummyVacationPackageMaster { get; set; }
        public DbSet<CouponMaster> CouponMaster { get; set; }
        public DbSet<CarHireDeals> CarHireDeals { get; set; }

    }
}
