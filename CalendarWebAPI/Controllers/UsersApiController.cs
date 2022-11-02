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

        [Route("/getusers/")]
        [HttpGet]
        public ActionResult<Models.UserViewModel> GetUsersByPage(string? filter,int page = 1, int pageSize = 10)
        {
            return _userService.GetUsersByPage(filter,page, pageSize);
        }


        [HttpGet("getall")]
        public ActionResult<List<Models.User>> GetUsers()
        {
            return _userService.GetAll();
        }

        [HttpGet]
        public ActionResult<object> GetUser(string username)
        {
            if (username != null)
            {
                return _userService.GetUser(username);

            }
            return null;

        }

        [HttpPut]
        [Authorize(Roles = "Superadmin,Admin,User")]

        public void EditUser([FromBody] JObject json)
        {
            Models.User user;
            var newPassword = "";
            var adminEdit = false;
            if (json["IsAdminEdit"] != null)
            {
                adminEdit = true;
                user = UserDto.FromJson(json);
            }
            else
            {
                user = UserDto.FromJsonPerson(json);
                newPassword = json["Password"].ToString();
            }
            _userService.Edit(user,adminEdit,newPassword);
            
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

            var response = _userService.Add(user);
            if (response == null)
            {
                return BadRequest(403);
            }
            else
            {
                return Ok(response);
            }
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
