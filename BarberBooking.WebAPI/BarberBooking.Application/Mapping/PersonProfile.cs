using AutoMapper;
using BarberBooking.Application.DTOs;
using BarberBooking.Core.Entities;

namespace BarberBooking.Application.Mapping
{
    public class PersonProfile:Profile
    {
        public PersonProfile()
        {
            CreateMap<Person, PersonDto>();
            CreateMap<PersonDto, Person>();
        }
    }
}
