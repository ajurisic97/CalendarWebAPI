using CalendarWebAPI.Models;
using CalendarWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CalendarWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleApiController : ControllerBase
    {
        public PersonService _personService;
        public PeopleApiController(PersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public ActionResult<List<Person>> GetAll()
        {
            return _personService.GetAll().ToList();
        }

        [HttpGet]
        [Route("/getperson")]
        public Person GetPerson(Guid guid)
        {
            return _personService.GetPerson(guid);
        }

        [HttpPost]
        public ActionResult<Person> Add(Person person)
        {
            return _personService.Add(person);
        }
    }
}
