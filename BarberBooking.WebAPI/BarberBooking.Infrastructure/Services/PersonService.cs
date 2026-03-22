using BarberBooking.Application.Interfaces;
using BarberBooking.Application.Interfaces.Services;
using BarberBooking.Core.Entities;
using BarberBooking.Core.Entities.Common;

namespace BarberBooking.Infrastructure.Services
{
    public class PersonService : IPersonService
    {
        private readonly IGenericRepository<Person> _genericRepository;
        private readonly IPersonRepository _personRepository;
        public PersonService(IGenericRepository<Person> genericRepository, IPersonRepository personRepository) 
        {
            _genericRepository = genericRepository;
            _personRepository = personRepository;
        }
        public async Task<Result<Person>> AddPerson(Person person)
        {
            try
            {
                var createdPerson = await _genericRepository.AddAsync(person);
                return Result<Person>.Success(createdPerson);
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return Result<Person>.Failure($"An error occurred while adding the person.{ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<Person>>> GetAllPersons()
        {
            try
            {
                var persons = await _genericRepository.GetAllAsync(p => true); // Get all persons without any filter
                return Result<IEnumerable<Person>>.Success(persons);
                
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return Result<IEnumerable<Person>>.Failure($"An error occurred while getting all persons.{ex.Message}");
            }
        }

        public async Task<Result<Person>> GetPersonById(Guid id)
        {
            try
            {
                var person = await _genericRepository.GetByIdAsync(id);
                if (person == null)
                {
                    return Result<Person>.Failure("Person not found.");
                }

                return Result<Person>.Success(person);

            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return Result<Person>.Failure($"An error occurred while retrieving the person. {ex.Message}");
            }
        }

        public Task<Result<Person>> UpdatePerson(Person person)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<Person>> GetPersonByIdWithBookings(Guid id)
        {
            try
            {
                var person = await _personRepository.GetPersonByIdWithBookingsAsync(id);
                if (person == null)
                {
                    return Result<Person>.Failure("Person not found.");
                }

                return Result<Person>.Success(person);

            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return Result<Person>.Failure($"An error occurred while retrieving the person. {ex.Message}");
            }
        }
    }
}
