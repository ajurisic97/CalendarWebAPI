using CalendarWebAPI.DbModels;
using CalendarWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CalendarWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkingDaysApiController : ControllerBase
    {
        public WorkingDayService _workingDaysService;
        public WorkingDaysApiController(WorkingDayService wds)
        {
            _workingDaysService = wds;
        }

        [HttpGet]
        public ActionResult<List<WorkingDay>> GetAll()
        {
            return _workingDaysService.GetWorkingDays().ToList();
        }

        [HttpPut]
        public void Update(WorkingDay workingDay)
        {
            _workingDaysService.UpdateWorkingDays(workingDay);
        }
    }
}
