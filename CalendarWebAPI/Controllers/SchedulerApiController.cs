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


        //Ostavljam zakomentirano za potrebe eventualnog kasnijeg testiranja/koristenja
        //[HttpGet]
        //public ActionResult<List<FullSchedulerItem>> GetAll()
        //{
        //    return _schedulerService.GetAll().ToList();
        //}
        [HttpGet("person_id")]
        public ActionResult<List<FullSchedulerItem>> GetByPerson(Guid person_id)
        {
            
            return _schedulerService.GetByPerson(person_id).ToList();
        }

        [HttpGet("person_id,startdate,enddate")]
        public ActionResult<List<FullSchedulerItem>> GetByPerson(Guid person_id, DateTime startDate, DateTime endDate)
        {
            return _schedulerService.GetByDates(person_id, startDate, endDate).ToList();
        }

        [HttpPost]
        public ActionResult<SchedulerItem> AddSchedulerItem([FromBody] JObject json)
        {
            var schedulerInfo = SchedulerDto.FromJson(json);
            _schedulerService.AddRecurringSchedulerItems(schedulerInfo.SchedulerId, schedulerInfo.SchedulerItem.Date, schedulerInfo.SchedulerItem,null,null);
            return schedulerInfo.SchedulerItem;
        }

        //period is "daily, weekly, monthly, yearly", endDate -> when does that recurring stop. If we don't write reccuring type and endDate we just post 1 
        // schedulerItem for date we wanted
        [HttpPost("period,enddate")]
        public ActionResult<SchedulerItem> AddSchedulerItemRecurring(string? recurringType, DateTime? endDate,[FromBody] JObject json)
        {
            var schedulerInfo = SchedulerDto.FromJson(json);
            _schedulerService.AddRecurringSchedulerItems(schedulerInfo.SchedulerId, schedulerInfo.SchedulerItem.Date, schedulerInfo.SchedulerItem,recurringType,endDate);
            return schedulerInfo.SchedulerItem;
        }

        [HttpPut("id")]
        public void Edit(Guid id,[FromBody] JObject json)
        {
            var schedulerInfo = SchedulerDto.FromJson(json);
            schedulerInfo.SchedulerId = id;
            _schedulerService.Edit(schedulerInfo.SchedulerId, schedulerInfo.SchedulerItem.Date, schedulerInfo.SchedulerItem);
        }

        [HttpDelete("id")]
        public void Delete(Guid id)
        {
            _schedulerService.Delete(id);
        }
    }
}
