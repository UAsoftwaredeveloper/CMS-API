using DataManager.TMMDbClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace DataManager
{
    public class TMMDBContext : DbContext
    {
        public TMMDBContext(DbContextOptions<TMMDBContext> options) : base(options)
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
        public DbSet<Subscribe> Subscribe { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<EnqueryPageDetails> EnqueryPageDetails { get; set; }
        public DbSet<HotelSearchDetails> HotelSearchDetails { get; set; }
        public DbSet<FlightSearchDetails> FlightSearchDetails { get; set; }
        public DbSet<CruiseSearchDetails> CruiseSearchDetails { get; set; }
        public DbSet<VideoConsulation> VideoConsulations { get; set; }
        public DbSet<PriceTrackingCustomerInfo> PriceTrackingCustomerInfos { get; set; }
        public DbSet<BlogEnqueryPageDetails> BlogEnqueryPageDetails { get; set; }
        public DbSet<CruiseEnquiry> CruiseEnquiries { get; set; }
        public DbSet<FlightsEnquiry> FlightsEnquiries { get; set; }
        public DbSet<BookingJourneyDetails> BookingJourneyDetails { get; set; }
        public DbSet<BookingPaxDetails> BookingPaxDetails { get; set; }
        public DbSet<BookingTransactionDetails> BookingTransactionDetails { get; set; }
        public DbSet<CruiseBookingTransactionDetails> CruiseBookingTransactionDetails { get; set; }
        public DbSet<CustomerReviewRatings> CustomerReviewRatings { get; set; }
        public DbSet<GroupTravelFlightEnqueryDetails> GroupTravelFlightEnqueryDetails { get; set; }
        public DbSet<QuotationEmailSupport> QuotationEmailSupport { get; set; }
        public DbSet<DynamicDestinationEnquiry> DynamicDestinationEnquiries { get; set; }
    }
}
