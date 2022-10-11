using CalendarWebAPI.Models;
using Newtonsoft.Json.Linq;

namespace CalendarWebAPI.Dtos
{
    public class UserDto
    {
        public static User FromJson(JObject json)
        {
            var jsonId = json["Id"];
            var id = Guid.NewGuid();
            if(jsonId.ToString()!="")
            {
                id = jsonId.ToObject<Guid>();
            }
            var jsonPerson = json["PersonId"];
            var personId = Guid.Empty;
            if (jsonPerson.Count()!=0)
            {
                personId = jsonPerson.ToObject<Guid>();
            }
            var username = json["Username"].ToString();
            var role = json["Role"].ToString();
            var email = json["Email"].ToString();
            var password = json["Password"].ToString();
            return new User(id,username,personId,role,email,password);
        }
    }
}
/*
{
  "Id": null,
  "Username": "test",
  "Password": "test",
  "PersonId": null,
  "Role": "User",
  "Email": "test@gmail.com"
},
{
  "Id": "325E8761-FC36-43DA-BA04-E87E4D92C93A",
  "Username": "LEGION2",
  "Password": "LEGION2",
  "PersonId": null,
  "Role": "Admin",
  "Email": "LEGION2@gmail.com"
}
 * */