using BarberBooking.Application.Interfaces;
using BarberBooking.Core.Entities;
using BarberBooking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BarberBooking.Infrastructure.Repositories
{
    public class PersonRepository : GenericRepository<Person>,IPersonRepository
    {
        
        public PersonRepository(AppDbContext context):base(context)
        {
        }
       
        public async Task<Person> GetPersonByIdWithBookingsAsync(Guid id)
        {
            return await _context.Persons
                .Include(p => p.Bookings)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
