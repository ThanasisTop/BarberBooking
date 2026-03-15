using BarberBooking.Core.Entities;
using BarberBooking.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BarberBooking.Infrastructure.Persistence
{
    public class AppDbContext : AppDbContextBase
    {
       
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfiguration(new BookingConfiguration());
            modelBuilder.AddConfiguration(new PersonConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
