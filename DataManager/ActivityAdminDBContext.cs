using DataManager.ActivityAdmin;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace DataManager
{
    public class ActivityAdminDBContext : DbContext
    {
        public ActivityAdminDBContext(DbContextOptions<ActivityAdminDBContext> options) : base(options)
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
        public DbSet<ActivitySearchLogs> ActivitySearchLogs { get; set; }
        public DbSet<ActivityBookingDetails> ActivityBookingDetails { get; set; }

    }
}
