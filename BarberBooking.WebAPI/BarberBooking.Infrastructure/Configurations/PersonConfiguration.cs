using BarberBooking.Core.Entities;
using BarberBooking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberBooking.Infrastructure.Configurations
{
    public class PersonConfiguration : DbEntityConfiguration<Person>
    {
        public override void Configure(EntityTypeBuilder<Person> entity)
        {
            entity.HasMany(p => p.Bookings)
                .WithOne(b => b.Person)
            .HasForeignKey(b => b.PersonId)
            .IsRequired(false);

            entity.Property(p => p.Rating)
                .HasPrecision(3, 2);

        }
    }
}
