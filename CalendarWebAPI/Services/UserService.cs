using CalendarWebAPI.DbModels;
using CalendarWebAPI.Repositories;

namespace CalendarWebAPI.Services
{
    public class UserService
    {
        public UserRepository _userRepository;
        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Models.User GetUser(string username)
        {
            return _userRepository.GetUser(username);
        }
        public List<Models.User> GetAll()
        {
            return _userRepository.GetAll();
        }
        public Models.User Add(Models.User user)
        {
            return _userRepository.Add(user);
        }
        public void Edit(Models.User user, bool adminEdit, string newPw)
        {
            _userRepository.Edit(user,adminEdit, newPw);
        }
        public void Delete(Guid guid)
        {
            _userRepository.Delete(guid);
        }

        public string Login(string username, string password)
        {
            return _userRepository.Login(username, password);
            
        }

        public void Signout()
        {
            _userRepository.Signout();
        }
    }
}
