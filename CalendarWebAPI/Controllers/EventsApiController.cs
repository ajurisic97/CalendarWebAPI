using CalendarWebAPI.Models;
using CalendarWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;

namespace CalendarWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsApiController : ControllerBase
    {
        public EventService _eventService;
        public EventsApiController(EventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]

        public ActionResult<List<Event>> GetAll(string? applicationName="Scheduler")
        {
            return _eventService.GetAll(applicationName).ToList();
        }
        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public ActionResult<Event> Add(string appName,string eventName,string eventDescription)
        {
            return _eventService.Add(appName,eventName,eventDescription);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin,User")]
        public void Delete(int eventType)
        {
            _eventService.Delete(eventType);
        }

        //Uncomment to reset(remove) all recurrings, events and person and new ones from json files
        //[HttpPost]
        //public ActionResult<object> PostERP()
        //{
        //    _eventService.AddEventsRecurringsAndPeople();
        //    return Ok();
        //}
    }
}
