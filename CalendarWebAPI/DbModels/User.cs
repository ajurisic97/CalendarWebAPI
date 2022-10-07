using System;
using System.Collections.Generic;

namespace CalendarWebAPI.DbModels
{
    public partial class User
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public Guid Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public Guid? PersonId { get; set; }

        public virtual Person? Person { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
