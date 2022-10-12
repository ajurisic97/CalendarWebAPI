namespace CalendarWebAPI.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid PersonId { get; set; }
        public Guid RoleId { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public User(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
        public User(Guid userId,string userName, Guid personId, Guid roleId, string email,string password)
        {
            Id = userId;
            UserName = userName;
            PersonId = personId;
            RoleId = roleId;
            Email = email;
            Password = password;
        }
    }
}
