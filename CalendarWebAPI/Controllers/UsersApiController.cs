using CalendarWebAPI.Dtos;
using CalendarWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace CalendarWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersApiController : ControllerBase
    {
        public UserService _userService;
        
        public UsersApiController(UserService userService)
        {
            this._userService = userService;
        }


        [HttpGet("getall")]
        [Authorize(Roles ="Superadmin,Admin,User")]
        public ActionResult<List<Models.User>> GetUsers()
        {
            return _userService.GetAll();
        }

        [HttpGet]
        [Authorize(Roles = "Superadmin,Admin,User")]
        public ActionResult<object> GetUser(string username)
        {
            if (username != null)
            {
                return _userService.GetUser(username);

            }
            return null;

        }


        [HttpPut]
        [Authorize(Roles = "Superadmin")]

        public void EditUser([FromBody] JObject json)
        {
            var user = UserDto.FromJson(json);

            _userService.Edit(user);
            
        }

        [HttpDelete]
        [Authorize(Roles = "Superadmin")]

        public void DeleteUser(Guid guid)
        {
            _userService.Delete(guid);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var response = _userService.Login(username, password);
            if(response == "User not found")
            {
                return NotFound(response);
            }
            else
            {
                //Request.Headers.Add("Authorization", response);
                return Ok(response);
            }
        }
        [AllowAnonymous]
        [Route("/signup")]
        [HttpPost]
        public IActionResult Signup([FromBody] JObject json)
        {
            var user = UserDto.FromJson(json);

            return Ok(_userService.Add(user));
        }

        [AllowAnonymous]
        [Route("/signout")]
        [HttpPost]
        public void Signout()
        {
            _userService.Signout();
        }

    }
}
