using CalendarWebAPI.Models;

namespace CalendarWebAPI.Mappers
{
    public class UserMapper
    {
        public static User FromDatabase(DbModels.User u)
        {
            if (u == null)
                return null;
            bool isAdmin = false;
            if (u.UserRoles.Any(x => x.Role.Name == "Admin" || x.Role.Name=="Superadmin"))
            {
                isAdmin = true;
            }
            var personId = Guid.Empty;
            if (!isAdmin)
            {
                personId = (Guid)u.PersonId;
            }
            return new User(u.Id,u.Username, isAdmin, personId,u.UserRoles.Select(x=>x.Role.Name).FirstOrDefault());
        }
    }
}
