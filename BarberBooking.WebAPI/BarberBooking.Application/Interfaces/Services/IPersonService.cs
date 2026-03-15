using BarberBooking.Core.Entities;
using BarberBooking.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberBooking.Application.Interfaces.Services
{
    public interface IPersonService
    {
        Task<Result<Person>> AddPerson(Person person);

        Task<Result<Person>> UpdatePerson(Person person);

        Task<Result<Person>> GetPersonById(Guid id);

        Task<Result<IEnumerable<Person>>> GetAllPersons();

        Task<Result<Person>> GetPersonByIdWithBookings(Guid id);

    }
}
