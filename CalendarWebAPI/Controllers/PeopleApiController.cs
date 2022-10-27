using Microsoft.AspNetCore.Authorization;
using CalendarWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace CalendarWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Superadmin,Admin,User")]
    public class PeopleApiController : ControllerBase
    {
        public PersonService _personService;
        public PeopleApiController(PersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public ActionResult<List<Models.Person>> GetAll()
        {
            return _personService.GetAll().ToList();
        }

        [HttpGet]
        [Route("/getperson")]
        public Models.Person GetPerson(Guid guid)
        {
            return _personService.GetPerson(guid);
        }

        [HttpPost]
        public ActionResult<Models.Person> Add(Models.Person person)
        {
            return _personService.Add(person);
        }
    }
}
