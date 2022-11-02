namespace CalendarWebAPI.Models
{
    public class UserViewModel
    {
        public List<User> Users { get; set; }
        public int UserCount { get; set; }
        public UserViewModel(List<User> users, int userCount)
        {
            Users = users;
            UserCount = userCount;
        }
    }
}
