using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using CalendarWebAPI.Services;
using CalendarWebAPI.Dtos;
using Newtonsoft.Json.Linq;


namespace CalendarWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Superadmin,Admin,User")]
    public class CalendarApiController : ControllerBase
    {
        public CalendarItemsService _calendarItemsService;
        public CalendarApiController(CalendarItemsService calendarService)
        {
            _calendarItemsService = calendarService;
        }
        [AllowAnonymous]
        [HttpGet("dt1,dt2,depth")]
        public ActionResult<List<FullCalendarDto>> GetAll(DateTime dt1, DateTime dt2,int depth=7)
        {

            var objectResult = _calendarItemsService.GetAll(dt1, dt2,depth).ToList();
            return objectResult;

        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult<List<Models.FilteredCalendarItem>> GetAllCalendarItems(DateTime dt, DateTime dt2)
        {
            return _calendarItemsService.GetCalendarItemsWithSubCulendar(dt, dt2);
        }

        [HttpPost]
        public ActionResult<Models.Calendar> Add([FromBody] JObject json)
        {
            var calendar = CalendarDto.FromJson(json);
            return _calendarItemsService.Add(calendar);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _calendarItemsService.Delete(id);
        }

        [HttpPut]
        public void Edit([FromBody] JObject json)
        {
            var calendar = CalendarDto.FromJson(json);
            _calendarItemsService.Edit(calendar);
        }

        [HttpPost("{calendarId}")]
        public ActionResult<Models.CalendarItem> AddCalendarItem(Guid calendarId,[FromBody]JObject json)
        {
            var dbCalendarItem = CalendarItemsDto.FromJson(json);
            return _calendarItemsService.AddCalendarItem(calendarId,dbCalendarItem);
        }
    }
}
