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

        
        [HttpPut]
        public void EditOnSaveChanges([FromBody] List<JObject> json)
        {

            List<RecurringSchedulerItems> rsi = RecurringSchedulersDto.FromJson(json);

            _schedulerService.EditOnSaveChanges(rsi);
        }
        [HttpDelete("{ids}")]
        public void Delete(string ids)
        {
            var newIds = ids.Split(" ").Select(s => Guid.Parse(s)).ToList(); 
            
            _schedulerService.Delete(newIds);
        }

        [HttpPost]
        public ActionResult<List<RecurringSchedulerItems>> AddOnSaveChanges([FromBody] List<JObject> json)
        {
            List<RecurringSchedulerItems> rsi = RecurringSchedulersDto.FromJson(json);
            _schedulerService.AddOnSaveChanges(rsi);
            return Ok();
        }
    }
}
