using CalendarWebAPI.Dtos;
using CalendarWebAPI.Models;
using CalendarWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;


namespace CalendarWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulerApiController : ControllerBase
    {
        public SchedulerService _schedulerService;
        public SchedulerApiController(SchedulerService schedulerService)
        {
            _schedulerService = schedulerService;
        }


        [HttpGet("guids")]
        public ActionResult<List<PersonScheduler>> GetPersonCalendar(DateTime dt, DateTime dt2, [FromQuery] List<Guid> guids)
        {
            if (!guids.Any() || guids.All(xx => Guid.Empty == xx))
                return NotFound();

            
            return Ok(_schedulerService.GetPersonCalendars(guids, dt, dt2).ToList());
        }

        //ostavljam za buduce koristenje (sa start i enddate i filtrirano po eventima)
        /*[HttpGet]
        public ActionResult<List<FullSchedulerItem>> GetByPerson(Guid person_id, DateTime startDate, DateTime endDate)
        {
            return _schedulerService.GetByDates(person_id, startDate, endDate).ToList();
        }*/

        //[HttpPost]
        //public ActionResult<SchedulerItem> AddSchedulerItem([FromBody] JObject json)
        //{
        //    var schedulerInfo = SchedulerDto.FromJson(json);
        //    _schedulerService.AddRecurringSchedulerItems(schedulerInfo.SchedulerId, schedulerInfo.SchedulerItem.Date, schedulerInfo.SchedulerItem,null,null);
        //    return schedulerInfo.SchedulerItem;
        //}

        //period is "daily, weekly, monthly, yearly", endDate -> when does that recurring stop. If we don't write reccuring type and endDate we just post 1 
        // schedulerItem for date we wanted
        [HttpPost]
        public ActionResult<SchedulerItem> AddSchedulerItemRecurring(Guid person_id, int eventType, string? recurringType, DateTime? endDate,[FromBody] JObject json)
        {
            var schedulerItem = SchedulerDto.SIFromJson(json);
            _schedulerService.AddRecurringSchedulerItems(person_id,eventType,schedulerItem,recurringType,endDate);
            return Ok(schedulerItem);
        }

        [HttpPut("id")]
        public void Edit([FromBody] JObject json)
        {
            var schedulerInfo = SchedulerDto.FromJson(json);
            _schedulerService.Edit(schedulerInfo.SchedulerId, schedulerInfo.SchedulerItem.Date, schedulerInfo.SchedulerItem);
        }

        [HttpDelete("id")]
        public void Delete(Guid id)
        {
            _schedulerService.Delete(id);
        }
    }
}
