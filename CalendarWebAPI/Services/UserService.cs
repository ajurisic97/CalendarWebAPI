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
        public Models.User GetUser(Models.User u)
        {
            return _userRepository.GetUser(u);
        }
        public List<Models.User> GetAll()
        {
            return _userRepository.GetAll();
        }
        public Models.User Add(Models.User user)
        {
            return _userRepository.Add(user);
        }
        public void Edit(Models.User user)
        {
            _userRepository.Edit(user);
        }
    }
}
