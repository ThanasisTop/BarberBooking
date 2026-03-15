using AutoMapper;
using BarberBooking.Application.DTOs;
using BarberBooking.Application.Interfaces.Services;
using BarberBooking.Core.Entities;
using BarberBooking.Core.Entities.Common;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BarberBooking.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;
        public PersonController(IPersonService personService, IMapper mapper)
        {
            _personService = personService;
            _mapper = mapper;
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetPersonById(Guid id)
        {
            var personResult = await _personService.GetPersonById(id);
            if (!string.IsNullOrEmpty(personResult.Error))
            {
                return BadRequest(personResult.Error);
            }

            return Ok(personResult);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllPersons()
        {
            var allPersons = await _personService.GetAllPersons();

            if (!string.IsNullOrEmpty(allPersons.Error))
            {
                return BadRequest(allPersons.Error);
            }

            var allPersonsDto = _mapper.Map<IEnumerable<PersonDto>>(allPersons.Value);

            return Ok(allPersonsDto);
        }

        [HttpGet("getPersonWithBookings/{id:Guid}")]
        public async Task<IActionResult> GetPersonWithBookings(Guid id)
        {
            var personResult = await _personService.GetPersonByIdWithBookings(id);
            if (!string.IsNullOrEmpty(personResult.Error))
            {
                return BadRequest(personResult.Error);
            }
            var person = personResult.Value; 
            return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> AddPerson(Person person)
        {
            var addPersonResult = await _personService.AddPerson(person);
            if (!string.IsNullOrEmpty(addPersonResult.Error))
            {
                return BadRequest(addPersonResult.Error);
            }

            return Ok(addPersonResult.Value);
        }
    }
}
