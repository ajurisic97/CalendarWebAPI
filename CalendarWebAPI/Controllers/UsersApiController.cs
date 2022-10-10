using CalendarWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CalendarWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersApiController : ControllerBase
    {
        public UserService userService;
        public UsersApiController(UserService userService)
        {
            this.userService = userService;
        }


        [HttpGet("getall")]
        public ActionResult<List<Models.User>> GetUsers()
        {
            return userService.GetAll();
        }

        [HttpGet]
        public ActionResult<object> GetUser(string username,string pw)
        {
            Models.User user = new Models.User(username, pw);
            if(user != null)
            {
                return userService.GetUser(user);
                
            }
            return null;
        }
    }
}
