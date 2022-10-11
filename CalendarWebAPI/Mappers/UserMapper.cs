using CalendarWebAPI.Models;

namespace CalendarWebAPI.Mappers
{
    public class UserMapper
    {
        public static User FromDatabase(DbModels.User u)
        {
            if (u == null)
                return null;
            var personId = Guid.Empty;
            if (!u.UserRoles.Any(x => x.Role.Name == "Admin" || x.Role.Name=="Superadmin"))
            {
                if(u.PersonId!=null)
                personId = (Guid)u.PersonId;
            }
            return new User(u.Id,u.Username, personId,u.UserRoles.Select(x=>x.Role.Name).FirstOrDefault(),u.Email,u.Password);
        }

        public static DbModels.User ToDatabase(User user)
        {
            return new DbModels.User()
            {
                Id = (user.Id != Guid.Empty)&&(user.Id!=null) ? user.Id : Guid.NewGuid(),
                Username = user.UserName,
                Password = user.Password,
                PersonId = user.PersonId!=Guid.Empty ? user.PersonId : null,
                Email=user.Email,
                
                

            };
        }
    }
}
