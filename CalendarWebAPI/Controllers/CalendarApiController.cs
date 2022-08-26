using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CalendarWebAPI.Services;
using CalendarWebAPI.Models;
using CalendarWebAPI.Dtos;
using Newtonsoft.Json.Linq;

namespace CalendarWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarApiController : ControllerBase
    {
        public CalendarItemsService _calendarItemsService;
        public CalendarApiController(CalendarItemsService calendarService)
        {
            _calendarItemsService = calendarService;
        }

        [HttpGet("dt1,dt2,depth")]
        public ActionResult<List<FullCalendarDto>> GetAll(DateTime dt1, DateTime dt2,int depth=7)
        {

            var objectResult = _calendarItemsService.GetAll(dt1, dt2,depth).ToList();
            return objectResult;

        }

        [HttpPost]
        public ActionResult<Calendar> Add([FromBody] JObject json)
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

    }
}
