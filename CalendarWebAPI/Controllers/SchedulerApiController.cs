﻿using CalendarWebAPI.Dtos;
using CalendarWebAPI.Models;
using CalendarWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;

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
        [Authorize(Roles = "Admin,User")]
        public ActionResult<List<PersonScheduler>> GetPersonCalendar(DateTime dt, DateTime dt2, string appName, [FromQuery] List<Guid> guids)
        {
            if (!guids.Any() || guids.All(xx => Guid.Empty == xx))
                return NotFound();

            
            return Ok(_schedulerService.GetPersonCalendars(guids, dt, dt2,appName).ToList());
        }

        
        [HttpPut]
        [Authorize(Roles = "Admin,User")]
        public void EditOnSaveChanges([FromBody] List<JObject> json)
        {

            List<RecurringSchedulerItems> rsi = RecurringSchedulersDto.FromJson(json);

            _schedulerService.EditOnSaveChanges(rsi);
        }
        [HttpDelete("{ids}")]
        [Authorize(Roles = "Admin,User")]
        public void Delete(string ids)
        {
            var newIds = ids.Split(" ").Select(s => Guid.Parse(s)).ToList(); 
            
            _schedulerService.Delete(newIds);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public ActionResult<List<RecurringSchedulerItems>> AddOnSaveChanges(string appName,[FromBody] List<JObject> json)
        {
            List<RecurringSchedulerItems> rsi = RecurringSchedulersDto.FromJson(json);
            _schedulerService.AddOnSaveChanges(appName,rsi);
            return Ok();
        }
    }
}
