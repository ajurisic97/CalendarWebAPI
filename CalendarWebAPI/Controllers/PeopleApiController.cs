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

        //Ovo je kasnije za sucelje:
        //[HttpPost]
        //public ActionResult<Person> Add([FromBody] JObject json)
        //{
        //    var p = PersonDto.FromJson(json);
        //    return _personService.Add(p);
        //}

        [HttpPost]
        public ActionResult<Person> Add(Person person)
        {
            return _personService.Add(person);
        }
    }
}
