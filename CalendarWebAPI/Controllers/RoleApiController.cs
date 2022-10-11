using CalendarWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CalendarWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleApiController : ControllerBase
    {
        public RoleService _roleService;
        public RoleApiController(RoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpGet]
        public ActionResult<List<Models.Role>> GetRoles()
        {
            return _roleService.GetAll().ToList();
        }
    }
}
