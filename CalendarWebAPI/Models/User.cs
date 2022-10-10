namespace CalendarWebAPI.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public Guid PersonId { get; set; }
        public string Role { get; set; }
        public User(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
        public User(Guid userId,string userName,bool isAdmin, Guid personId, string role)
        {
            Id = userId;
            UserName = userName;
            IsAdmin = isAdmin;
            PersonId = personId;
            Role = role;
        }
    }
}
