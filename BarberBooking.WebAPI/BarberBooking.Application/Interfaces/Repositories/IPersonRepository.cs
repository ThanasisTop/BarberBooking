using BarberBooking.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberBooking.Application.Interfaces
{
    public interface IPersonRepository
    {
        Task<Person> GetPersonByIdWithBookingsAsync(Guid id);  
    }
}
