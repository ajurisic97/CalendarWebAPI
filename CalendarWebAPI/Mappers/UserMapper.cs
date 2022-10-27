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
            if (u.PersonId != null)
            {
                personId = (Guid)u.PersonId;

            }
            //if (!u.UserRoles.Any(x => x.Role.Name == "Admin" || x.Role.Name=="Superadmin"))
            //{
            //    if(u.PersonId!=null)
            //    personId = (Guid)u.PersonId;
            //}
            var user = new User(u.Id, u.Username, personId, u.UserRoles.Select(x => x.Role.Id).FirstOrDefault(), u.Email, "");
            if(u.UserRoles.Any())
            user.Role = u.UserRoles.Select(x => x.Role.Name).First();
            return user;
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
