using BarberBooking.Core.Entities;
using BarberBooking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberBooking.Infrastructure.Configurations
{
    public class BookingConfiguration : DbEntityConfiguration<Booking>
    {
        public override void Configure(EntityTypeBuilder<Booking> entity)
        {

            entity.HasOne(b => b.Person)
                .WithMany(p => p.Bookings)
                .HasForeignKey(b => b.PersonId);

            entity.HasIndex(b => new { b.PersonId, b.BookingDate, b.Time })
                  .IsUnique();

            entity.Property(b => b.Email)
                  .IsRequired(false);

            entity.Property(b => b.Phone)
                  .IsRequired(false);
        }
    }
}
