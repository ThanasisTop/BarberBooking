using BarberBooking.Core.Entities;

namespace BarberBooking.Application.Interfaces
{
    public interface IPersonRepository
    {
        Task<Person> GetPersonByIdWithBookingsAsync(Guid id);  
    }
}
