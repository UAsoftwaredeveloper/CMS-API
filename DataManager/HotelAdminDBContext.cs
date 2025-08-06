using DataManager.HotelAdmin;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace DataManager
{
    public class HotelAdminDBContext : DbContext
    {
        public HotelAdminDBContext(DbContextOptions<HotelAdminDBContext> options) : base(options)
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
        public DbSet<HotelBookingDetails> HotelBookingDetails { get; set; }

    }
}
