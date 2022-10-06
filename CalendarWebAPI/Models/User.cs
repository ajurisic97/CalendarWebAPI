namespace CalendarWebAPI.Models
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public Guid PersonId { get; set; }
        public User(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
        public User(string userName,bool isAdmin, Guid personId)
        {
            UserName = userName;
            IsAdmin = isAdmin;
            PersonId = personId;
        }
    }
}
