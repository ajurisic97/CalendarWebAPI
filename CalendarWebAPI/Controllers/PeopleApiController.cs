using Microsoft.AspNetCore.Authorization;
using CalendarWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using CalendarWebAPI.Dtos;

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
        [Route("/getpeople/")]
        [HttpGet]
        public ActionResult<Models.PersonViewModel> GetPeopleByPage(string? search,int page = 1, int pageSize = 10)
        {
            return _personService.GetPeopleByPage(search,page, pageSize);
        }
        [HttpPost]
        [Authorize(Roles = "Superadmin,Admin,User")]
        public ActionResult<Models.Person> Add([FromBody] JObject person)
        {
            Models.Person p = PersonDto.FromJson(person);
            return _personService.Add(p);
        }

        [HttpPut]
        [Authorize(Roles = "Superadmin,User")]
        public ActionResult<Models.Person> Edit([FromBody] JObject person)
        {
            Models.Person p = PersonDto.FromJson(person);
            return _personService.Edit(p);
        }

        [HttpDelete]
        [Authorize(Roles = "Superadmin,User")]
        public void Delete(Guid guid)
        {
             _personService.Delete(guid);
        }
    }
}
