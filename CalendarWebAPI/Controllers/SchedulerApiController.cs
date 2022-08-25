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
        //[HttpGet("person_id")]
        //public ActionResult<List<FullSchedulerItem>> GetByPerson(Guid person_id)
        //{
        //    return _schedulerService.GetByPerson(person_id).ToList();
        //}

        [HttpGet("person_id,startdate,enddate")]
        public ActionResult<List<FullSchedulerItem>> GetByPerson(Guid person_id,DateTime startDate, DateTime endDate)
        {
            return _schedulerService.GetByDates(person_id,startDate,endDate).ToList();
        }

        [HttpPost]
        public ActionResult<SchedulerItem> AddSchedulerItem([FromBody] JObject json)
        {
            var schedulerInfo = SchedulerDto.FromJson(json);
            return _schedulerService.AddSchedulerItem(schedulerInfo.SchedulerId,schedulerInfo.SchedulerItem.Date,schedulerInfo.SchedulerItem);
            
        }

        [HttpPut]
        public void Edit([FromBody] JObject json)
        {
            var schedulerInfo = SchedulerDto.FromJson(json);
            _schedulerService.Edit(schedulerInfo.SchedulerId,schedulerInfo.SchedulerItem.Date, schedulerInfo.SchedulerItem);
        }

    }
}
