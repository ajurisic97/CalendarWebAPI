using System;
using System.Collections.Generic;

namespace CalendarWebAPI.DbModels
{
    public partial class User
    {
        public User()
        {
            Roles = new HashSet<Role>();
        }

        public Guid Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;

        public virtual ICollection<Role> Roles { get; set; }
    }
}
