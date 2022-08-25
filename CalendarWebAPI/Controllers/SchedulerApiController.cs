using CalendarWebAPI.Models;
using CalendarWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public ActionResult<List<FullSchedulerItem>> GetAll()
        {
            return _schedulerService.GetAll().ToList();
        }
        [HttpGet("person_id")]
        public ActionResult<List<FullSchedulerItem>> GetByPerson(Guid person_id)
        {
            return _schedulerService.GetByPerson(person_id).ToList();
        }

        [HttpGet("person_id,startdate,enddate")]
        public ActionResult<List<FullSchedulerItem>> GetByPerson(Guid person_id,DateTime startDate, DateTime endDate)
        {
            return _schedulerService.GetByDates(person_id,startDate,endDate).ToList();
        }



    }
}
