using CalendarWebAPI.Models;
using CalendarWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<List<Event>> GetAll()
        {
            return _eventService.GetAll().ToList();
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
